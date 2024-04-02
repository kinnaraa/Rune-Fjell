using System.Collections;
using UnityEngine;

public class Expand : MonoBehaviour
{
    public float growthRate = 0.1f; // Rate at which the object grows per half second
    public float halfSecond = 0.5f; // Half second interval

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExpandObject());
    }

    IEnumerator ExpandObject()
    {
        while (true)
        {
            // Get the current scale of the object
            Vector3 currentScale = transform.localScale;
            
            // Increase the scale by the growth rate
            Vector3 newScale = currentScale + new Vector3(growthRate, growthRate, growthRate);
            
            // Apply the new scale to the object
            transform.localScale = newScale;
            
            // Wait for half a second
            yield return new WaitForSeconds(halfSecond);
        }
    }
}
