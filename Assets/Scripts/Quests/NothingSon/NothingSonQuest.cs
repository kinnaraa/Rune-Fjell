using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static QuestLog;

public class NothingSonQuest : MonoBehaviour
{
    public GameManager GM;
    public QuestManager questManager;
    //public QuestLog questLog;
    public newSkillTree skillTree;

    public GameObject Player;
    public GameObject MomGnome;
    public GameObject Berry;
    public GameObject Wood;
    public GameObject SonGnome;

    //public Transform[] LocationsOfItems;
    public bool questStarted;
    public int numBerry;
    public int numWood;
    public bool itemsCollected;
    public bool questDone = false;

    private string[] dialogue = new string[6];
    private string[] dialogue2 = new string[4];
    public TextMeshProUGUI momSpeech;
    public GameObject EButton;
    private int dialogueCount = 0;

    public FindGnomeVillageQuest FGV;

    AudioSource GnomeVoice;

    public void Start()
    {
        //questStarted = true;
        questStarted = false;
        itemsCollected = false;
        numBerry = 0;
        numWood = 0;
        //Update Quest Log (start)

        dialogue[0] = "Gleebel! I was so worried for you, don't ever leave me like that again!";
        dialogue[1] = "Thank you for helping him home.";
        dialogue[2] = "I'm sorry my son doesn't know any better, he really needs to get his life together.";
        dialogue[3] = "Would you mind helping me get supplies for dinner since my son seems to be good for nothing?";
        dialogue[4] = "I just need 2 berries for my meal and 3 wood stacks for my fire.";
        dialogue[5] = "There should be some by the glade just down the way you came and to the left.";

        dialogue2[0] = "Thank you so much, this is exactly what I needed!";
        dialogue2[1] = "I see my son gave you that interesting little rock he had.";
        dialogue2[2] = "If you're looking to learn more about it, I can point you in the right direction...";
        dialogue2[3] = "I would take that to the gnome in the smokey hut in the back of town. He'll have some info for you.";

        GnomeVoice = MomGnome.GetComponent<AudioSource>();
    }

    public void Update()
    {

        //float distanceGnomeHouse = Vector3.Distance(player.transform.position, GnomeHouse.transform.position);
        float distanceGnome = Vector3.Distance(Player.transform.position, MomGnome.transform.position);

        if ( distanceGnome < 3 && !questStarted && FGV.foundVillage)
        {
            //Gnome Dialogue with Player
            EButton.transform.localScale = new Vector3(1, 1, 1);
            EButton.SetActive(true);

            momSpeech.text = dialogue[dialogueCount];
            if (dialogueCount == 0)
            {
                if (!GnomeVoice.isPlaying)
                {
                    GnomeVoice.Play();
                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogueCount++;
                //makes the loop re-start if you click
                if (GnomeVoice.isPlaying)
                {
                    GnomeVoice.Stop();
                }
                GnomeVoice.Play();
            }

            if (dialogueCount >= 6)
            {
                EButton.SetActive(false);
                dialogueCount = 0;
                momSpeech.text = "";
                GnomeVoice.Stop();
                questStarted = true;
                SonGnome.GetComponent<GnomeWander>().firstPointSetOn = false;
                SonGnome.GetComponent<GnomeWander>().enabled = true;
                SonGnome.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 2;


            }

        }

        if (questStarted && !questDone)
        {
            //float distanceBerry = Vector3.Distance(Player.transform.position, Berry.transform.position);
            //float distanceWood = Vector3.Distance(Player.transform.position, Wood.transform.position);

            if (numWood >= 3 && numBerry >= 2)
            {
                itemsCollected = true;
                //Update Quest Log (done)
            }

            if (itemsCollected && (distanceGnome < 2) && !questDone)
            {
                EButton.transform.localScale = new Vector3(1, 1, 1);
                EButton.SetActive(true);

                if (dialogueCount == 0)
                {
                    if (!GnomeVoice.isPlaying)
                    {
                        GnomeVoice.Play();
                    }
                }

                momSpeech.text = dialogue2[dialogueCount];
                if (Input.GetKeyDown(KeyCode.E))
                {
                    dialogueCount++;
                    //makes the loop re-start if you click
                    if (GnomeVoice.isPlaying)
                    {
                        GnomeVoice.Stop();
                    }
                    GnomeVoice.Play();
                }

                if (dialogueCount >= 4)
                {
                    EButton.transform.localScale = new Vector3(0, 0, 0);

                    EButton.SetActive(false);
                    momSpeech.text = "";
                    GnomeVoice.Stop();

                    questDone = true;

                    questManager.allQuests["Good For Nothing Son"].isActive = false;
                    skillTree.skillPoints += 3;
                    questManager.allQuests["This Guy Stinks"].isActive = true;
                    MomGnome.GetComponent<GnomeWander>().firstPointSetOn = false;

                }
            }
        }

    }

    //Starting Quest
    public void StartQuest()
    {
        questStarted = true;
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

}
