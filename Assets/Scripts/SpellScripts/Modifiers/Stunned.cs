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
        else if(gameObject.GetComponentInParnet<WyrmBehavior>().enabled)
        {
            gameObject.GetComponentInParent<WyrmBehavior>().enabled = false;
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
        if(gameObject.GetComponentInParent<EnemyMovement>())
        {
            gameObject.GetComponentInParent<EnemyMovement>().enabled = true;
        }
        else if(gameObject.GetComponentInParent<WyrmBehavior>())
        {
            gameObject.GetComponentInParent<WyrmBehavior>().enabled = true;
        }
        Destroy(gameObject);
    }
}
