using System.Collections;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().PlayerHealth -= 10;
        }
    }

}
