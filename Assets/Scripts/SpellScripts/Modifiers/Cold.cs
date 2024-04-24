using System.Collections;
using UnityEngine;

public class Cold : MonoBehaviour
{
    private void Start() 
    {
        if(gameObject.GetComponentInParent<EnemyMovement>())
        {
            if(gameObject.GetComponentInParent<EnemyMovement>().moveSpeed > 0.5)
            {
                gameObject.GetComponentInParent<EnemyMovement>().moveSpeed /= 2;
                StartCoroutine(Reset());
            }
            else
            {
                Destroy(gameObject);
            }
        }   
        else if (gameObject.GetComponentInParent<PlayerMovement>())
        {
            if(gameObject.GetComponentInParent<PlayerMovement>().moveSpeed > 0.5)
            {
                gameObject.GetComponentInParent<PlayerMovement>().moveSpeed /= 2;
                StartCoroutine(Reset());
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else if (GameObject.Find("Wrym"))
        {
            if(GameObject.Find("Wrym").GetComponent<WrymBehavior>().moveSpeed > 0.5)
            {
                GameObject.Find("Wrym").GetComponent<WrymBehavior>().moveSpeed /= 2;
                StartCoroutine(Reset());
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(3f);
        if(gameObject.GetComponentInParent<EnemyMovement>())
        {
            gameObject.GetComponentInParent<EnemyMovement>().moveSpeed *= 2;
        }
        else if(GameObject.Find("Wrym").GetComponent<WrymBehavior>())
        {
            GameObject.Find("Wrym").GetComponent<WrymBehavior>().moveSpeed *= 2;
        }
        else
        {
            gameObject.GetComponentInParent<PlayerMovement>().moveSpeed *= 2;
        }
        Destroy(gameObject);
    }
}
