using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class QuestLog : MonoBehaviour
{
    public List<Quest> allQuests = new List<Quest>();
    public List<Quest> activeQuests = new List<Quest>();
    public List<Quest> completedQuests = new List<Quest>();
    public GameObject currentQuest;
    public GameObject questContent;

    public class Quest
    {
        public string title;
        public KeyValuePair<Sprite, int> rewardPairs;
        public int numNeeds;
        public string infoText;
        public bool isActive = false;
        public bool completed = false;
        public GameObject questObject;
        public bool currentQuest;

        public Quest(string questTitle, int needs, string questInfoText, KeyValuePair<Sprite, int> questRewards)
        {
            title = questTitle;
            infoText = questInfoText;
            numNeeds = needs;
            rewardPairs = questRewards;
            currentQuest = false;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        allQuests = new List<Quest>()
        {
            new Quest("Follow the Wisps", 1, "The wisps have never led you wrong before.", new KeyValuePair<Sprite, int>{}),
            new Quest("Help the Gnome", 1, "You come across a gnome in the woods crying for help. Help the gnome by slaying his attacker.",  new KeyValuePair<Sprite, int>(Resources.Load<Sprite>("UI/Kenaz_Default"), 1)),
            new Quest("Find the Gnome Village", 1, "find gnome village info, blah blah, blah", new KeyValuePair<Sprite, int>(Resources.Load<Sprite>("UI/socket"), 1)),
            /*
            new Quest("Good For Nothing Son", 7, "Gnome's mom needs help getting supplies for dinner", stupidSon),
            new Quest("Pesky Wolves", 5, "Gnome is grumpy about the loud wolves in the area and offered you a reward to get rid of them", peskyWolves),
            new Quest("This Guy Stinks", 10, "Find gnome in town who needs help in weird smokey hut", thisGuyStinks),
            new Quest("Cracking the Code", 1, "The seer offered you to try a mushroom, will you accept?", crackingCode),
            new Quest("Where Art Gnome", 3, "Several Gnomes have gone missing in the night!", whereArtGnome),
            new Quest("Unknown", 1, "Make your way towards the mountains to try and find the beast who stole the gnomes", unknownQuest),
            new Quest("Something Lurking in the Deep", 1, "He�s heard legends of a great worm who would eventually awake to destroy gnomekind. Destroy him before he kills us all.", somethingLurking),
            */
        };

        Debug.Log(allQuests.Count());

        for (int i = 0; i < allQuests.Count; i++)
        {
            allQuests[i].questObject = questContent.transform.GetChild(i).gameObject;
            questContent.transform.GetChild(i).GetChild(0).gameObject.name = allQuests[i].title;
            allQuests[i].questObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = allQuests[i].title;
            allQuests[i].questObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = allQuests[i].infoText;

            if (allQuests[i].rewardPairs.Key != null)
            {
                allQuests[i].questObject.transform.GetChild(3).gameObject.SetActive(true);
                allQuests[i].questObject.transform.GetChild(5).gameObject.SetActive(true);
                Debug.Log(allQuests[i].rewardPairs.Key);
                allQuests[i].questObject.transform.GetChild(3).GetComponent<Image>().sprite = allQuests[i].rewardPairs.Key;
                allQuests[i].questObject.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "x" + allQuests[i].rewardPairs.Value;
            }
            else
            {
                allQuests[i].questObject.transform.GetChild(3).gameObject.SetActive(false);
                allQuests[i].questObject.transform.GetChild(5).gameObject.SetActive(false);
            }
        }

        allQuests[0].isActive = true;

        activeQuests = new List<Quest>();

        currentQuest.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < allQuests.Count(); i++)
        {
            /*if (allQuests[i].isActive && !activeQuests.Contains(allQuests[i]))
            {
                activeQuests.Add(allQuests[i]);
                allQuests[i].questObject.SetActive(true);
            }
            else if (!allQuests[i].isActive && activeQuests.Contains(allQuests[i])) 
            {
                activeQuests.Remove(allQuests[i]);
                allQuests[i].questObject.SetActive(false);
            }else if (!allQuests[i].isActive)
            {
                allQuests[i].questObject.SetActive(false);
            }*/

            if (allQuests[i].completed)
            {
                allQuests[i].isActive = false;
                allQuests[i].currentQuest = false;
                allQuests[i].questObject.SetActive(false);
            }

            if (allQuests[i].isActive)
            {
                allQuests[i].questObject.SetActive(true);
            }

            if (allQuests[i].currentQuest)
            {
                currentQuest.SetActive(true);
                currentQuest.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = allQuests[i].title;
                currentQuest.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = allQuests[i].infoText;
            }
        }
    }
}
