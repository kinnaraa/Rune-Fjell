using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlayerUI;
    public GameObject Camera;
    public GameObject Portal;
    public GameObject Tent;
    public GameObject Campfire;

    public GameObject Bat;

    public Transform BatSpawn;

    public WispSystem WS;

    public Transform TentPos;
    public Transform CampfirePos;

    public QuestManager questManager;
    public newSkillTree skillTree;
    public TabMenuScript TMS;

    public TextMeshProUGUI gnomeTalk;
    public bool FirstBatDead = false;

    public bool finishedQuest = false;

    public KeyCode EKey = KeyCode.E;

    public float distance;
    public GameObject[] currentSpeaker;
    bool HelpOver = false;

    AudioSource GnomeVoice;

    public FindGnomeVillageQuest findVillageQuest;
    private List<string> dialogue = new List<string>
    {
        "I came out here looking for adventure and all I found was trouble",
        "Can you help me find my village?",
        "I couldn't get this thing to work, maybe you can?",
        "Follow me!"
    };
    private int index = 0;
    private bool secondQuestBegan = true;

    public void Start()
    {
        StartCoroutine(OnStart());
    }

    public void Update()
    {
        GnomeVoice = currentSpeaker[0].GetComponent<AudioSource>();
        distance = Vector3.Distance(Player.transform.position, currentSpeaker[0].transform.position);
        if(FirstBatDead && secondQuestBegan)
        {
            if (!HelpOver)
            {
                gnomeTalk.text = "Thank you for the help!";
               
                 GnomeVoice.Play();
                
                HelpOver = true;
            }
            if (distance <= 5f)
            {
                GameObject.FindGameObjectWithTag("EButton").transform.localScale = new Vector3(1, 1, 1);
                if (Input.GetKeyDown(EKey))
                {
                    FirstGnomeConvo();
                    index++;
                }
            }
            else
            {
                GameObject.FindGameObjectWithTag("EButton").transform.localScale = new Vector3(0, 0, 0);
                GnomeVoice.Stop();
            }
        }
    }

    public IEnumerator OnStart()
    {
       
        yield return new WaitForSeconds(1f);
        Player.SetActive(true);
        yield return new WaitForSeconds(2f);
        Player.GetComponentInParent<Rigidbody>().useGravity = true;
        Player.GetComponentInParent<PlayerMovement>().enabled = true;
        Player.GetComponentInParent<Player>().enabled = true;
        Camera.GetComponent<CinemachineBrain>().enabled = true;
        PlayerUI.SetActive(true);

        yield return new WaitForSeconds(1f);
        Tent.SetActive(true);
        Campfire.SetActive(true);

        Tent.transform.position = Portal.transform.position;
        Campfire.transform.position = Portal.transform.position;
        StartCoroutine(MoveObject(Tent, TentPos));
        StartCoroutine(MoveObject(Campfire, CampfirePos));

        yield return new WaitForSeconds(2f);
        Portal.SetActive(false);
        StartCoroutine(StartFirstQuest());
    }

    public IEnumerator StartFirstQuest()
    {
        yield return new WaitForSeconds(1f);
        questManager.allQuests["Follow the Wisps"].isActive = true;

        WS.StartFirstPath();
    }

    public IEnumerator MoveObject(GameObject thing, Transform endTransform)
    {
        float elapsedTime = 0f;

        // Save the initial rotation
        Quaternion initialRotation = thing.transform.rotation;

        while (elapsedTime < 0.5f)
        {
            // Interpolate position
            thing.transform.position = Vector3.Lerp(Portal.transform.position, endTransform.position, elapsedTime / 0.5f);

            // Interpolate rotation randomly
            float randomAngle = Random.Range(0, 360);
            Quaternion randomRotation = Quaternion.Euler(randomAngle, randomAngle, randomAngle);
            thing.transform.rotation = Quaternion.Lerp(initialRotation, randomRotation, elapsedTime / 0.5f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        thing.transform.SetPositionAndRotation(endTransform.position, endTransform.rotation);
    }

    public IEnumerator SaveTheGnome()
    {
        yield return new WaitForSeconds(1f);

        Instantiate(Bat, BatSpawn.position, BatSpawn.rotation);
        if (!GnomeVoice.isPlaying)
        {
            GnomeVoice.Play();
        }

    }

    public void FirstGnomeConvo()
    {
        if(index < dialogue.Count - 1)
        {
            //makes the loop re-start if you click
            if (GnomeVoice.isPlaying)
            {
                GnomeVoice.Stop();
            }
            GnomeVoice.Play();

            gnomeTalk.text = dialogue[index];
        }
        else
        {
            GameObject.FindGameObjectWithTag("EButton").transform.localScale = new Vector3(0, 0, 0);

            secondQuestBegan = false;
            index = 0;

            //unlock kenaz rune and finish/start quests
            questManager.allQuests["Help the Gnome"].isActive = false;
            questManager.allQuests["Find the Gnome Village"].isActive = true;

            //force open skilltree
            StartCoroutine(Player.GetComponentInParent<Player>().MenuCooldown());
            Player.GetComponentInParent<Player>().tabMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Player.GetComponentInParent<Player>().PM.enabled = false;
            Player.GetComponentInParent<Player>().Magic.enabled = false;
            Player.GetComponentInParent<Player>().Cam.enabled = false;
            TMS.OpenSkillTree();
            finishedQuest = true;
            // is there a way to make the kenaz rune selected?

            gnomeTalk.text = dialogue[dialogue.Count - 1];
            findVillageQuest.StartPath();
            if (GnomeVoice.isPlaying)
            {
                GnomeVoice.Stop();
            }
        }
    }
}
