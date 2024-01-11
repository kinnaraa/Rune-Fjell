using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStormScript : MonoBehaviour
{
    public GameObject Player;
    public GameObject FireStorm;
    public float cooldown;
    private bool cooldownActive = false;
    public bool isInFireStorm = false;

    public void Update()
    {
        FireStorm.transform.position = new Vector3(Player.transform.position.x, FireStorm.transform.position.y, Player.transform.position.z);
    }
    public IEnumerator CastFireStorm()
    {
        if(!cooldownActive && gameObject.GetComponent<PlayerMovement>().grounded)
        {
            isInFireStorm = true;
            gameObject.GetComponent<PlayerMovement>().readyToJump = false;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            StartCoroutine(Lift(0.25f));
            FireStorm.GetComponent<ParticleSystem>().Play();
            FireStorm.GetComponent<BoxCollider>().enabled = true;
            gameObject.GetComponent<PlayerMovement>().moveSpeed = gameObject.GetComponent<PlayerMovement>().moveSpeed + 5;

            yield return new WaitForSeconds(10f);

            StartCoroutine(Lower(0.25f));
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.GetComponent<PlayerMovement>().readyToJump = true;
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

    public IEnumerator Lift(float duration)
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = Player.transform.position;
        Vector3 targetPosition = initialPosition + Vector3.up * 0.5f;

        while (elapsedTime < duration)
        {
            Player.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the player is exactly at the target position
        Player.transform.position = targetPosition;
    }

    public IEnumerator Lower(float duration)
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = Player.transform.position;
        Vector3 targetPosition = initialPosition - Vector3.up * 0.5f;

        while (elapsedTime < duration)
        {
            Player.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the player is exactly at the target position
        Player.transform.position = targetPosition;
    }

    public IEnumerator Cooldown()
    {
        cooldownActive = true;
        yield return new WaitForSeconds(cooldown);
        cooldownActive = false;
    }
}
