using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public GameObject questContent;
    public GameObject currentQuest;
    public Dictionary<string, QuestLog.Quest> allQuests = new Dictionary<string, QuestLog.Quest> { };

    public ThisGuyStinks TGS;
    public NothingSonQuest GFNS;

    private string TGSinfo;
    private string GFNSinfo;

    // Start is called before the first frame update
    void Start()
    {
        allQuests["Follow the Wisps"] = new QuestLog.Quest("Follow the Wisps", "The wisps have never led you wrong before.", new KeyValuePair<Sprite, int> { });
        allQuests["Help the Gnome"] = new QuestLog.Quest("Help the Gnome", "You come across a gnome in the woods crying for help. Help the gnome by slaying his attacker.", new KeyValuePair<Sprite, int>(Resources.Load<Sprite>("UI/Kenaz_Default"), 1));
        allQuests["Find the Gnome Village"] = new QuestLog.Quest("Find the Gnome Village", "Follow the gnome as he leads you to his village.", new KeyValuePair<Sprite, int>(Resources.Load<Sprite>("UI/skill_point_icon"), 2));
        allQuests["This Guy Stinks"] = new QuestLog.Quest("This Guy Stinks", "An unsual gnome in a smokey hut has some important info for you. But you must bring him what he wants to learn his secrets.\nMushrooms: " + TGS.mushroomCount + " / 10", new KeyValuePair<Sprite, int>(Resources.Load<Sprite>("UI/skill_point_icon"), 3));
        allQuests["Good For Nothing Son"] = new QuestLog.Quest("Good For Nothing Son", "The lost gnome's mother needs help getting supplies for dinner. Find her 3 wood and 1 berry.\nBerries: " + GFNS.numBerry + "/ 2\nWood: " + GFNS.numWood + " / 3", new KeyValuePair<Sprite, int>(Resources.Load<Sprite>("UI/skill_point_icon"), 2));
        allQuests["Where Art Gnome"] = new QuestLog.Quest("Where Art Gnome", "You hear loud commotion coming from the gnome town square.", new KeyValuePair<Sprite, int>(Resources.Load<Sprite>("UI/skill_point_icon"), 6));
        allQuests["Something Lurking"] = new QuestLog.Quest("Something Lurking in the Deep", "He?s heard legends of a great worm who would eventually awake to destroy gnomekind. Destroy him before he kills us all.", new KeyValuePair<Sprite, int>(Resources.Load<Sprite>("UI/skill_point_icon"), 3));

        allQuests["Follow the Wisps"].questObject = questContent.transform.GetChild(0).gameObject;
        allQuests["Help the Gnome"].questObject = questContent.transform.GetChild(1).gameObject;
        allQuests["Find the Gnome Village"].questObject = questContent.transform.GetChild(2).gameObject;
        allQuests["Good For Nothing Son"].questObject = questContent.transform.GetChild(3).gameObject;
        allQuests["This Guy Stinks"].questObject = questContent.transform.GetChild(4).gameObject;
        allQuests["Where Art Gnome"].questObject = questContent.transform.GetChild(5).gameObject;
        allQuests["Something Lurking"].questObject = questContent.transform.GetChild(6).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        TGSinfo = "An unsual gnome in a smokey hut has some important info for you. But you must bring him what he wants to learn his secrets.\nMushrooms: " + TGS.mushroomCount + " / 10";
        GFNSinfo = "The lost gnome's mother needs help getting supplies for dinner. Find her 3 wood and 1 berry.\nBerries: " + GFNS.numBerry + "/ 2\nWood: " + GFNS.numWood + " / 3";

        allQuests["This Guy Stinks"].infoText = TGSinfo;
        allQuests["Good For Nothing Son"].infoText = GFNSinfo;

        foreach (var q in allQuests)
        {
            if (q.Key == "Follow the Wisps")
            {
                q.Value.questObject.transform.GetChild(3).GetComponent<Image>().sprite = q.Value.rewardPairs.Key;
                q.Value.questObject.transform.GetChild(3).gameObject.SetActive(false);
                q.Value.questObject.transform.GetChild(5).gameObject.SetActive(false);
            }

            q.Value.questObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = q.Value.title;
            q.Value.questObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = q.Value.infoText;
            q.Value.questObject.transform.GetChild(3).GetComponent<Image>().sprite = q.Value.rewardPairs.Key;
            q.Value.questObject.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = q.Value.rewardPairs.Value.ToString();

            if (q.Value.isActive)
            {
                q.Value.questObject.SetActive(true);
                currentQuest.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = q.Value.title;
                currentQuest.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = q.Value.infoText;
            }
            else
            {
                q.Value.questObject.SetActive(false);
            }
        }
    }
}
