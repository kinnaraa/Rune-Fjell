using System.Collections;
using UnityEngine;

public class NothingSonQuest : MonoBehaviour
{
    public GameManager GM;
    public GameObject Berry;
    public GameObject Wood;
    public GameObject Player;
    public GameObject NothingSonGnome;
    public GameObject GnomeHouse;
    //public Transform[] LocationsOfItems;
    public bool questStarted;
    public int numBerry;
    public int numWood;
    public bool itemsCollected;
    
    public QuestLog questLog;

    public void Start()
    {
        //questStarted = true;
        questStarted = false;
        itemsCollected = false;
        numBerry = 2;
        numWood = 3;
        Debug.Log("Quest started is: " + questStarted);
        //Update Quest Log (start)
    }

    public void Update()
    {
        float distanceGnomeHouse = Vector3.Distance(Player.transform.position, GnomeHouse.transform.position);
        float distanceGnome = Vector3.Distance(Player.transform.position, NothingSonGnome.transform.position);

        if ( distanceGnomeHouse < 2 && Input.GetKeyDown(KeyCode.E))
        {
            questStarted = true;
            //Gnome Dialogue with Player
            Debug.Log("Talking to Mom Gnome");
            TalkToGnome();
        }

        if (questStarted)
        {
            //float distanceBerry = Vector3.Distance(Player.transform.position, Berry.transform.position);
            //float distanceWood = Vector3.Distance(Player.transform.position, Wood.transform.position);

            if (numWood == 0 && numBerry == 0)
            {
                itemsCollected = true;
                Debug.Log("Quest nearly complete");
                //Update Quest Log (done)
            }

            if (itemsCollected && (distanceGnome < 2))
            {
                Debug.Log("Quest Complete!");
            }
        }

    }

    //Starting Quest
    public void StartQuest()
    {
        questStarted = true;
    }

    // Logic for Gnome Dialogue
    void TalkToGnome()
    {

    }

/*    public void OnTriggerEnter(Collider other)
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
*/

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
