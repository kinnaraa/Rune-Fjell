using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagic : MonoBehaviour
{
    public Player player;
    public string currentSpell;
    
    [Header("Spell Scripts")]
    public FireballScript Fireball;
    public MagicMissleScript MagicMissle;

    [Header("Keybinds")]
    public KeyCode QAbility = KeyCode.Q;
    public KeyCode EAbility = KeyCode.E;
    public KeyCode fireKey = KeyCode.Mouse0;

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
            } 
            else if(currentSpell == "Fireball")
            {
                Fireball.CastFireball();
            }
            else
            {
                MagicMissle.CastMagicMissle();
            }
        }
    }
}
