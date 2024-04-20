using System.Collections.Generic;
using UnityEngine;

public class MonolithManager : MonoBehaviour
{
    public GameObject text;
    public GameObject[] Monoliths = new GameObject[20];
    public List<GameObject> FoundMonoliths = new();

    public bool raidoUnlocked = false;
    public bool ehwazUnlocked = false;
    public bool halagazUnlocked = false;
    public bool isaUnlocked = false;
    public bool sowiloUnlocked = false;

    private void Update()
    {
        foreach(var monolith in FoundMonoliths)
        {
            if (monolith.name == "Sowilo")
            {
                sowiloUnlocked = true;
            }
            else if (monolith.name == "Ehwaz")
            {
                ehwazUnlocked = true;
            }
            else if (monolith.name == "Halagaz")
            {
                halagazUnlocked = true;
            }
            else if (monolith.name == "Isa")
            {
                isaUnlocked = true;
            }
            else if (monolith.name == "Raidho")
            {
                raidoUnlocked = true;
            }
        }

        
    }
}
