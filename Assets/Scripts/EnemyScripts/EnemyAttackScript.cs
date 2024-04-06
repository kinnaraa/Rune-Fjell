using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    public int damage = 10;
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().PlayerHealth -= damage;
            if(gameObject.transform.parent.name == "IceCreature(Clone)")
            {
                Instantiate(Resources.Load<GameObject>("Modifiers/Cold"), other.transform.position, Resources.Load<GameObject>("Modifiers/Cold").transform.rotation).transform.parent = other.transform;
            }
        }
    }

}
