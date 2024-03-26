using System.Collections;
using Cinemachine;
using TMPro;
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

    public QuestLog questLog;
    public newSkillTree newSkillTree;
    public TabMenuScript TMS;

    public TextMeshProUGUI gnomeTalk;
    public bool FirstBatDead = false;

    public void Start()
    {
        StartCoroutine(OnStart());
    }

    public void Update()
    {
        if(FirstBatDead)
        {
            FirstBatDead = false;
            StartCoroutine(GnomeDialogue());
        }
    }

    public IEnumerator OnStart()
    {
       
        yield return new WaitForSeconds(1f);
        Player.SetActive(true);
        yield return new WaitForSeconds(2f);
        Player.GetComponent<Rigidbody>().useGravity = true;
        Player.GetComponent<PlayerMovement>().enabled = true;
        Player.GetComponent<Player>().enabled = true;
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
        questLog.allQuests[0].isActive = true;
        questLog.allQuests[0].currentQuest = true;

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

        GameObject enemy = Instantiate(Bat, BatSpawn.position, BatSpawn.rotation);

    }

    public IEnumerator GnomeDialogue()
    {
        gnomeTalk.text = "I came out here looking for adventure and all I found was trouble";
        yield return new WaitForSeconds(2f);
        gnomeTalk.text = "Can you help me find my village?";
        yield return new WaitForSeconds(2f);
        gnomeTalk.text = "I couldn't get this thing to work, maybe you can?";

        //unlock kenaz rune
        newSkillTree.skillList[1][0].unlocked = true;

        //force open skilltree
        StartCoroutine(Player.GetComponent<Player>().MenuCooldown());
        Player.GetComponent<Player>().tabMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Player.GetComponent<Player>().PM.enabled = false;
        Player.GetComponent<Player>().Magic.enabled = false;
        Player.GetComponent<Player>().Cam.enabled = false;
        TMS.OpenSkillTree();
    }
}
