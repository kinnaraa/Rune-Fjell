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

    void Start()
    {
        player = GameObject.Find("Player").transform;
        Animator = GetComponentInChildren<Animator>();
    }

    void Update()
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
            Animator.SetBool("Moving", true);
        }
        else if (distanceToPlayer <= stopDistance)
        {
            Animator.SetBool("Moving", false);
            if(readyToAttack)
            {
                StartCoroutine(Attack());
                StartCoroutine(Cooldown());
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
        Animator.SetTrigger("Attack");
        yield return new WaitForSeconds(3f);
        Animator.SetTrigger("Attack");
        damageCollider.enabled = false;
    }
}
