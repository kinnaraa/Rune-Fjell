
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WyrmHealth : MonoBehaviour
{
    public GameObject Wrym;
    public int maxHealth = 100;
    public int currentHealth;
    public SkinnedMeshRenderer[] bodyParts;
    public Material red;
    public AudioSource deathSound;
    public Coroutine flashingCoroutine;

    private bool isRed = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {   
        // Check if the enemy is defeated
        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public IEnumerator Die()
    {
        GetComponent<WrymBehavior>().enabled = false;
        deathSound.Play();
        yield return new WaitForSeconds(3f);
        Destroy(Wrym);
        
        GameObject.Find("Player").SetActive(false);
        SceneManager.LoadScene("Credits");
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
