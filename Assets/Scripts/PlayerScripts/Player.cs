using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int PlayerHealth;
    public int SkillPoints;
    public int Coins;

    public string QAbility;
    public string EAbility;

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
        tabMenuOpen = tabMenu.activeSelf;
        escMenuOpen = escMenu.activeSelf;
        if(Input.GetKey(TabMenu) && canOpenMenu)
        {
            if(tabMenuOpen)
            {
                tabMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                Cam.enabled = true;
                PM.enabled = true;
                Magic.enabled = true;
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

        if(Input.GetKey(EscMenu) && canOpenMenu)
        {
            if(escMenuOpen)
            {
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
        yield return new WaitForSeconds(1f);
        canOpenMenu = true;
    }
}
