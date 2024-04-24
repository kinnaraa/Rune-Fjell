using UnityEngine.SceneManagement;
using UnityEngine;
using Cinemachine;

public class EscMenuScript : MonoBehaviour
{
    public PlayerMovement PM;
    public ThirdPersonCam Cam;
    public PlayerMagic Magic;
    public CinemachineBrain cinemachine;


    // Static reference to the player instance
    public static EscMenuScript Instance { get; private set; }
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
        gameObject.SetActive(true);
        GameObject.Find("Player").GetComponent<Player>().tabMenu.SetActive(true);
        if(GameObject.Find("Are You Sure"))
        {
            GameObject.Find("Are You Sure").SetActive(true);
        }
        SceneManager.LoadScene("MainMenuScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Resume()
    {
        gameObject.SetActive(false);

        cinemachine.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Cam.enabled = true;
        PM.enabled = true;
        Magic.enabled = true;
    }
}
