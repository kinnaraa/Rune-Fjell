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
    public TextMeshProUGUI secondDialogue;
    public GameObject normalMayorEButton;

    private string[] dialogue2 = new string[5];
    public GameObject bigBoys;

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
        dialogue2[2] = "If you kill one ice creature and a megabat maybe we can find some clues as to what's going on.";
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
                StartCoroutine(FadeOut());
            }  
        }

        // second dialogue after cutscene

        if(canTalkToMayor && Vector3.Distance(Player.transform.position, NormalMayorGnome.transform.position) < 5.0f)
        {
            normalMayorEButton.transform.localScale = new Vector3(1, 1, 1);
            normalMayorEButton.SetActive(true);

            secondDialogue.text = dialogue2[dialogueCount];

            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogueCount++;
            }

            Debug.Log(dialogueCount);

            if (dialogueCount >= 4)
            {
                normalMayorEButton.SetActive(false);
                secondDialogue.text = "";
                bigBoys.SetActive(true);
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
