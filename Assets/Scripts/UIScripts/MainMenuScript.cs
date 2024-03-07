using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public GameObject creditsMenu;

    public void OpenCredits()
    {
        creditsMenu.SetActive(true);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void CloseCredits()
    {
        creditsMenu.SetActive(false);
    }
}
