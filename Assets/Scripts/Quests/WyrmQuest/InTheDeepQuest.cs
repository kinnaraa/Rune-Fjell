using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InTheDeepQuest : MonoBehaviour
{
    public QuestManager questManager;

    public GameObject player;
    
    public Transform caveEntrance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (questManager.allQuests["Something Lurking"].isActive)
        {
            float distanceToCaveEntrance = Vector3.Distance(player.transform.position, caveEntrance.transform.position);

            if (distanceToCaveEntrance < 2.0f)
            {
                Debug.Log("Entering Cave");
            }
        }
    }
}
