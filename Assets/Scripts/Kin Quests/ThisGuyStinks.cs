using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThisGuyStinks : MonoBehaviour
{
    public int mushroomCount = 0;
    public bool collectedShrooms = false;
    public GameObject Player;
    public bool canCollect = false;
    public bool knocked = false;
    public GameObject gnomeHouse;
    public GameObject weedGnome;
    private Transform initialGnomeLocation;
    public QuestManager questManager;
    public newSkillTree skillTree;

    public Image blackScreen;
    public float fadeDuration = 1f;
    public GameObject cutsceneText;

    public WhereArtGnome WAG;

    public GameObject EtoKnock;

    private string[] dialogue = new string[6];
    public TextMeshProUGUI weedGnomeSpeech;
    public GameObject EButton;

    private string[] dialogue2 = new string[8];
    private int dialogueCount = 0;
    private bool questOver = false;

    // Start is called before the first frame update
    void Start()
    {
        initialGnomeLocation = weedGnome.transform;
        dialogue[0] = "Ahhh, yes. It's you. I've been expecting you...";
        dialogue[1] = "I know what you have... and I know you don't know what it is. *snickers*";
        dialogue[2] = "That thing you have may unlock a whole magical world for you, traveler.";
        dialogue[3] = "How about this? Find me 10 mushrooms for my... studies... and I'll tell you some of my secrets.";
        dialogue[4] = "There should be some if you follow the path straight when leaving town.";
        dialogue[5] = "";

        dialogue2[0] = "Ahh, you're back. I can smell the mushrooms on you, hand them over.";
        dialogue2[1] = "Thank you very much. Now let me explain to you this little rock here that you have.";
        dialogue2[2] = "Long ago there were rocks inscribed with nordic runes, giving them unbelievable powers.";
        dialogue2[3] = "Many are lost, but even those that were found could not be weilded by any soul who tried.";
        dialogue2[4] = "There must be something special about you, traveler.";
        dialogue2[5] = "There are monoliths around this island engraved with the missing runes which only began glowing once you arrived.";
        dialogue2[6] = "Once you unlock multiple runes, you may even be able to combine them into \"bind runes\"...";
        dialogue2[7] = "Anyways, it's getting late, come stay with me for the night.";
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(Player.transform.position, gnomeHouse.transform.position) < 5)
        {
            if (!knocked)
            {
                EtoKnock.SetActive(true);
            }
            else
            {
                EtoKnock.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.E) && !knocked)
            {
                weedGnome.transform.position = new Vector3(-194.58f, 24.80045f, -58.64f);
                knocked = true;
            }
            
            if (Input.GetKeyDown(KeyCode.E) && knocked && !canCollect)
            {
                EButton.transform.localScale = new Vector3(1, 1, 1);
                EButton.SetActive(true);

                weedGnomeSpeech.text = dialogue[dialogueCount];
                if (Input.GetKeyDown(KeyCode.E))
                {
                    dialogueCount++;
                }

                Debug.Log(dialogueCount);

                if (dialogueCount >= 6)
                {
                    EButton.SetActive(false);
                    weedGnomeSpeech.text = "";

                    questManager.allQuests["This Guy Stinks"].isActive = true;

                    weedGnome.transform.position = initialGnomeLocation.position;
                    canCollect = true;
                    dialogueCount = 0;
                }
            }
        }

        if(mushroomCount >= 10)
        {
            collectedShrooms = true;
        }

        if(collectedShrooms && Vector3.Distance(Player.transform.position, gnomeHouse.transform.position) < 5 && !questOver)
        {
            EButton.transform.localScale = new Vector3(1, 1, 1);
            EButton.SetActive(true);

            weedGnomeSpeech.text = dialogue2[dialogueCount];
            if (Input.GetKeyDown(KeyCode.E) && !questOver)
            {
                dialogueCount++;
            }

            Debug.Log(dialogueCount);

            if (dialogueCount >= 8 && !questOver)
            {
                weedGnomeSpeech.text = "";
                EButton.SetActive(false);
                questManager.allQuests["This Guy Stinks"].isActive = false;
                skillTree.skillPoints += 3;
                questOver = true;
                startCutscene();
            }
        }
    }

    void startCutscene()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color currentColor = blackScreen.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            currentColor.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            blackScreen.color = currentColor;
            yield return null;
        }

        cutsceneText.SetActive(true);

        yield return new WaitForSeconds(5f);

        cutsceneText.SetActive(false);
        questManager.allQuests["Where Art Gnome"].isActive = true;

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color currentColor = blackScreen.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            currentColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            blackScreen.color = currentColor;
            yield return null;
        }

        WAG.StartQuest();
    }
}
