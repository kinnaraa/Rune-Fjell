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
        if(other.gameObject.GetComponent<EnemyHealth>())
        {
            other.gameObject.GetComponent<EnemyHealth>().currentHealth = 0;
        }
    }
}
