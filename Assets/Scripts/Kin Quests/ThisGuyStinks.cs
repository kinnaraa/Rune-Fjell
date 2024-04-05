using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisGuyStinks : MonoBehaviour
{
    public int mushroomCount = 0;
    public bool collectedShrooms = false;
    public GameObject Player;
    public bool canCollect = false;
    public bool knocked = false;
    public GameObject gnomeHouse;
    public GameObject weedGnome;
    private Transform initialGnomeLocation;

    // Start is called before the first frame update
    void Start()
    {
        initialGnomeLocation = weedGnome.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(Player.transform.position, gnomeHouse.transform.position) < 5)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("moving weed gnome");
                weedGnome.transform.position = new Vector3(-194.17f, 24.80045f, -59.43f); // rotation to 48.562
                knocked = true;
            }
            
            if (Input.GetKeyDown(KeyCode.E) && knocked && !canCollect)
            {
                talkToWeedGnome1();
                Debug.Log("talking to gnome");
                // update quest log and active quest info
                weedGnome.transform.position = initialGnomeLocation.position;
                canCollect = true;
            }
        }

        if(mushroomCount >= 10)
        {
            collectedShrooms = true;
        }

        if (collectedShrooms)
        {
            //update quest log info and active quest info to tell you to go back to weed gnome
        }

        if(collectedShrooms && Vector3.Distance(Player.transform.position, gnomeHouse.transform.position) < 1.5)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                talkToWeedGnome2();
            }
        }
    }

    void talkToWeedGnome1()
    {
        // dialogue telling you he has helpful secret knowledge but only if you collect him 10 mushrooms
    }

    void talkToWeedGnome2()
    {
        // dialogue telling you thank you and then explains bind runes to you
    }
}
