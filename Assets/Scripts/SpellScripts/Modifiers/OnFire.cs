using System.Collections;
using UnityEngine;

public class OnFire : MonoBehaviour
{
    private Coroutine flashingCoroutine;
    private void Start() 
    {
        StartCoroutine(FireDamage());
        Destroy(gameObject, 3f);
    }

    IEnumerator FireDamage()
    {
        for(int i = 0; i <= 5; i++)
        {
            if(gameObject.GetComponentInParent<EnemyHealth>())
            {
                gameObject.GetComponentInParent<EnemyHealth>().currentHealth -= 1;
                if (!gameObject.GetComponentInParent<EnemyHealth>().CheckIfRed() && flashingCoroutine == null)
                {      
                    flashingCoroutine = StartCoroutine(gameObject.GetComponentInParent<EnemyHealth>().FlashRed(() =>
                    {
                        // This is the callback function, it will be invoked when the coroutine ends
                        flashingCoroutine = null; // Reset the coroutine state
                    }));
                }
            }
            else
            {
                GameObject.Find("Wrym").GetComponent<WyrmHealth>().currentHealth -= 1;
                if (!GameObject.Find("Wrym").GetComponent<WyrmHealth>().CheckIfRed() && flashingCoroutine == null)
                {      
                    flashingCoroutine = StartCoroutine(GameObject.Find("Wrym").GetComponent<WyrmHealth>().FlashRed(() =>
                    {
                        // This is the callback function, it will be invoked when the coroutine ends
                        flashingCoroutine = null; // Reset the coroutine state
                    }));
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
