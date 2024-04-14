using System.Collections;
using UnityEngine;
public class GnomeWander : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Transform destination;
    public float moveSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        destination = pointA;
    }

    // Update is called once per frame
    void Update()
    {
        // Moving forward
        transform.position = Vector3.Lerp(transform.position, destination.position, Time.deltaTime * moveSpeed);

        // Check if position is reached
        float dist = Vector3.Distance(transform.position, destination.position);

        if (dist <= 1)
        {
            // Changes position
            if (destination == pointA)
            {
                destination = pointB;
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                destination = pointA;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }
}
