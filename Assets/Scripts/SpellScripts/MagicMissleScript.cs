using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissleScript : MonoBehaviour
{
    public GameObject magicMissle;
    public int speed;
    public float cooldown;
    public GameObject firingPoint;
    private bool cooldownActive = false;

    public void CastMagicMissle()
    {
        if(!cooldownActive)
        {
            // Instantiate the fireball at the firing point position and rotation
            GameObject newMagicMissle = Instantiate(magicMissle, firingPoint.transform.position, firingPoint.transform.rotation);
            // Access the Rigidbody component of the instantiated fireball
            Rigidbody magicMissleRB = newMagicMissle.GetComponent<Rigidbody>();
            // Set the velocity of the fireball in the forward direction of the firing point
            magicMissleRB.velocity = firingPoint.transform.forward * speed;

            StartCoroutine(Cooldown());
        }
        else
        {
            return;
        }
    }

    public IEnumerator Cooldown()
    {
        cooldownActive = true;
        yield return new WaitForSeconds(cooldown);
        cooldownActive = false;
    }
}
