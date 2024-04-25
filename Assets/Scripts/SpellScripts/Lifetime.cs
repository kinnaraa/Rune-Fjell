using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lifetime : MonoBehaviour
{
    public float lifetime;

    public void Start() 
    {
        Destroy(gameObject, lifetime);    
    }

    public void OnCollisionEnter(Collision collision)
    {   
        Destroy(gameObject);
    }
    public void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
