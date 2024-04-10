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

    public Vector3 spawn;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        Animator = GetComponentInChildren<Animator>();
        spawn = transform.position;
    }

    void Update()
    {
        if(transform.position.y <= -2)
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
                        StartCoroutine(Cooldown());
                    }
                    else
                    {
                        Attacking = true;
                        StartCoroutine(Spit());
                        StartCoroutine(Cooldown());
                    }
                }
            }
        }
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
        yield return new WaitForSeconds(2.5f);
        transform.Translate(Vector3.forward * 100f * Time.deltaTime);
        yield return new WaitForSeconds(1f);
        damageCollider.enabled = false;
        Attacking = false;
    }

    public IEnumerator Spit()
    {
        Animator.SetTrigger("SpitAttack");
        yield return new WaitForSeconds(2.5f);
        GameObject spitball = Instantiate(Resources.Load<GameObject>("SpellPrefabs/SpitBall"), transform.position, Resources.Load<GameObject>("SpellPrefabs/SpitBall").transform.rotation);
        spitball.GetComponent<Rigidbody>().velocity = transform.forward * 20;
        Attacking = false;
    }
}
