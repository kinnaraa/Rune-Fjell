using System.Collections;
using UnityEngine;

public class Cold : MonoBehaviour
{
    private void Start() 
    {
        gameObject.GetComponent<EnemyMovement>().moveSpeed /= 2;
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        gameObject.GetComponent<EnemyMovement>().moveSpeed *= 2;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
