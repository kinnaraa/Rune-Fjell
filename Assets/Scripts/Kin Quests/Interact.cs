using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public bool canPickup = false;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(gameObject.transform.position, player.transform.position) < 1.5)
        {
            canPickup = true;
            Debug.Log(canPickup);
        }

        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(gameObject);
        }
    }
}
