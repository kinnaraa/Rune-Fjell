using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    //public GameObject item;
    public Player player;
    public bool isGrabbable = false;
    // Start is called before the first frame update
    void Start()
    {
        //isGrabbable = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(gameObject.transform.position, player.transform.position);

        if (distance < 5)
        {
            isGrabbable = true;
        }

        if(isGrabbable && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(gameObject);
            Debug.Log("wood collect");
            //numWood--;
            //Debug.Log("num of wood: " + numWood);
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
