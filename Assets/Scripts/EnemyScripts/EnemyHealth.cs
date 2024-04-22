
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
        sounds.Play();
        yield return new WaitForSeconds(sounds.clip.length);
        sounds.Stop();
        Destroy(gameObject);
    }

    public bool checkIfRed()
    {
        return isRed;
    }

    public IEnumerator FlashRed()
    {
        //Debug.Log(currentHealth);
        if(!isRed)
        {
            if(bodyPartsIce.Length != 0)
            {
                isRed = true;
                List<Material> OGMats = new();
                foreach (var bodyPart in bodyPartsIce)
                {
                    if(bodyPart)
                    {
                        OGMats.Add(bodyPart.material);
                        bodyPart.material = red;
                    }
                }
                yield return new WaitForSeconds(0.5f);
                for(int i = 0; i < bodyPartsIce.Length; i++)
                {
                    bodyPartsIce[i].material = OGMats[i];
                }
                isRed = false;
            }
            else
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
                yield return new WaitForSeconds(0.5f);
                for(int i = 0; i < bodyParts.Length; i++)
                {
                    if(bodyParts[i] != null)
                    {
                        bodyParts[i].material = OGMats[i];
                    }
                }
                isRed = false;
            }
        }
    }
}
