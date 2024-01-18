using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    public GameObject Player;
    public ParticleSystem teleport;
    public float cooldown;
    public Transform TeleportPositon;

    [Header("Keybinds")]
    public KeyCode fireKey = KeyCode.Mouse0;

    public bool cooldownActive = false;
    public void Start()
    {
        TeleportPositon = Player.transform;
    }

    public IEnumerator CastTeleport()
    {
        yield return new WaitForSeconds(1f);
        if(TeleportPositon != null)
        {
            Player.transform.position = TeleportPositon.position;
            Player.transform.rotation = TeleportPositon.rotation;
        }
        
        StartCoroutine(Cooldown());
    }
    public IEnumerator Cooldown()
    {
        cooldownActive = true;
        yield return new WaitForSeconds(cooldown);
        cooldownActive = false;
    }
}
