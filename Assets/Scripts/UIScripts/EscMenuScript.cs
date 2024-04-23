using UnityEngine.SceneManagement;
using UnityEngine;

public class EscMenuScript : MonoBehaviour
{
    // Static reference to the player instance
    public static EscMenuScript Instance { get; private set; }

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
        gameObject.SetActive(false);
    }

    // Optional: Add any player-specific initialization logic here
    public void MainMenu()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("MainMenuScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
