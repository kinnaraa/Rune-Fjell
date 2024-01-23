using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
   private Transform firingPoint;
    void Start()
    {
        firingPoint = GameObject.Find("FiringPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(firingPoint.transform.position.x, firingPoint.transform.position.y, firingPoint.transform.position.z);
        gameObject.transform.position = pos;
    }
}
