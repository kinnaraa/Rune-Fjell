using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class MainMenuScript : MonoBehaviour
{
    public GameObject creditsMenu;

    public void OpenCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void EnterTheArena()
    {
        SceneManager.LoadScene("ArenaScene");
    }
}
