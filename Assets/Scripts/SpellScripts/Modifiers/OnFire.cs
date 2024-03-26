using System.Collections;
using UnityEngine;

public class OnFire : MonoBehaviour
{
    private void Start() 
    {
        StartCoroutine(FireDamage());
        Destroy(gameObject, 3f);
    }

    IEnumerator FireDamage()
    {
        for(int i = 0; i <= 5; i++)
        {
            Debug.Log("Fire Damage");
            gameObject.GetComponentInParent<EnemyHealth>().currentHealth -= 1;
            if( gameObject.GetComponentInParent<EnemyHealth>().checkIfRed())
            {
                StartCoroutine(gameObject.GetComponentInParent<EnemyHealth>().FlashRed());
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
