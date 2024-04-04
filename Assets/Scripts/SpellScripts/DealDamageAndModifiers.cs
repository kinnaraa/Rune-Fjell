using System.Collections.Generic;
using UnityEngine;

public class DealDamageAndModifiers : MonoBehaviour
{
    private PlayerMagic PM;
    private List<GameObject> Modifiers = new List<GameObject>();

    public void Start() 
    {
        PM = GameObject.Find("Player").GetComponent<PlayerMagic>();
        Modifiers.Add(Resources.Load<GameObject>("Modifiers/OnFire"));
        Modifiers.Add(Resources.Load<GameObject>("Modifiers/Cold"));
        Modifiers.Add(Resources.Load<GameObject>("Modifiers/Stunned"));
    }
    public void OnTriggerEnter(Collider other) 
    {   
        if(other.GetComponent<EnemyHealth>())
        {
            foreach(Ability a in PM.allAbilities)
            {
                string newName = gameObject.transform.parent.name.Replace("(Clone)", "");
                if(a.Name == newName)
                {
                    if(other.name.Contains("Wrym"))
                    {
                        other.GetComponent<WyrmHealth>().currentHealth -= a.damage * PM.damageModifier;
                        StartCoroutine(other.GetComponent<WyrmHealth>().FlashRed());
                    }
                    else
                    {
                        other.GetComponent<EnemyHealth>().currentHealth -= a.damage * PM.damageModifier;
                        StartCoroutine(other.GetComponent<EnemyHealth>().FlashRed());
                    }
            

                    if(a.Modifier == 0)
                    {
                        return;
                    }
                    else if(a.Modifier == 1)
                    {
                        Instantiate(Modifiers[0], other.transform.position, Modifiers[0].transform.rotation).transform.parent = other.transform;
                    }
                    else if(a.Modifier == 2)
                    {
                        Instantiate(Modifiers[1], other.transform.position, Modifiers[1].transform.rotation).transform.parent = other.transform;
                    }
                    else if(a.Modifier == 3)
                    {
                        Instantiate(Modifiers[2], other.transform.position, Modifiers[2].transform.rotation).transform.parent = other.transform;
                    }
                }
            }
        }    
    }
}
