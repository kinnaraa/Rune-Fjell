using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
   public static DontDestroyOnLoad Instance { get; private set; }

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
    }
}
