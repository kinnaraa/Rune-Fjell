using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject playerModel;
    public BoxCollider areaVisible;

    public float speedOfEnemy = 5;

    bool playerInArea;
    // Start is called before the first frame update
    void Start()
    {
        playerModel = GameObject.FindGameObjectWithTag("Player");
        playerInArea = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        
        if (playerInArea)
        {
            AttackPlayer();
        }
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        //BoxCollider enemyArea = collision.gameObject.GetComponent<BoxCollider>();
        if (collision.gameObject == playerModel)
        {
            playerInArea = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        //BoxCollider enemyArea = collision.gameObject.GetComponent<BoxCollider>();
        if (collision.gameObject == playerModel)
        {
            playerInArea = false;
        }
    }

    public void AttackPlayer()
    {
        //calculate movement dist
        var step = speedOfEnemy * Time.deltaTime;

        //basic move towards script
        transform.position = Vector3.MoveTowards(transform.position, playerModel.transform.position, step);
    }
}
