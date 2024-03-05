using UnityEngine;

public class RotateText : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Assuming your canvas is on the main camera. If it's on a different camera, replace with the correct camera reference.
        mainCamera = Camera.main;

        // Call the function to update rotation every frame
        UpdateRotation();
    }

    void Update()
    {
        // Call the function to update rotation every frame
        UpdateRotation();
    }

    void UpdateRotation()
    {
        // Check if the main camera is available
        if (mainCamera != null)
        {
            // Make the UI object face the camera
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                mainCamera.transform.rotation * Vector3.up);
        }
    }
}