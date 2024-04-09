using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InTheDeepQuest : MonoBehaviour
{
    //public GameManager GM;
    public QuestManager questManager;

    public GameObject player;
    public GameObject wyrm;
    
    public Transform caveEntrance;
    public Transform crowdOfGnomes;

    public bool deepQuestStart;
    //public bool questStarted;
    //public 

    // Start is called before the first frame update
    void Start()
    {
        deepQuestStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceCrowdOfGnomes = Vector3.Distance(player.transform.position, crowdOfGnomes.transform.position);

        if (distanceCrowdOfGnomes < 3 && Input.GetKeyDown(KeyCode.E))
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
