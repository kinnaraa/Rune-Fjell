using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabMenuScript : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject SkillTree;

    public void Awake()
    {
        SkillTree.SetActive(false);
        Inventory.SetActive(true);
    }
    public void OpenInventory()
    {
        SkillTree.SetActive(false);
        Inventory.SetActive(true);
    }

    public void OpenSkillTree()
    {
        Inventory.SetActive(false);
        SkillTree.SetActive(true);
    }
}
