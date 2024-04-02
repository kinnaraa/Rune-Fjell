
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private GameManager GM;
    public SkinnedMeshRenderer[] bodyParts;
    public MeshRenderer[] bodyPartsIce;
    public Material red;

    private bool isRed = false;

    void Start()
    {
        currentHealth = maxHealth;
        //GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {   
        // Check if the enemy is defeated
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle death, such as playing an animation, spawning particles, etc.
        //GM.FirstBatDead = true;
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
            if(bodyParts.Length == 0)
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
                    bodyParts[i].material = OGMats[i];
                }
                isRed = false;
            }
        }
    }
}
