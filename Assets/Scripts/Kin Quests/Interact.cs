using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public bool canPickup = false;
    public Player player;
    public ThisGuyStinks thisGuyStinks;
    private GameObject EtoInteract;

    // Start is called before the first frame update
    void Start()
    {
        EtoInteract = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(gameObject.transform.position, player.transform.position) < 1.5 && thisGuyStinks.canCollect)
        {
            canPickup = true;
            EtoInteract.SetActive(true);
        }
        else
        {
            canPickup = false;
            EtoInteract.SetActive(false);
        }

        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            thisGuyStinks.mushroomCount++;
            Destroy(gameObject);
        }
    }
}
