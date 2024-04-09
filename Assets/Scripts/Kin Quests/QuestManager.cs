using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public QuestLog questLog;

    // Start is called before the first frame update
    void Start()
    {
        questLog.allQuests["Follow the Wisps"] = new QuestLog.Quest("Follow the Wisps", "The wisps have never led you wrong before.", new KeyValuePair<Sprite, int> { });
        questLog.allQuests["Help the Gnome"] = new QuestLog.Quest("Help the Gnome", "You come across a gnome in the woods crying for help. Help the gnome by slaying his attacker.", new KeyValuePair<Sprite, int>(Resources.Load<Sprite>("UI/Kenaz_Default"), 1));
        questLog.allQuests["Find the Gnome Village"] = new QuestLog.Quest("Find the Gnome Village", "find gnome village info, blah blah, blah", new KeyValuePair<Sprite, int>(Resources.Load<Sprite>("UI/socket"), 1));
        questLog.allQuests["This Guy Stinks"] = new QuestLog.Quest("This Guy Stinks", "An unsual gnome in town has some important info for you. But you must bring him what he wants to learn his secrets.", new KeyValuePair<Sprite, int>(Resources.Load<Sprite>("UI/socket"), 3));
        questLog.allQuests["Good For Nothing Son"] = new QuestLog.Quest("Good For Nothing Son", "Gnome's mom needs help getting supplies for dinner", new KeyValuePair<Sprite, int>(Resources.Load<Sprite>("UI/socket"), 3));
        questLog.allQuests["Where Art Gnome"] = new QuestLog.Quest("Where Art Gnome", "Several Gnomes have gone missing in the night!", new KeyValuePair<Sprite, int>(Resources.Load<Sprite>("UI/socket"), 3));
        questLog.allQuests["Something Lurking"] = new QuestLog.Quest("Something Lurking in the Deep", "He?s heard legends of a great worm who would eventually awake to destroy gnomekind. Destroy him before he kills us all.", new KeyValuePair<Sprite, int>(Resources.Load<Sprite>("UI/socket"), 3));

        questLog.allQuests["Follow the Wisps"].questObject = questLog.transform.GetChild(0).gameObject;
        questLog.allQuests["Help the Gnome"].questObject = questLog.transform.GetChild(1).gameObject;
        questLog.allQuests["Find the Gnome Village"].questObject = questLog.transform.GetChild(2).gameObject;
        questLog.allQuests["Good For Nothing Son"].questObject = questLog.transform.GetChild(3).gameObject;
        questLog.allQuests["This Guy Stinks"].questObject = questLog.transform.GetChild(4).gameObject;
        questLog.allQuests["Where Art Gnome"].questObject = questLog.transform.GetChild(5).gameObject;
        questLog.allQuests["Something Lurking"].questObject = questLog.transform.GetChild(6).gameObject;

        foreach (var q in questLog.allQuests)
        {
            q.Value.questObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = q.Value.title;
            q.Value.questObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = q.Value.infoText;
            q.Value.questObject.transform.GetChild(3).GetComponent<Image>().sprite = q.Value.rewardPairs.Key;
            q.Value.questObject.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = q.Value.rewardPairs.Value.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
