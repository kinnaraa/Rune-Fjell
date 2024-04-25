using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class FindGnomeVillageQuest : MonoBehaviour
{
    public GameManager GM;
    public GameObject Player;
    public NavMeshAgent Gnome;
    public Transform[] path;
    public bool OnFirstPath = false;
    private int index;
    public QuestManager questManager;
    public bool foundVillage = false;

    public NothingSonQuest nothingSonQuest;
    public newSkillTree skillTree;

    public void Start()
    {
        index = 0;
    }

    public void Update()
    {
        if (OnFirstPath)
        {
            float distance = Vector3.Distance(Player.transform.position, Gnome.transform.position);
            if (distance <= 4)
            {
                if (!Gnome.pathPending && Gnome.remainingDistance < 0.1f)
                {
                    index++;
                    if (index < path.Length)
                    {
                        MoveToNextWaypoint();
                    }
                    else
                    {
                        if (!foundVillage)
                        {
                            skillTree.skillPoints += 2;

                            questManager.allQuests["Find the Gnome Village"].isActive = false;
                            questManager.allQuests["Good For Nothing Son"].isActive = true;

                            GM.gnomeTalk.text = "";
                            
                        }
                        foundVillage = true;
                        
                    }
                }
            }
        }
    }

    void MoveToNextWaypoint()
    {
        // Set the next waypoint as the target destination
        Gnome.SetDestination(path[index].position);
    }

    public void StartPath()
    {
        questManager.allQuests["Find the Gnome Village"].isActive = true;
        
        OnFirstPath = true;
        // Start moving towards the first waypoint
        Gnome.SetDestination(path[index].position);
    }
}
