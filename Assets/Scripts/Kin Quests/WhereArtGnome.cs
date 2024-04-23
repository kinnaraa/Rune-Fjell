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

    public bool megaBatDead = false;
    public bool bigIceBoyDead = false;

    private GameObject megaBat;
    public GameObject bigIceBoy;

    private string[] endDialogue = new string[7];
    private bool WAGfinished = false;

    public newSkillTree skillTree;

    public QuestManager questManager;
    public bool healingUnlocked = false;
    public bool damageUnlocked = false;

    AudioSource GnomeVoice;
    public int clueCount = 0;
    private GameObject clue1;
    private GameObject clue2;

    private GameObject clue1Popup;
    private GameObject clue2Popup;

    public GameObject thoughtTexts;
    private TextMeshProUGUI firstClueThoughts;
    private TextMeshProUGUI secondClueThoughts;

    public GameObject collectedRunes;
    private GameObject Rune1;
    private GameObject Rune2;

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

        GnomeVoice = GameObject.Find("Mayor").GetComponent<AudioSource>();

        megaBat = bigBoys.transform.GetChild(0).gameObject;
        bigIceBoy = bigBoys.transform.GetChild(1).gameObject;

        clue1 = bigBoys.transform.GetChild(2).gameObject;
        clue2 = bigBoys.transform.GetChild(3).gameObject;

        clue1Popup = clue1.transform.GetChild(0).gameObject;
        clue2Popup = clue2.transform.GetChild(0).gameObject;

        firstClueThoughts = thoughtTexts.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        secondClueThoughts = thoughtTexts.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        Rune1 = collectedRunes.gameObject;
        Rune2 = collectedRunes.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //initial dialogue at meeting of gnomes
        GnomeVoice = GameObject.Find("Mayor").GetComponent<AudioSource>();
        GameObject Mayor = GameObject.Find("Mayor");

        if (startedQuest && Vector3.Distance(Player.transform.position, triggerCutsene.transform.position) < 10.0f && !canTalkToMayor && !bigIceBoyDead && !megaBatDead)
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
            mayorSpeech.text = dialogue[dialogueCount];


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

            Debug.Log(GnomeVoice.isPlaying);

            if(dialogueCount >= 3)
            {
                EButton.SetActive(false);
                canTalkToMayor = true;
                dialogueCount = 0;
                mayorSpeech.text = "";
                GnomeVoice.Stop();
                StartCoroutine(FadeOut());
            }  
        }

        // second dialogue after cutscene

        if(canTalkToMayor && !fightingCreatures && Vector3.Distance(Player.transform.position, NormalMayorGnome.transform.position) < 5.0f)
        {

            Mayor.GetComponent<GnomeWander>().agent.speed = 0;
            normalMayorEButton.transform.localScale = new Vector3(1, 1, 1);
            normalMayorEButton.SetActive(true);

            if (dialogueCount == 0)
            {
                if (!GnomeVoice.isPlaying)
                {
                    GnomeVoice.Play();
                }
            }
            normalMayorDialogue.text = dialogue2[dialogueCount];

            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogueCount++;
                if (GnomeVoice.isPlaying)
                {
                    GnomeVoice.Stop();
                }
                GnomeVoice.Play();
            }

            if (dialogueCount >= 4)
            {
                Mayor.GetComponent<GnomeWander>().UpdateDestination();
                normalMayorEButton.SetActive(false);
                normalMayorDialogue.text = "";
                GnomeVoice.Stop();
                dialogueCount = 0;
                bigBoys.SetActive(true);
                fightingCreatures = true;
                canTalkToMayor = false;
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

            if (clue1 && Vector3.Distance(Player.transform.position, clue1.transform.position) < 5.0f)
            {
                clue1Popup.SetActive(true);
                clue2Popup.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(clue1);
                    clueCount++;
                    StartCoroutine(FadeOutThoughts());
                }
            }
            if (clue2 && Vector3.Distance(Player.transform.position, clue2.transform.position) < 5.0f)
            {
                clue2Popup.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(clue2);
                    clueCount++;
                    StartCoroutine(FadeOutThoughts());
                }
            }

            if (bigIceBoyDead && megaBatDead && clueCount >= 2)
            {
                fightingCreatures = false;
            }
        }

        // monsters are dead and return to mayor

        if(!WAGfinished && bigIceBoyDead && megaBatDead && Vector3.Distance(Player.transform.position, NormalMayorGnome.transform.position) < 5.0f)
        {
            normalMayorEButton.transform.localScale = new Vector3(1, 1, 1);
            normalMayorEButton.SetActive(true);

            if (dialogueCount == 0)
            {
                if (!GnomeVoice.isPlaying)
                {
                    GnomeVoice.Play();
                }
            }
            normalMayorDialogue.text = endDialogue[dialogueCount];

            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogueCount++;
                if (GnomeVoice.isPlaying)
                {
                    GnomeVoice.Stop();
                }
                GnomeVoice.Play();
            }

            if (dialogueCount >= 7)
            {
                normalMayorEButton.SetActive(false);
                normalMayorDialogue.text = "";
                GnomeVoice.Stop();
                WAGfinished = true;
                questManager.allQuests["Where Art Gnome"].isActive = false;
                questManager.allQuests["Something Lurking"].isActive = true;

                //unlock healing and damage runes
                healingUnlocked = true;
                damageUnlocked = true;

                Rune1.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Wunjo_Activated");
                Rune2.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Uruz_Activated");

                StartCoroutine(displayRunes());

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

    IEnumerator FadeInThoughts(TextMeshProUGUI text)
    {
        float elapsedTime = 0f;

        Color originalColor = text.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);
            text.color = newColor;

            yield return null;
        }
    }

    IEnumerator FadeOutThoughts()
    {
        TextMeshProUGUI text = firstClueThoughts;
        if(clueCount == 1)
        {
            text = firstClueThoughts;
        }else if (clueCount == 2)
        {
            text = secondClueThoughts;
        }

        float elapsedTime = 0f;

        Color originalColor = text.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);

            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);
            text.color = newColor;

            yield return null;
        }

        StartCoroutine(FadeInThoughts(text));
    }

    IEnumerator displayRunes()
    {
        Debug.Log("Fade out");

        float elapsedTime = 0f;
        Color currentColor = Rune1.GetComponent<Image>().color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            currentColor.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            Rune1.GetComponent<Image>().color = currentColor;
            Rune2.GetComponent<Image>().color = currentColor;
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(displayRunes2());
    }

    IEnumerator displayRunes2()
    {
        float elapsedTime = 0f;

        Color originalColor = Rune1.GetComponent<Image>().color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);
            Rune1.GetComponent<Image>().color = newColor;
            Rune2.GetComponent<Image>().color = newColor;

            yield return null;
        }
    }
}
