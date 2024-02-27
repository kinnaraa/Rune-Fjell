using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject playerModel;

    public BoxCollider areaVisible;
    public float attackRange;
    float distanceToPlayer;
    //public BoxCollider attackRange;

    public GameObject enemyHitbox;
    //public BoxCollider hitboxCollider = GetComponent<Collider>();

    public float speedOfEnemy = 5;

    bool playerInArea;
    bool chasePlayer;
    // Start is called before the first frame update
    void Start()
    {
        playerModel = GameObject.FindGameObjectWithTag("Player");
        playerInArea = false;
        chasePlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(playerModel.transform.position, gameObject.transform.position);
        Debug.Log("distance vector: " + distanceToPlayer);

        if (playerInArea && chasePlayer)
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
            chasePlayer = true;
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


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == enemyHitbox)
        {
            chasePlayer = false;
        }
        else if((collision.gameObject == playerModel) && (distanceToPlayer <= attackRange))
        {
            Debug.Log("Collision Enter Pass");

            chasePlayer = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == enemyHitbox)
        {
            chasePlayer = true;
        }
        else if (collision.gameObject == playerModel)
        {
            chasePlayer = true;
        }
    }
    
    
/*    private void OnCollisionStay(Collision collision)
    {
        *//*
        if (distanceToPlayer <= attackRange)
        {
            chasePlayer = false;
        }
        *//*
    }*/
    

    public void AttackPlayer()
    {
        //calculate movement dist
        var step = speedOfEnemy * Time.deltaTime;

        //basic move towards script
        transform.position = Vector3.MoveTowards(transform.position, playerModel.transform.position, step);
    }
}
