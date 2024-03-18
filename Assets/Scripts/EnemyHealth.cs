
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private GameManager GM;
    public SkinnedMeshRenderer[] bodyParts;
    public Material red;

    private bool isRed = false;

    void Start()
    {
        currentHealth = maxHealth;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider collision) 
    {
        //Check if the colliding object is on a certain layer
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerAbilities"))
        {
            // Deduct health (you can adjust the amount)
            currentHealth -= 20;
            StartCoroutine(FlashRed());

            // Check if the enemy is defeated
            if (currentHealth <= 0)
            {
                Die();
            }
            Debug.Log(currentHealth);
        }
    }

    void Die()
    {
        // Handle death, such as playing an animation, spawning particles, etc.
        GM.FirstBatDead = true;
        Destroy(gameObject);
    }

    IEnumerator FlashRed()
    {
        if(!isRed)
        {
            isRed = true;
            List<Material> OGMats = new();
            foreach (var bodyPart in bodyParts)
            {
                OGMats.Add(bodyPart.material);
                bodyPart.material = red;
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
