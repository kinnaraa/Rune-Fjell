using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DealDamageAndModifiers : MonoBehaviour
{
    private PlayerMagic PM;
    private List<GameObject> Modifiers = new List<GameObject>();
    private Coroutine flashingCoroutine;

    public void Start() 
    {
        PM = GameObject.Find("Player").GetComponent<PlayerMagic>();
        Modifiers.Add(Resources.Load<GameObject>("Modifiers/OnFire"));
        Modifiers.Add(Resources.Load<GameObject>("Modifiers/Cold"));
        Modifiers.Add(Resources.Load<GameObject>("Modifiers/Stunned"));
    }
    public void OnTriggerEnter(Collider other) 
    {   
        if(other.GetComponent<EnemyHealth>() || GameObject.Find("Wyrm").GetComponent<WyrmHealth>())
        {
            foreach(Ability a in PM.allAbilities)
            {
                if(GameObject.Find("Wyrm").GetComponent<WyrmHealth>())
                {
                    GameObject.Find("Wyrm").GetComponent<WyrmHealth>().currentHealth -= a.damage * PM.damageModifier;
                    if (!GameObject.Find("Wyrm").GetComponent<WyrmHealth>().CheckIfRed() && flashingCoroutine == null)
                    {      
                        flashingCoroutine = StartCoroutine(GameObject.Find("Wyrm").GetComponent<WyrmHealth>().FlashRed(() =>
                        {
                            // This is the callback function, it will be invoked when the coroutine ends
                            flashingCoroutine = null; // Reset the coroutine state
                        }));
                    }
                }
                else
                {
                    other.GetComponent<EnemyHealth>().currentHealth -= a.damage * PM.damageModifier;
                    if (!other.GetComponent<EnemyHealth>().CheckIfRed() && flashingCoroutine == null)
                    {      
                        flashingCoroutine = StartCoroutine(other.GetComponent<EnemyHealth>().FlashRed(() =>
                        {
                            // This is the callback function, it will be invoked when the coroutine ends
                            flashingCoroutine = null; // Reset the coroutine state
                        }));
                    }
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
