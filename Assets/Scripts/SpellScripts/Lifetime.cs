using UnityEngine;

public class Lifetime : MonoBehaviour
{
    public float lifetime;

    public void Start() 
    {
        Destroy(gameObject, lifetime);    
    }
    public void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject, 0.15f);
    }
}
