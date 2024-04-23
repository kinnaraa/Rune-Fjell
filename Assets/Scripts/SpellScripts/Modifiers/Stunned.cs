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
        else if(gameObject.GetComponentInParent<WrymBehavior>().enabled)
        {
            gameObject.GetComponentInParent<WrymBehavior>().enabled = false;
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
        else if(gameObject.GetComponentInParent<WrymBehavior>())
        {
            gameObject.GetComponentInParent<WrymBehavior>().enabled = true;
        }
        Destroy(gameObject);
    }
}
