using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    public GameObject escMenu;
    public GameObject tabMenu;
    public GameObject AreYouSure;

    public Image blackScreen;
    public GameObject tabScreen;
    public GameObject escScreen;
    private float fadeDuration = 3.0f;

    public WyrmHealth wyrmHealth;

    // Start is called before the first frame update
    void Start()
    {
        escMenu = GameObject.Find("Player").GetComponent<Player>().escMenu;
        tabMenu = GameObject.Find("Player").GetComponent<Player>().tabMenu;
        blackScreen = GameObject.Find("PlayerUI").transform.GetChild(7).GetComponent<Image>();
        AreYouSure = wyrmHealth.AreYouSure;
    }

    private void Update()
    {

    }

    public IEnumerator EndGame()
    {
        Debug.Log("ending game");
        yield return new WaitForSeconds(0f);

        // Fade out and wait until it's done
        yield return StartCoroutine(FadeOut());

        // After fading out, load the Credits scene
        SceneManager.LoadScene("Credits");
    }


    public IEnumerator FadeOut()
    {
        Debug.Log("fading out");
        float elapsedTime = 0f;
        Color currentColor = blackScreen.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            currentColor.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            blackScreen.color = currentColor;
            yield return null;
        }

        AreYouSure = GameObject.Find("Are You Sure?");

        escScreen = escMenu.transform.GetChild(5).gameObject;
        tabScreen = tabMenu.transform.GetChild(1).gameObject;

        AreYouSure = wyrmHealth.AreYouSure;

        escScreen.SetActive(true);
        tabScreen.SetActive(true);

        tabMenu.SetActive(true);
        escMenu.SetActive(true);
        AreYouSure.SetActive(true);
    }
}
