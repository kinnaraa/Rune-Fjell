using System.Collections;
using UnityEngine;

public class PortalAnimationForTeleport : MonoBehaviour
{
    private GameObject player;

    public void Animate(GameObject obj)
    {
        StartCoroutine(Play(obj));
    }

    public IEnumerator Play(GameObject obj)
    {
        player = GameObject.Find("Player"); // Get the player GameObject reference every time the coroutine is called

        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + Vector3.up * 1.8f;

        while (elapsedTime < 3f)
        {
            float t = elapsedTime / 3f;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the object ends up exactly at the target position
        transform.position = targetPosition;
        player.transform.position = obj.transform.GetChild(0).position;
        Destroy(gameObject);
    }
}
