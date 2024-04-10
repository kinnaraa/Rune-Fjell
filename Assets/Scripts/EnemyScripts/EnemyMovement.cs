using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    public float stopDistance = 4f;
    public float detectionDistance = 15f;
    private bool readyToAttack = true;
    private Animator batAnimator;
    public Collider damageCollider;

    void Start()
    { 
        batAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        player = GameObject.Find("Player").transform;
        if(player)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
            // Calculate direction in local space
            Vector3 direction = (player.position - transform.position).normalized;

            // Rotate towards the player on the y-axis only
            Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);


            if (distanceToPlayer <= detectionDistance && distanceToPlayer > stopDistance)
            {
                // Move towards the player using Translate
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }
            else if (distanceToPlayer <= stopDistance)
            {
                if(readyToAttack)
                {
                    StartCoroutine(Attack());
                    StartCoroutine(Cooldown());
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
        damageCollider.enabled = true;
        batAnimator.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(3f);
        batAnimator.SetBool("IsAttacking", false);
        damageCollider.enabled = false;
    }
}
