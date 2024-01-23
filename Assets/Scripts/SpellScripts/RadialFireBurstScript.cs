using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialFireBurstScript : MonoBehaviour
{
    private Transform Player;
    void Start()
    {
        Player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(Player.transform.position.x, Player.transform.position.y - 0.5f, Player.transform.position.z);
        gameObject.transform.position = pos;
    }
}
