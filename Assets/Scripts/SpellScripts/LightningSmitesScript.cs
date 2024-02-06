using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSmitesScript : MonoBehaviour
{
    public Transform[] airs;
    public Transform[] grounds;

    public GameObject lightning;
    public bool firing;

    void Update()
    {
        if(firing)
        {
            StartCoroutine(Smiting());
        }
    }

    public IEnumerator Smiting()
    {
        firing = false;
        lightning = Resources.Load("SpellPrefabs/Lightning") as GameObject;

        for(int i = 0; i < airs.Length; i++)
        {
            int randomGround = Random.Range(0, grounds.Length);
            int randomAir = Random.Range(0, airs.Length);

            GameObject Lightning = Instantiate(lightning, lightning.transform.position, lightning.transform.rotation); 
            StartCoroutine(ExtendLightning(airs[randomAir].position, grounds[randomGround].position, Lightning.GetComponent<LineRenderer>(), 15, true));

            yield return new WaitForSeconds(0.2f);
        }
    }

    public IEnumerator ExtendLightning(Vector3 startPosition, Vector3 targetPosition, LineRenderer Line, float speed, bool killSmite)
    {
        float elapsedTime = 0f;

        while (elapsedTime < 0.5f)
        {
            // Interpolate between the start and target positions
            Vector3 currentPos = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
            
            // Update the line renderer positions
            Line.SetPosition(0, startPosition);
            Line.SetPosition(1, currentPos);

            // Increment elapsed time based on time.deltaTime and draw speed
            elapsedTime += Time.deltaTime * speed;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final position is set correctly
        Line.SetPosition(0, startPosition);
        Line.SetPosition(1, targetPosition);

        if(killSmite)
        {
            Destroy(Line.gameObject, 1f);
        }
    }
}
