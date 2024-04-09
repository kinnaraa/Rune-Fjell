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
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            PM.enabled = false;
            Magic.enabled = false;
            Cam.enabled = false;
        }
    }
}
