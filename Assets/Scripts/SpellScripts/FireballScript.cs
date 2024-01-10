using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebalScript : MonoBehaviour
{
   public GameObject fireball;
   public int speed;
   public float cooldown;
   public GameObject firingPoint;
   private bool cooldownActive = false;

   [Header("Keybinds")]
    public KeyCode fireKey = KeyCode.Mouse0;

    public void Update()
    {
        if(Input.GetKey(fireKey) && !cooldownActive)
        {
            // Instantiate the fireball at the firing point position and rotation
            GameObject newFireball = Instantiate(fireball, firingPoint.transform.position, firingPoint.transform.rotation);
            // Access the Rigidbody component of the instantiated fireball
            Rigidbody fireballRb = newFireball.GetComponent<Rigidbody>();
            // Set the velocity of the fireball in the forward direction of the firing point
            fireballRb.velocity = firingPoint.transform.forward * speed;

            StartCoroutine(Cooldown());
        }
    }

    public IEnumerator Cooldown()
    {
        cooldownActive = true;
        yield return new WaitForSeconds(cooldown);
        cooldownActive = false;
    }
}
