using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;
using System.IO;

public class TabMenuScript : MonoBehaviour
{
    // Static reference to the player instance
    public static TabMenuScript Instance { get; private set; }
    public GameObject Map;
    public GameObject SkillTree;
    public GameObject QuestLog;

    public newSkillTree skillTree;

    // Optional: Add any player-specific variables or methods here

    private void Awake()
    {
        // Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            // If an instance already exists, destroy this instance
            Destroy(gameObject);
            return;
        }

        // Set the instance to this object
        Instance = this;

        // Make the player object persist between scenes
        DontDestroyOnLoad(gameObject);

        SkillTree.SetActive(false);
        QuestLog.SetActive(false);
        Map.SetActive(true);
        gameObject.SetActive(false);
    }

    // Optional: Add any player-specific initialization logic here
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

        skillTree.infoSection.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Skill Tree";
        skillTree.infoSection.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Unlock Runes at Monoliths around the island or through major quest rewards.\n\nBind runes are abilities located between Runes and require both of their surrounding Runes to be unlocked before being able to unlock the Bindrune.\n\nUnlock Bindrunes through skill points which are gained through quest completion.";
        skillTree.infoSection.transform.GetChild(3).gameObject.SetActive(false);
        skillTree.infoSection.transform.GetChild(2).gameObject.SetActive(false);

        SkillTree.SetActive(true);
        
        //skillTree.infoSection.transform.GetChild(4).GetComponent<Image>().sprite = skillTree.nullSkill.sprite;
    }

    public void OpenQuestLog()
    {
        Map.SetActive(false);
        SkillTree.SetActive(false);
        QuestLog.SetActive(true);
    }
}
