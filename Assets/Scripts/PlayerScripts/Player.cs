using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
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
    public PlayerMovement PM;
    public ThirdPersonCam Cam;
    public PlayerMagic Magic;

    [Header("Keybinds")]
    public KeyCode TabMenu = KeyCode.Tab;
    public KeyCode EscMenu = KeyCode.Escape;

    private bool tabMenuOpen;
    private bool escMenuOpen;

    private bool canOpenMenu = true;

    public void Update()
    {
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

        if(Input.GetKeyDown(TabMenu) && canOpenMenu)
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

    public IEnumerator MenuCooldown()
    {
        canOpenMenu = false;
        yield return new WaitForSeconds(0.25f);
        canOpenMenu = true;
    }

    public void Kill()
    {
        transform.position = spawn.position;
        PlayerHealth = 100;
        PlayerStamina = 100;
    }
}
