using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InTheDeepQuest : MonoBehaviour
{
    //public GameManager GM;
    public QuestManager questManager;

    public GameObject player;
    public GameObject wyrm;
    public GameObject gnomeMayor;
    
    public Transform caveEntrance;

    public bool deepQuestStart;

    // Start is called before the first frame update
    void Start()
    {
        deepQuestStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceGnomesMayor = Vector3.Distance(player.transform.position, gnomeMayor.transform.position);

        if (distanceGnomesMayor < 3 && Input.GetKeyDown(KeyCode.E))
        {
            deepQuestStart = true;
            questManager.allQuests["In The Deep"].isActive = true;
            //Gnome Dialogue with Player
            TalkToCrowd();
            Debug.Log("Talking to Gnome Crowd");
        }

        if (deepQuestStart)
        {
            float distanceToCaveEntrance = Vector3.Distance(player.transform.position, caveEntrance.transform.position);

            if (distanceToCaveEntrance < 1)
            {
                Debug.Log("Entering Cave");
            }
        }
    }

    public void TalkToCrowd()
    {

    }
}
