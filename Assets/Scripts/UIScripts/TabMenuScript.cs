using UnityEngine;

public class TabMenuScript : MonoBehaviour
{
    // Static reference to the player instance
    public static TabMenuScript Instance { get; private set; }
    public GameObject Map;
    public GameObject SkillTree;
    public GameObject QuestLog;

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
        SkillTree.SetActive(true);
    }

    public void OpenQuestLog()
    {
        Map.SetActive(false);
        SkillTree.SetActive(false);
        QuestLog.SetActive(true);
    }
}
