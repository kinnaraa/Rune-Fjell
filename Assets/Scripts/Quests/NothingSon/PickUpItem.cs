using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    //public GameObject item;
    public GameObject player;
    public bool isGrabbable;
    public NothingSonQuest nothingQuest;
    // Start is called before the first frame update
    void Start()
    {
        isGrabbable = false;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(gameObject.transform.position, player.transform.position);

        if (distance < 2 && nothingQuest.questStarted)
        {
            isGrabbable = true;
        }

        if(isGrabbable && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("pressing E");

            if(gameObject.name == "Wood")
            {
                if(nothingQuest.numWood < 3)
                {
                    nothingQuest.numWood++;
                }
                Debug.Log("wood collected: " + nothingQuest.numWood);
                Destroy(gameObject);
            }
            if (gameObject.name == "Berry")
            {
                if(nothingQuest.numBerry < 2)
                {
                    nothingQuest.numBerry++;
                }
                Debug.Log("berry collected: " + nothingQuest.numBerry);
                Destroy(gameObject);
            }
            
        }
    }

/*    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colliding with object!");
        if (other.gameObject.tag == "Player")
        {
            Destroy(item.gameObject);
        }
    }*/
}
