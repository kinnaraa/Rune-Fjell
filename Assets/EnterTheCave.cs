using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterTheCave : MonoBehaviour
{
    public PlayerMovement PM;
    public ThirdPersonCam Cam;
    public PlayerMagic Magic;
    public Player Player;

    public static EnterTheCave Instance { get; private set; }

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

    public void GoToCave()
    {
        Player = GameObject.Find("Player").GetComponent<Player>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Cam.enabled = true;
        PM.enabled = true;
        Magic.enabled = true;

        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += Player.OnSceneLoaded;
        
        // Load the scene
        SceneManager.LoadScene("CaveScene");
        gameObject.SetActive(false);
    }

    public void Coward()
    {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Cam.enabled = true;
        PM.enabled = true;
        Magic.enabled = true;
    }
}  
