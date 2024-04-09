using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterTheCave : MonoBehaviour
{
    public PlayerMovement PM;
    public ThirdPersonCam Cam;
    public PlayerMagic Magic;

    public void GoToCave()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Cam.enabled = true;
        PM.enabled = true;
        Magic.enabled = true;
        SceneManager.LoadScene("CaveScene");
    }

    public void Coward()
    {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Cam.enabled = true;
        PM.enabled = true;
        Magic.enabled = true;
    }
}
