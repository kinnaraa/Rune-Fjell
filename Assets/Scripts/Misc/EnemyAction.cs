using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject playerModel;

    public BoxCollider areaVisible;
    public float distanceToPlayer;
    //public BoxCollider attackRange;

    public BoxCollider enemyHitbox;

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
        if((collision.gameObject == playerModel) && (Vector3.Distance(playerModel.transform.position, gameObject.transform.position) <= distanceToPlayer))
        {
            chasePlayer = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == playerModel)
        {
            chasePlayer = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Vector3.Distance(playerModel.transform.position, gameObject.transform.position) <= distanceToPlayer)
        {
            chasePlayer = false;
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
