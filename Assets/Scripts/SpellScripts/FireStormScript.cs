using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireStormScript : MonoBehaviour
{
    public GameObject FireStorm;
    public float cooldown;
    private bool cooldownActive = false;
    public bool isInFireStorm = false;

    public IEnumerator CastFireStorm()
    {
        if(!cooldownActive)
        {
            isInFireStorm = true;
            FireStorm.GetComponent<ParticleSystem>().Play();
            FireStorm.GetComponent<BoxCollider>().enabled = true;
            gameObject.GetComponent<PlayerMovement>().moveSpeed = gameObject.GetComponent<PlayerMovement>().moveSpeed + 5;
            yield return new WaitForSeconds(10f);
            gameObject.GetComponent<PlayerMovement>().moveSpeed = gameObject.GetComponent<PlayerMovement>().moveSpeed - 5;
            FireStorm.GetComponent<ParticleSystem>().Stop();
            FireStorm.GetComponent<BoxCollider>().enabled = false;
            isInFireStorm = false;
            StartCoroutine(Cooldown());
        }
        else
        {
            yield break;
        }
    }
    public IEnumerator Cooldown()
    {
        cooldownActive = true;
        yield return new WaitForSeconds(cooldown);
        cooldownActive = false;
    }
}
