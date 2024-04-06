using UnityEngine;

public class WyrmSpitAttack : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().PlayerHealth -= 10;
            Destroy(gameObject.transform.parent.gameObject);
        }    
    }
}
