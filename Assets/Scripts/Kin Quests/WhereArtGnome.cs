using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhereArtGnome : MonoBehaviour
{
    public GameObject gnomesMeeting;
    public GameObject gnomesNormal;
    public GameObject gnomeMom;

    public Transform triggerCutsene;
    bool startedQuest = false;
    public GameObject Player;

    public GameObject mainCamera;

    public Transform targetPosition;
    public Quaternion targetRotation;
    public float moveDuration = 2f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition.position = new Vector3(-152.15f, 30.169f, -46.43f);
        Vector3 rotationAngles = new Vector3(7.651f, -59.951f, -3.315f);
        targetRotation = Quaternion.Euler(rotationAngles);

        // Store initial position and rotation
        initialPosition = mainCamera.transform.position;
        initialRotation = mainCamera.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (startedQuest && Vector3.Distance(Player.transform.position, triggerCutsene.transform.position) < 22.0f)
        {

            /*initialPosition = mainCamera.transform.position;
            initialRotation = mainCamera.transform.rotation;

            Debug.Log("In radius");

            elapsedTime += Time.deltaTime;

            if (elapsedTime < moveDuration)
            {
                float t = elapsedTime / moveDuration;

                // Interpolate position and rotation
                mainCamera.transform.position = Vector3.Lerp(initialPosition, targetPosition.position, t);
                mainCamera.transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, t);
            }
            else
            {
                // Ensure we reach the target position and rotation exactly
                mainCamera.transform.position = targetPosition.position;
                mainCamera.transform.rotation = targetRotation;
            }
            */
        }
    }

    public void StartQuest()
    {
        gnomesMeeting.SetActive(true);
        gnomeMom.SetActive(false);
        gnomesNormal.SetActive(false);
        startedQuest = true;
    }
}
