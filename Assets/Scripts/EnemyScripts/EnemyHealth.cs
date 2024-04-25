
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private GameManager GM;
    public SkinnedMeshRenderer[] bodyParts;
    public MeshRenderer[] bodyPartsIce;
    public Material red;
    private AudioSource sounds;
    public AudioClip deathSound;
    public Coroutine flashingCoroutine;

    private bool isRed = false;

    void Start()
    {
        currentHealth = maxHealth;
        sounds = gameObject.GetComponent <AudioSource>();
        if(SceneManager.GetActiveScene().name == "MainScene")
        {
            GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
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
        // Handle death, such as playing an animation, spawning particles, etc.
        if(gameObject.name == "Bat(Clone)" && SceneManager.GetActiveScene().name == "MainScene")
        {
            GM.FirstBatDead = true;
        }
        GetComponent<EnemyMovement>().enabled = false;
        sounds.clip = deathSound;
        sounds.Play();
        Debug.Log(sounds.isPlaying);
        Debug.Log(sounds.volume);
        Debug.Log(sounds.clip);

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
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

            if (bodyPartsIce.Length != 0)
            {
                List<Material> originalMats = new();
                foreach (var bodyPart in bodyPartsIce)
                {
                    if (bodyPart)
                    {
                        originalMats.Add(bodyPart.material);
                        bodyPart.material = red;
                    }
                }
                yield return new WaitForSeconds(0.1f);
                for (int i = 0; i < bodyPartsIce.Length; i++)
                {
                    if (bodyPartsIce[i] != null)
                    {
                        bodyPartsIce[i].material = originalMats[i];
                    }
                }
            }
            else
            {
                List<Material> originalMats = new();
                foreach (var bodyPart in bodyParts)
                {
                    if (bodyPart)
                    {
                        originalMats.Add(bodyPart.material);
                        bodyPart.material = red;
                    }
                }
                yield return new WaitForSeconds(0.1f);
                for (int i = 0; i < bodyParts.Length; i++)
                {
                    if (bodyParts[i] != null)
                    {
                        bodyParts[i].material = originalMats[i];
                    }
                }
            }

            isRed = false; // Reset the red state after flashing
            // Invoke the callback to notify that the coroutine has ended
            coroutineEnded?.Invoke();
        }
        else
        {
            yield return new WaitForSeconds(0);
        }
    }
}
