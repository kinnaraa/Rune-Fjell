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

    // Start is called before the first frame update
    void Start()
    {
        initialGnomeLocation = weedGnome.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(Player.transform.position, gnomeHouse.transform.position) < 5)
        {
            if (Input.GetKeyDown(KeyCode.E) && !canCollect)
            {
                weedGnome.transform.position = new Vector3(-194.17f, 24.80045f, -59.43f); // rotation to 48.562
                knocked = true;
            }
            
            if (Input.GetKeyDown(KeyCode.E) && knocked && !canCollect)
            {
                talkToWeedGnome1();

                questManager.allQuests["This Guy Stinks"].isActive = true;

                weedGnome.transform.position = initialGnomeLocation.position;
                canCollect = true;
            }
        }

        if(mushroomCount >= 10)
        {
            collectedShrooms = true;
        }

        if (collectedShrooms)
        {
            //update quest log info and active quest info to tell you to go back to weed gnome
        }

        if(collectedShrooms && Vector3.Distance(Player.transform.position, gnomeHouse.transform.position) < 5)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                talkToWeedGnome2();

                questManager.allQuests["This Guy Stinks"].isActive = false;
                skillTree.skillPoints += 3;
                startCutscene();
            }
        }
    }

    void talkToWeedGnome1()
    {
        // dialogue telling you he has helpful secret knowledge but only if you collect him 10 mushrooms
    }

    void talkToWeedGnome2()
    {
        // dialogue telling you thank you and then explains bind runes to you
    }

    void startCutscene()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        questManager.allQuests["Where Art Gnome"].isActive = true;

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

        yield return new WaitForSeconds(5f); // Wait for 3 seconds

        cutsceneText.SetActive(false);
        //Instantiate gnomes in village

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
    }
}
