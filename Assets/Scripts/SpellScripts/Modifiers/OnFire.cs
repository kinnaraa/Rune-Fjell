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
            gameObject.GetComponentInParent<EnemyHealth>().currentHealth -= 1;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
