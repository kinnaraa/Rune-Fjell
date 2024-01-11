using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBlastScript : MonoBehaviour
{
    public ParticleSystem fireBlast;
    public float cooldown;

    [Header("Keybinds")]
    public KeyCode fireKey = KeyCode.Mouse0;

    public bool cooldownActive = false;
    public bool usingFireBlast = false;
    private bool isPlaying = false; 

    public void Update()
    {
        isPlaying = fireBlast.isPlaying;
        if(usingFireBlast)
        {
            if (!cooldownActive)
            {
                if (Input.GetKey(fireKey))
                {
                    if(!isPlaying)
                    {
                        fireBlast.Play();
                    }
                }
                else
                {
                    fireBlast.Stop();
                }
            }
        }
    }

    public IEnumerator CastFireBlast()
    {
        usingFireBlast = true;
        yield return new WaitForSeconds(4f);
        usingFireBlast = false;
        fireBlast.Stop();
        StartCoroutine(Cooldown());
    }
    public IEnumerator Cooldown()
    {
        cooldownActive = true;
        usingFireBlast = false;
        yield return new WaitForSeconds(cooldown);
        cooldownActive = false;
    }
}
