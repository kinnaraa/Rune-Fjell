using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMenu : MonoBehaviour
{
    public List<GameObject> MonolithButtons = new List<GameObject>(); // Initialize as a list
    private MonolithManager MM;
    private GameObject player;

    private PlayerMovement PM;
    private ThirdPersonCam Cam;
    private PlayerMagic Magic;

    public GameObject tabMenu;

    private void Awake() 
    {
        MM = GameObject.Find("Monoliths").GetComponent<MonolithManager>();
        player = GameObject.Find("Player");

        PM = GameObject.Find("Player").GetComponent<PlayerMovement>();
        Cam = GameObject.Find("Main Camera").GetComponent<ThirdPersonCam>();
        Magic = GameObject.Find("Player").GetComponent<PlayerMagic>();
    }

    private void OnEnable() 
    {
        if (MM.FoundMonoliths.Count != 0)
        {
            foreach (GameObject obj in MM.FoundMonoliths)
            {
                foreach (GameObject button in MonolithButtons)
                {
                    if(button != null && button.name == obj.name)
                    {
                        button.SetActive(true);
                    }
                }
            }
        }
    }

    // Modify Teleport to take the name of the button as a parameter
    public void Teleport(string buttonName)
    {
        foreach (GameObject obj in MM.FoundMonoliths)
        {
            if(buttonName == obj.name && MM.raidoUnlocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Cam.enabled = true;
                PM.enabled = true;
                Magic.enabled = true;

                Vector3 SpawnPos = player.transform.position;
                SpawnPos.y -= 1;
                GameObject particles = Instantiate(Resources.Load<GameObject>("SpellPrefabs/TeleportParticles"), SpawnPos, Resources.Load<Transform>("SpellPrefabs/TeleportParticles").rotation); // Use Quaternion.identity for rotation
                particles.transform.parent = player.transform;
                particles.GetComponentInChildren<PortalAnimationForTeleport>().Animate(obj);

                // close tab menu
                tabMenu.SetActive(false);
            }
        }
    }
}