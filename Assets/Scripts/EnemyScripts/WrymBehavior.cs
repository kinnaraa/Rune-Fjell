using System.Collections;
using UnityEngine;

public class WrymBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    public float stopDistance = 4f;
    public float detectionDistance = 15f;
    private bool readyToAttack = true;
    private Animator Animator;
    public Collider damageCollider;
    private bool Attacking;
    public AudioSource attackSound;
    public AudioSource spitSound;
    public AudioSource enterSound;

    public Transform spitPos;

    public Vector3 spawn;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        Animator = GetComponentInChildren<Animator>();
        spawn = transform.position;
        StartCoroutine(WhoAreYou());
    }

    void Update()
    {
        if(transform.position.y <= -5)
        {
            transform.position = spawn;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        // Calculate direction in local space
        Vector3 direction = (player.position - transform.position).normalized;

        // Rotate towards the player on the y-axis only
        Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        if(!Attacking)
        {
            if (distanceToPlayer <= detectionDistance && distanceToPlayer > stopDistance)
            {
                // Move towards the player using Translate
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                Animator.SetBool("Moving", true);
            }
            else if (distanceToPlayer <= stopDistance)
            {
                Animator.SetBool("Moving", false);
                if(readyToAttack)
                {
                    System.Random random = new System.Random();
                    int AttackChoice = random.Next(0, 2);
                    if(AttackChoice == 1)
                    {
                        Attacking = true;
                        StartCoroutine(Attack());
                    }
                    else
                    {
                        Attacking = true;
                        StartCoroutine(Spit());
                    }
                }
            }
        }
    }

    public IEnumerator WhoAreYou() {

        yield return new WaitForSeconds(7f);
        enterSound.Play();

    }


    public IEnumerator Cooldown()
    {
        readyToAttack = false;
        yield return new WaitForSeconds(4f);
        readyToAttack = true;
    }

    public IEnumerator Attack()
    {
        Animator.SetTrigger("StandAttack");
        damageCollider.enabled = true;
        attackSound.Play();
        yield return new WaitForSeconds(3f);
        damageCollider.enabled = false;
        Attacking = false;
        StartCoroutine(Cooldown());
    }

    public IEnumerator Spit()
    {
        Animator.SetTrigger("SpitAttack");
        yield return new WaitForSeconds(2.5f);
        spitSound.Play();
        // Calculate the direction towards the player
        Vector3 direction = (player.position - spitPos.position).normalized;
        GameObject spitball = Instantiate(Resources.Load<GameObject>("SpellPrefabs/SpitBall"), spitPos.position, Resources.Load<GameObject>("SpellPrefabs/SpitBall").transform.rotation);
        
        spitball.GetComponent<Rigidbody>().velocity = direction * 20;
        Attacking = false;
        StartCoroutine(Cooldown());
    }
}
