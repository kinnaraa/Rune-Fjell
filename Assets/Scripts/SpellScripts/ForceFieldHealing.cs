using System.Collections;
using UnityEngine;

public class ForceFieldHealing : MonoBehaviour
{
    private GameObject player;
    private bool isRegeneratingHealth = false;
    private float distance;
    void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(RegainHealth());
    }
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
    }

    IEnumerator RegainHealth()
    {
        while (distance <= 10 && !isRegeneratingHealth)
        {
            if (player.GetComponent<Player>().PlayerHealth < 100)
            {
                player.GetComponent<Player>().PlayerHealth += 5f; // Adjust the increment value as needed
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
