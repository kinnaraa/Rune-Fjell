using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WyrmHealth : MonoBehaviour
{
    public GameObject Wrym;
    public int maxHealth = 100;
    public int currentHealth;
    public SkinnedMeshRenderer[] bodyParts;
    public Material red;
    public AudioSource deathSound;
    public Coroutine flashingCoroutine;
    public GameObject AreYouSure;

    private bool isRed = false;
    public EndingManager endingManager;
    private bool ended = false;

    void Start()
    {
        AreYouSure = GameObject.Find("Are You Sure?");
        AreYouSure.SetActive(false);
        currentHealth = maxHealth;
    }

    void Update()
    {   
        // Check if the enemy is defeated
        if (currentHealth <= 0 && !ended)
        {
            StartCoroutine(Die());
            ended = true;
        }
    }

    public IEnumerator Die()
    {
        Debug.Log("wyrm dead");
        GetComponent<WrymBehavior>().enabled = false;
        deathSound.Play();
        yield return new WaitForSeconds(1f);
        // make ending nicer

        yield return StartCoroutine(endingManager.EndGame());

        gameObject.SetActive(false);
    }

    public bool CheckIfRed()
    {
        return isRed;
    }

    public IEnumerator FlashRed(Action coroutineEnded)
    {
        if (!isRed)
        {
            isRed = true;
            List<Material> OGMats = new();
            foreach (var bodyPart in bodyParts)
            {
                if(bodyPart)
                {
                    OGMats.Add(bodyPart.material);
                    bodyPart.material = red;
                }
            }
            yield return new WaitForSeconds(0.1f);
            for(int i = 0; i < bodyParts.Length; i++)
            {
                if(bodyParts[i] != null)
                {
                    bodyParts[i].material = OGMats[i];
                }
            }
            
            isRed = false;
            // Invoke the callback to notify that the coroutine has ended
            coroutineEnded?.Invoke();
        }
    }
}
