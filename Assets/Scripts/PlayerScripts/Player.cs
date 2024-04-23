using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Static reference to the player instance
    public static Player Instance { get; private set; }

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

    // Optional: Add any player-specific initialization logic here
    public float PlayerHealth;
    public float PlayerStamina;
    public int HealthRegenRate;
    public int StamRegenRate;
    public int DamageIncrease;    
    public int SkillPoints;
    public int Coins;
    public Transform spawn;

    public GameObject tabMenu;
    public GameObject escMenu;
    public GameObject EnterCaveMenu;
    public PlayerMovement PM;
    public ThirdPersonCam Cam;
    public PlayerMagic Magic;
    

    [Header("Keybinds")]
    public KeyCode TabMenu = KeyCode.Tab;
    public KeyCode EscMenu = KeyCode.Escape;

    private bool tabMenuOpen;
    private bool escMenuOpen;

    private bool canOpenMenu = true;

    public AudioClip deathSound;
    private AudioSource SpecialSounds;


    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
        // Set the player's position and rotation after the scene has loaded
        gameObject.SetActive(false);
        transform.localPosition = new Vector3(-144.960007f,20.7600002f,65.3499985f);
        transform.localScale = new Vector3(1.80072713f,1.80072713f,1.80072713f);
        gameObject.SetActive(true);
        PlayerStamina = 100;
        PlayerHealth = 100;
        spawn = gameObject.transform;
    }

    public void Update()
    {
        SpecialSounds = GameObject.Find("SpecialPlayerAudio").GetComponent<AudioSource>();
        Cam = GameObject.Find("Main Camera").GetComponent<ThirdPersonCam>();
        if(PlayerHealth <= 0)
        {
            Kill();
        }

        Image health = GameObject.Find("Health").GetComponent<Image>();
        health.fillAmount = PlayerHealth/100;

        Image stam = GameObject.Find("Stam").GetComponent<Image>();
        stam.fillAmount = PlayerStamina/100;

        tabMenuOpen = tabMenu.activeSelf;
        escMenuOpen = escMenu.activeSelf;
        if(EnterCaveMenu && !EnterCaveMenu.activeSelf)
        {
            if(Input.GetKeyDown(TabMenu) && canOpenMenu )
            {
                if(tabMenuOpen)
                {
                    StartCoroutine(MenuCooldown());
                    tabMenu.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;

                    Cam.enabled = true;
                    PM.enabled = true;
                    Magic.enabled = true;
                    Magic.SetAbilityUI();
                }
                else if(!escMenuOpen)
                {
                    StartCoroutine(MenuCooldown());
                    tabMenu.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    PM.enabled = false;
                    Magic.enabled = false;
                    Cam.enabled = false;
                }
            }

            if(Input.GetKeyDown(EscMenu) && canOpenMenu)
            {
                if(escMenuOpen)
                {
                    StartCoroutine(MenuCooldown());
                    escMenu.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;

                    Cam.enabled = true;
                    PM.enabled = true;
                    Magic.enabled = true;
                }
                else if(!tabMenuOpen) 
                {
                    StartCoroutine(MenuCooldown());
                    escMenu.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    PM.enabled = false;
                    Magic.enabled = false;
                    Cam.enabled = false;
                }
            }
        }
    }

    public IEnumerator MenuCooldown()
    {
        canOpenMenu = false;
        yield return new WaitForSeconds(0.25f);
        canOpenMenu = true;
    }

    public void Kill()
    {
        SpecialSounds.clip = deathSound;
        SpecialSounds.Play();
        gameObject.transform.position = spawn.position;
        PlayerHealth = 100;
        PlayerStamina = 100;
        GameObject.Find("Player").GetComponent<PlayerMovement>().stepCoolDown = 0;
    }
}
