using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispSystem : MonoBehaviour
{
    public GameManager GM;
    public GameObject Wisp;
    public GameObject Player;
    public GameObject Gnome;
    public Transform[] FirstSetOfPoints;
    private bool firstPath = false;
    private bool OnFirstPath = false;
    private GameObject CurrentWisp;
    private int index = 1;

    public void Update()
    {
        if(firstPath)
        {
            CurrentWisp = Instantiate(Wisp, FirstSetOfPoints[0].position, FirstSetOfPoints[0].rotation);
            firstPath = false;
            OnFirstPath = true;
        }

        if(OnFirstPath)
        {
            float distance = Vector3.Distance(Player.transform.position, CurrentWisp.transform.position);
            //Debug.Log(distance);
            if(distance <= 4)
            {
                Destroy(CurrentWisp);
                CurrentWisp = Instantiate(Wisp, FirstSetOfPoints[index].position, FirstSetOfPoints[index].rotation);
                index++;
            }
            if(index == FirstSetOfPoints.Length + 1)
            {
                OnFirstPath = false;
            }
        }
        else
        {
            float distance = Vector3.Distance(Player.transform.position, Gnome.transform.position);
            if(distance <= 10)
            {
                StartCoroutine(GM.SaveTheGnome());
            }
        }
    }

    public void StartFirstPath()
    {
        firstPath = true;
    }
}
