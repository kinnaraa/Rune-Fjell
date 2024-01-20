using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagic : MonoBehaviour
{
    public Player player;
    public Ability currentSpell;
    
    [Header("Abilities")]
    public Ability[] abilities;

    [Header("Keybinds")]
    public KeyCode Q = KeyCode.Q;
    public KeyCode E = KeyCode.E;
    public KeyCode combatCameraMode = KeyCode.Mouse1;

    private int index = 0;

    public void Update() 
    {
        currentSpell = abilities[index];

        if(Input.GetKey(combatCameraMode))
        {  
            if(Input.GetKeyDown(Q))
            {
                currentSpell.Cast();
                StartCoroutine(PauseMovement());
            }
        }
        else
        {
            if(Input.GetKeyDown(Q))
            {
                if(index == abilities.Length)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
            }
            else if(Input.GetKeyDown(E))
            {
                if(index == 0)
                {
                    index = abilities.Length;
                }
                else
                {
                    index--;
                }
            }
        }
    }

    public IEnumerator PauseMovement()
    {
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<PlayerMovement>().enabled = true;
    }
}
