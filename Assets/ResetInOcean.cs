using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetInOcean : MonoBehaviour
{
    public GameObject Player;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            Player.GetComponent<Player>().Kill();
        }
    }
}
