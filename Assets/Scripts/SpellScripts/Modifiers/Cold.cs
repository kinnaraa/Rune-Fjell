using System.Collections;
using UnityEngine;

public class Cold : MonoBehaviour
{
    private void Start() 
    {
        if(gameObject.GetComponentInParent<EnemyMovement>().moveSpeed > 0.5)
        {
            gameObject.GetComponentInParent<EnemyMovement>().moveSpeed /= 2;
            StartCoroutine(Reset());
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(3f);
        gameObject.GetComponentInParent<EnemyMovement>().moveSpeed *= 2;
        Destroy(gameObject);
    }
}
