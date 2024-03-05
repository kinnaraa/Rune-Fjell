using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    public float stopDistance = 6f;
    public float detectionDistance = 15f;
    public GameObject Text;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        //StartCoroutine(EekEekBitch());
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionDistance && distanceToPlayer > stopDistance)
        {
            // Calculate direction in local space
            Vector3 direction = (player.position - transform.position).normalized;

            // Rotate towards the player on the y-axis only
            Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

            // Move towards the player using Translate
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else if (distanceToPlayer <= stopDistance)
        {
            // Stop moving or add additional behavior
        }
    }

    public IEnumerator EekEekBitch()
    {
        yield return new WaitForSeconds(10f);
        Text.SetActive(true);
    }
}
