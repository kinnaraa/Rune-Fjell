using System.Collections;
using UnityEngine;
using TMPro;

public class WispSystem : MonoBehaviour
{
    public GameManager GM;
    public GameObject Wisp;
    public GameObject Player;
    public GameObject Gnome;
    public Transform[] FirstSetOfPoints;
    public string[] WispText = new string[5];
    private bool firstPath = false;
    private bool OnFirstPath = false;
    private GameObject CurrentWisp;
    private int index;

    public QuestManager questManager;

    public void Start()
    {
        index = 0;
    }

    public void Update()
    {
        if (firstPath)
        {
            CurrentWisp = Instantiate(Wisp, FirstSetOfPoints[index].position, FirstSetOfPoints[index].rotation);
            CurrentWisp.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = WispText[index];
            firstPath = false;
            OnFirstPath = true;
        }

        if (OnFirstPath)
        {
            float distance = Vector3.Distance(Player.transform.position, CurrentWisp.transform.position);
            if (distance <= 2)
            {
                Destroy(CurrentWisp);
                index++;

                if (index < FirstSetOfPoints.Length)
                {
                    CurrentWisp = Instantiate(Wisp, FirstSetOfPoints[index].position, FirstSetOfPoints[index].rotation);
                    CurrentWisp.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = WispText[index];
                }
                else
                {
                    OnFirstPath = false;

                    questManager.allQuests["Follow the Wisps"].isActive = false;
                    questManager.allQuests["Help the Gnome"].isActive = true;

                    StartCoroutine(GM.SaveTheGnome());
                }
            }
        }
    }

    public void StartFirstPath()
    {
        firstPath = true;
    }
}
