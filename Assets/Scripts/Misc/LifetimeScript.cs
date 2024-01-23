using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifetimeScript : MonoBehaviour
{
    public float lifetime = 5f; // Lifetime in seconds

    void Start()
    {
        // Invoke the DestroySelf method after the specified lifetime
        Invoke("DestroySelf", lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Destroy the GameObject if it collides with something
        DestroySelf();
    }

    void DestroySelf()
    {
        // Destroy the GameObject this script is attached to
        Destroy(gameObject);
    }
}
