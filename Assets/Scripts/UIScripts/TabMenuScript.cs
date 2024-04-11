using UnityEngine;

public class TabMenuScript : MonoBehaviour
{
    public GameObject Map;
    public GameObject SkillTree;
    public GameObject QuestLog;

    public void Awake()
    {
        SkillTree.SetActive(false);
        QuestLog.SetActive(false);
        Map.SetActive(true);
    }
    public void OpenMap()
    {
        SkillTree.SetActive(false);
        QuestLog.SetActive(false);
        Map.SetActive(true);
    }

    public void OpenSkillTree()
    {
        Map.SetActive(false);
        QuestLog.SetActive(false);
        SkillTree.SetActive(true);
    }

    public void OpenQuestLog()
    {
        Map.SetActive(false);
        SkillTree.SetActive(false);
        QuestLog.SetActive(true);
    }
}
