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
    public PlayerMovement PM;
    public ThirdPersonCam Cam;
    public PlayerMagic Magic;

    [Header("Keybinds")]
    public KeyCode TabMenu = KeyCode.Tab;

    private bool tabMenuOpen;
    private bool canOpenMenu = true;

    public void Update()
    {
        tabMenuOpen = tabMenu.activeSelf;
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
            else
            {
                tabMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                PM.enabled = false;
                Magic.enabled = false;
                Cam.enabled = false;
            }
            StartCoroutine(MenuCooldown());
        }
    }

    public IEnumerator MenuCooldown()
    {
        canOpenMenu = false;
        yield return new WaitForSeconds(1f);
        canOpenMenu = true;
    }
}
