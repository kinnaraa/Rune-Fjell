using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabMenuScript : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject SkillTree;
    public GameObject QuestLog;

    public void Awake()
    {
        SkillTree.SetActive(false);
        QuestLog.SetActive(false);
        Inventory.SetActive(true);
    }
    public void OpenInventory()
    {
        SkillTree.SetActive(false);
        QuestLog.SetActive(false);
        Inventory.SetActive(true);
    }

    public void OpenSkillTree()
    {
        Inventory.SetActive(false);
        QuestLog.SetActive(false);
        SkillTree.SetActive(true);
    }

    public void OpenQuestLog()
    {
        Inventory.SetActive(false);
        SkillTree.SetActive(false);
        QuestLog.SetActive(true);
    }
}
