using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class WhereArtGnome : MonoBehaviour
{
    public GameObject gnomesMeeting;
    public GameObject gnomesNormal;
    public GameObject gnomeMom;

    public Transform triggerCutsene;
    bool startedQuest = false;
    public GameObject Player;

    public GameObject mainCamera;

    public Transform targetPosition;
    public Quaternion targetRotation;

    public Image blackScreen;

    public QuestManager qM;
    private string[] dialogue = new string[4];
    public TextMeshProUGUI mayorSpeech;

    public GameObject EButton;
    private float fadeDuration = 1.0f;
    private int dialogueCount = 0;

    private bool canTalkToMayor = false;
    public GameObject NormalMayorGnome;
    public TextMeshProUGUI normalMayorDialogue;
    public GameObject normalMayorEButton;

    private string[] dialogue2 = new string[4];
    public GameObject bigBoys;

    public bool fightingCreatures = false;

    private bool megaBatDead = false;
    private bool bigIceBoyDead = false;

    private GameObject megaBat;
    private GameObject bigIceBoy;

    private string[] endDialogue = new string[7];
    private bool WAGfinished = false;

    public newSkillTree skillTree;

    public QuestManager questManager;
    public bool healingUnlocked;
    public bool damageUnlocked = false;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition.position = new Vector3(-152.15f, 30.169f, -46.43f);
        Vector3 rotationAngles = new Vector3(7.651f, -59.951f, -3.315f);
        targetRotation = Quaternion.Euler(rotationAngles);
        dialogue[0] = "Word has spread and as some of you may know, several gnomes have gone missing in the night...";
        dialogue[1] = "I am working hard to get to the root of this issue. We can't see any more of our friends and family members gone.";
        dialogue[2] = "I just hope it isn't what I fear it is...";
        dialogue[3] = "";

        dialogue2[0] = "I've been watching you newcomer with your runes...";
        dialogue2[1] = "You must help us, you are the only one who can harness the runes, and if it is what I fear, you will need them.";
        dialogue2[2] = "If you find a Dark Ice Creature and a Megabat maybe we can find some clues as to what's going on.";
        dialogue2[3] = "They usually hang out around the mountain to the east. Please let me know if you find any clues, traveler.";

        endDialogue[0] = "Please tell me you have good news!";
        endDialogue[1] = "...";
        endDialogue[2] = "I see... it is indeed what I feared. Those holes you found in the ground can only mean one thing.";
        endDialogue[3] = "The gnome eater... he has returned.";
        endDialogue[4] = "I beg you to help us traveler. He who possessed the runes is said to be our savior.";
        endDialogue[5] = "Please, find the passage into the depths of the mountain... that is where the real monster lies.";
        endDialogue[6] = "Here, take these. I have kept them safe, waiting for this moment. These will help you to face the beast.";

        megaBat = bigBoys.transform.GetChild(0).gameObject;
        bigIceBoy = bigBoys.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //initial dialogue at meeting of gnomes

        if (startedQuest && Vector3.Distance(Player.transform.position, triggerCutsene.transform.position) < 10.0f && !canTalkToMayor)
        {
            EButton.transform.localScale = new Vector3(1, 1, 1);
            EButton.SetActive(true);

            mayorSpeech.text = dialogue[dialogueCount];
            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogueCount++;
            }

            Debug.Log(dialogueCount);

            if(dialogueCount >= 3)
            {
                EButton.SetActive(false);
                canTalkToMayor = true;
                dialogueCount = 0;
                mayorSpeech.text = "";
                StartCoroutine(FadeOut());
            }  
        }

        // second dialogue after cutscene

        if(canTalkToMayor && !fightingCreatures && Vector3.Distance(Player.transform.position, NormalMayorGnome.transform.position) < 5.0f)
        {
            normalMayorEButton.transform.localScale = new Vector3(1, 1, 1);
            normalMayorEButton.SetActive(true);

            normalMayorDialogue.text = dialogue2[dialogueCount];

            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogueCount++;
            }

            Debug.Log(dialogueCount);

            if (dialogueCount >= 4)
            {
                normalMayorEButton.SetActive(false);
                normalMayorDialogue.text = "";
                dialogueCount = 0;
                bigBoys.SetActive(true);
                fightingCreatures = true;
            }
        }

        if (fightingCreatures)
        {
            if (!megaBat)
            {
                megaBatDead = true;
            }
            if (!bigIceBoy)
            {
                bigIceBoyDead = true;
            }
        }

        // monsters are dead and return to mayor

        if(!WAGfinished && bigIceBoyDead && megaBatDead && Vector3.Distance(Player.transform.position, NormalMayorGnome.transform.position) < 5.0f)
        {
            normalMayorEButton.transform.localScale = new Vector3(1, 1, 1);
            normalMayorEButton.SetActive(true);

            normalMayorDialogue.text = endDialogue[dialogueCount];

            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogueCount++;
            }

            if (dialogueCount >= 7)
            {
                normalMayorEButton.SetActive(false);
                normalMayorDialogue.text = "";
                WAGfinished = true;
                questManager.allQuests["Where Art Gnome"].isActive = false;
                questManager.allQuests["Something Lurking"].isActive = true;

                //unlock healing and damage runes
                healingUnlocked = true;
                damageUnlocked = true;

                // add skill points
                skillTree.skillPoints += 6;
            }
        }
    }

    public void StartQuest()
    {
        gnomesMeeting.SetActive(true);
        gnomeMom.SetActive(false);
        gnomesNormal.SetActive(false);
        startedQuest = true;
    }

    
    IEnumerator FadeOut()
    {
        Debug.Log("Fade out");

        float elapsedTime = 0f;
        Color currentColor = blackScreen.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            currentColor.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            blackScreen.color = currentColor;
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        gnomesMeeting.SetActive(false);
        gnomesNormal.SetActive(true);
        gnomeMom.SetActive(true);
        dialogueCount = 0;

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        Debug.Log("Fading in");

        float elapsedTime = 0f;
        Color currentColor = blackScreen.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            currentColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            blackScreen.color = currentColor;
            yield return null;
        }
    }
}
