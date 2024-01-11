using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagic : MonoBehaviour
{
    public Player player;
    public string currentSpell;
    
    [Header("Spell Scripts")]
    public MagicMissleScript MagicMissle;
    public FireballScript Fireball;
    public FireBlastScript FireBlast;

    [Header("Keybinds")]
    public KeyCode QAbility = KeyCode.Q;
    public KeyCode EAbility = KeyCode.E;
    public KeyCode fireKey = KeyCode.Mouse0;

    public void Start() 
    {
        MagicMissle = gameObject.GetComponent<MagicMissleScript>();
        Fireball = gameObject.GetComponent<FireballScript>();
        FireBlast = gameObject.GetComponent<FireBlastScript>();
    }

    public void Update() 
    {
        if(Input.GetKey(QAbility))
        {
            currentSpell = player.QAbility;
        }
        else if(Input.GetKey(EAbility))
        {
            currentSpell = player.EAbility;
        }

        if(Input.GetKey(fireKey))
        {  
            if(currentSpell == "MagicMissle")
            {
                MagicMissle.CastMagicMissle();
                StartCoroutine(PauseMovement());
            } 
            else if(currentSpell == "Fireball")
            {
                Fireball.CastFireball();
                StartCoroutine(PauseMovement());
            }
            else if(currentSpell == "FireBlast")
            {
                if(FireBlast.usingFireBlast == false && !FireBlast.cooldownActive)
                {
                    StartCoroutine(FireBlast.CastFireBlast());
                    StartCoroutine(PauseMovement());
                }
            }
            else
            {
                MagicMissle.CastMagicMissle();
                StartCoroutine(PauseMovement());
            }
        }
    }

    public IEnumerator PauseMovement()
    {
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        yield return new WaitForSeconds(0.25f);
        gameObject.GetComponent<PlayerMovement>().enabled = true;
    }
}
