using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisGuyStinks : MonoBehaviour
{
    public int mushroomCount = 0;
    public bool collectedShrooms = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("mushrooms: " + mushroomCount);
        if(mushroomCount >= 10)
        {
            collectedShrooms = true;
        }

        if (collectedShrooms)
        {

        }
    }
}
