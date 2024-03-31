using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    Hashtable playerInventory = new Hashtable();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Adding to Inventory!");
        if (other.gameObject.tag == "Berry")
        {
            if (!playerInventory.ContainsKey("Berry"))
            {
                playerInventory.Add("Berry", (int)1);
                Debug.Log("Added Berry!");
            }
            else
            {
                //int numOfBerries = playerInventory["Berry"].Value;
                //playerInventory["Berry"] = numOfBerries++;
            }
            //Destroy(item.gameObject);
        }
        if (other.gameObject.tag == "Wood")
        {
            if (!playerInventory.ContainsKey("Wood"))
            {
                playerInventory.Add("Wood", (int)1);
                Debug.Log("Added Wood!");
            }
            else
            {
                //int numOfBerries = playerInventory["Berry"].Value;
                //playerInventory["Berry"] = numOfBerries++;
            }
        }
    }
}
