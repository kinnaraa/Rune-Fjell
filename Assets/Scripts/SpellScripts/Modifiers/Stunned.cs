using System.Collections;
using UnityEngine;

public class Stunned : MonoBehaviour
{
    private void Start() 
    {
        if(gameObject.GetComponentInParent<EnemyMovement>().enabled)
        {
            gameObject.GetComponentInParent<EnemyMovement>().enabled = false;
            StartCoroutine(Reset());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(3f);
        gameObject.GetComponentInParent<EnemyMovement>().enabled = true;
        Destroy(gameObject);
    }
}
