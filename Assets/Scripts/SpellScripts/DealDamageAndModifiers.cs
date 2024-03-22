using System;
using UnityEngine;

public class DealDamageAndModifiers : MonoBehaviour
{
    private PlayerMagic PM;
    public GameObject[] Modifiers;
    public void Start() 
    {
        PM = GameObject.Find("Player").GetComponent<PlayerMagic>();
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.GetComponent<EnemyHealth>())
        {
            foreach(Ability a in PM.abilities)
            {
                if(a.name == gameObject.name)
                {
                    other.GetComponent<EnemyHealth>().currentHealth -= a.damage;
                    StartCoroutine(other.GetComponent<EnemyHealth>().FlashRed());

                    if(a.Modifier == 0)
                    {
                        return;
                    }
                    else if(a.Modifier == 1)
                    {
                        Instantiate(Modifiers[1], other.transform.position, Modifiers[1].transform.rotation).transform.parent = other.transform;
                    }
                    else if(a.Modifier == 2)
                    {
                        Instantiate(Modifiers[2], other.transform.position, Modifiers[2].transform.rotation).transform.parent = other.transform;
                    }
                    else if(a.Modifier == 3)
                    {
                        Instantiate(Modifiers[3], other.transform.position, Modifiers[3].transform.rotation).transform.parent = other.transform;
                    }
                }
            }
        }    
    }
}
