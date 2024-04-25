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
        if(!other.CompareTag("Environment"))
        {
            foreach (Ability a in PM.allAbilities)
            {
                string newName = gameObject.transform.parent.name.Replace("(Clone)", "");
                if (a.Name == newName)
                {
                    if (GameObject.Find("Wyrm") || GameObject.Find("Wyrm(Clone)"))
                    {
                        GameObject wyrm = gameObject;
                        if(GameObject.Find("Wyrm"))
                        {
                            wyrm = GameObject.Find("Wyrm");
                        }
                        else if(GameObject.Find("Wyrm(Clone)"))
                        {
                        wyrm = GameObject.Find("Wyrm(Clone)");
                        }

                        WyrmHealth wyrmHealth = wyrm.GetComponent<WyrmHealth>();
                        wyrmHealth.currentHealth -= a.damage * PM.damageModifier;
                        
                        if (!wyrmHealth.CheckIfRed() && wyrmHealth.flashingCoroutine == null)
                        {
                            wyrmHealth.flashingCoroutine = StartCoroutine(wyrmHealth.FlashRed(() =>
                            {
                                // This is the callback function, it will be invoked when the coroutine ends
                                wyrmHealth.flashingCoroutine = null; // Reset the coroutine state
                            }));
                        }
                    }
                    else if (other.GetComponent<EnemyHealth>())
                    {
                        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
                        enemyHealth.currentHealth -= a.damage * PM.damageModifier;
                        
                        if (!enemyHealth.CheckIfRed() && enemyHealth.flashingCoroutine == null)
                        {
                            enemyHealth.flashingCoroutine = StartCoroutine(enemyHealth.FlashRed(() =>
                            {
                                // This is the callback function, it will be invoked when the coroutine ends
                                enemyHealth.flashingCoroutine = null; // Reset the coroutine state
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
}
