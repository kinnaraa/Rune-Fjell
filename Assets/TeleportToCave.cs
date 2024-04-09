using UnityEngine;

public class TeleportToCave : MonoBehaviour
{
    public GameObject AreYouSure; 
    public PlayerMovement PM;
    public ThirdPersonCam Cam;
    public PlayerMagic Magic;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            AreYouSure.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Cam.enabled = true;
            PM.enabled = true;
            Magic.enabled = true;
        }
    }
}
