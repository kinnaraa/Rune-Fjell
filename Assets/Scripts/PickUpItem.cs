using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public GameObject item;
    public Player player;
    public bool isGrabbable;
    // Start is called before the first frame update
    void Start()
    {
        isGrabbable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colliding with object!");
        if (other.gameObject.tag == "Player")
        {
            Destroy(item.gameObject);
        }
    }
}
