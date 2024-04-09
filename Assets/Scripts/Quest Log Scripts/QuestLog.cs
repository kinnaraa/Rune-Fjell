using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class QuestLog : MonoBehaviour
{
    public GameObject questContent;
    public QuestManager questManager;

    public class Quest
    {
        public string title;
        public KeyValuePair<Sprite, int> rewardPairs;
        public string infoText;
        public bool isActive = false;
        public GameObject questObject;

        public Quest(string questTitle, string questInfoText, KeyValuePair<Sprite, int> questRewards)
        {
            title = questTitle;
            infoText = questInfoText;
            rewardPairs = questRewards;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
