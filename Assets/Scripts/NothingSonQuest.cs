using System.Collections;
using UnityEngine;

public class NothingSonQuest : MonoBehaviour
{
    public GameManager GM;
    public GameObject Berry;
    public GameObject Wood;
    public GameObject Player;
    public GameObject Gnome;
    //public Transform[] FirstSetOfPoints;
    private bool questStarted = false;
    //private bool OnFirstPath = false;
    //private GameObject CurrentWisp;
    private int index;
    private int numBerry;
    private int numWood;
    
    public QuestLog questLog;

    public void Start()
    {
        questStarted = true;
        index = 0;
        numBerry = 3;
        numWood = 4;
        Debug.Log("Quest started is: " + questStarted);
    }

    public void Update()
    {
        if (questStarted)
        {
            //float distanceBerry = Vector3.Distance(Player.transform.position, Berry.transform.position);
            //float distanceWood = Vector3.Distance(Player.transform.position, Wood.transform.position);

            float distanceWood = Vector3.Distance(Player.transform.position, Wood.transform.position);

            if (distanceWood <= 4)
            {
                //Debug.Log("Within DIstance");
                if (Input.GetKey("E"))
                {
                    /*Destroy(Wood);
                    Debug.Log("wood collect");
                    numWood--;
                    Debug.Log("num of wood: " + numWood);*/
                }
                
            }

            if (numWood == 0 && numBerry == 0)
            {

            }
        }

    }
    public void StartQuest()
    {
        questStarted = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Berry")
        {
            Debug.Log("berry collect");
            numBerry--;
            Debug.Log("num of berry: " + numBerry);
        }
        if(other.tag == "Wood")
        {
            Debug.Log("wood collect");
            numWood--;
            Debug.Log("num of wood: " + numWood);
        }
    }


    /*
    public void Start()
    {
        index = 0;
    }

    public void Update()
    {
        if (firstPath)
        {
            CurrentWisp = Instantiate(Wisp, FirstSetOfPoints[index].position, FirstSetOfPoints[index].rotation);
            firstPath = false;
            OnFirstPath = true;
        }

        if (OnFirstPath)
        {
            float distance = Vector3.Distance(Player.transform.position, CurrentWisp.transform.position);
            if (distance <= 4)
            {
                Destroy(CurrentWisp);
                index++;

                if (index < FirstSetOfPoints.Length)
                {
                    CurrentWisp = Instantiate(Wisp, FirstSetOfPoints[index].position, FirstSetOfPoints[index].rotation);
                }
                else
                {
                    OnFirstPath = false;

                    Debug.Log("start new Quest");
                    questLog.allQuests[0].completed = true;
                    questLog.allQuests[1].isActive = true;
                    questLog.allQuests[1].currentQuest = true;

                    StartCoroutine(GM.SaveTheGnome());
                }
            }
        }
    }
    public void StartFirstPath()
    {
        firstPath = true;
    }
    */

}
