using UnityEngine;

public class ForceFieldDamage : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance < 4)
        {
            player.GetComponent<PlayerMagic>().damageModifier = 2;
        }
    }
}
