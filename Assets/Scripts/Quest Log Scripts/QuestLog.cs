using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLog : MonoBehaviour
{
    public List<Quest> quests;

    public class Quest
    {
        public string title;
        public Dictionary<Sprite, int> needInfo;
        public int numNeeds;
        public string infoText;
        public bool started = false;
        public bool completed = false;

        public Quest(string questTitle, int needs, string questInfoText)
        {
            title = questTitle;
            infoText = questInfoText;
            numNeeds = needs;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        quests = new List<Quest>()
        {
            new Quest("Help the Gnome", 1, "help gnome info...blah blah blah..."),
            new Quest("Find the Gnome Village", 1, "find gnome village info, blah blah, blah"),
            new Quest("Good For Nothing Son", 7, "Gnome�s mom needs help getting supplies for dinner"),
            new Quest("Pesky Wolves", 5, "Gnome is grumpy about the loud wolves in the area and offered you a reward to get rid of them"),
            new Quest("This Guy Stinks", 10, "Find gnome in town who needs help in weird smokey hut"),
            new Quest("Cracking the Code", 1, "The seer offered you to try a mushroom, will you accept?"),
            new Quest("Where Art Gnome", 3, "Several Gnomes have gone missing in the night!"),
            new Quest("Unknown", 1, "Make your way towards the mountains to try and find the beast who stole the gnomes"),
            new Quest("Something Lurking in the Deep", 1, "He�s heard legends of a great worm who would eventually awake to destroy gnomekind. Destroy him before he kills us all."),
        };

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<Sprite>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
