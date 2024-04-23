using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float sprintSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;

    private Player playerScript;
    private Animator animator;

    public AudioClip walkingSound;
    private AudioSource SpecialSounds;
    public float stepCoolDown;
    public float stepRate = 0.1f;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();

        readyToJump = true;
        playerScript = GameObject.Find("Player").GetComponent<Player>();

        StartCoroutine(DeductStam());
        StartCoroutine(RegainStam());

        SpecialSounds = GameObject.Find("SpecialPlayerAudio").GetComponent<AudioSource>();
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
        {
            rb.drag = groundDrag;
        } 
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    public IEnumerator soundDelay()
    {
        SpecialSounds.Play();
        yield return new WaitForSeconds(SpecialSounds.clip.length);
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Check if the player is moving (not idle)
        bool isWalking = (horizontalInput != 0 || verticalInput != 0);

        // Set the "IsWalking" parameter of the animator
        animator.SetBool("IsWalking", isWalking);
        if (isWalking && stepCoolDown <= 0f)
        {
            if(((SpecialSounds.clip.name == "Player_DeathV1" || SpecialSounds.clip.name == "Player_Magic") && !SpecialSounds.isPlaying) || (SpecialSounds.clip.name == "Player_Footsteps" && !SpecialSounds.isPlaying)) {
                SpecialSounds.clip = walkingSound;
                SpecialSounds.Play();
                stepCoolDown = 500f;
            }
            
        }
        else if (!isWalking && SpecialSounds.clip.name == "Player_Footsteps")
        {
            SpecialSounds.Stop();
            stepCoolDown = 0;
        }
        else
        {
            stepCoolDown = stepCoolDown - stepRate;
        }

        // on ground
        if (grounded)
        {
            if (Input.GetKey(sprintKey) && playerScript.PlayerStamina > 5)
            {
                rb.AddForce(moveDirection.normalized * (moveSpeed * sprintSpeed) * 10f, ForceMode.Force);
                animator.SetBool("IsRunnin", true);
                if (stepCoolDown <= 0f)
                {
                    SpecialSounds.clip = walkingSound;
                    SpecialSounds.Play();
                    stepCoolDown = 500f;
                }
            }
            else
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
                animator.SetBool("IsRunnin", false);
                
            }
        }
        // in air
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(Input.GetKey(sprintKey))
        {
            rb.AddForce(transform.up * (jumpForce*2), ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        playerScript.PlayerStamina -= 10;
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    public IEnumerator DeductStam()
    {
        while (true)
        {
            if (Input.GetKey(sprintKey) && (horizontalInput != 0 || verticalInput != 0) && playerScript.PlayerStamina > 1f)
            {
                playerScript.PlayerStamina -= 1f;
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    public IEnumerator RegainStam()
    {
        float timeSinceLastSprint = 0f; // Track time since the last sprint key press

        while (true)
        {
            // Check if the player has not used the sprint key recently
            if (!Input.GetKey(sprintKey))
            {
                timeSinceLastSprint += Time.deltaTime;
            }
            else
            {
                timeSinceLastSprint = 0f; // Reset the timer if the sprint key is pressed
            }

            // Check if the player has not used the sprint key for a while and their stamina is below 100
            if (timeSinceLastSprint > 3f && playerScript.PlayerStamina < 100)
            {
                // Increment stamina over time
                playerScript.PlayerStamina += 5f; // Adjust the increment value as needed
                timeSinceLastSprint = 0f;
            }

            yield return null;
        }
    }
}