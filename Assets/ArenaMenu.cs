using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEditorInternal;
using UnityEngine;

public class ArenaMenu : MonoBehaviour
{
    public PlayerMovement PM;
    public ThirdPersonCam Cam;
    public PlayerMagic Magic;

    public GameObject Bat;
    public GameObject IceCreature;
    public GameObject Wyrm;

    public Transform[] Spawns;

    private int numOfBats;
    private int numOfIceCreatures;
    private int numOfWyrms;

    public TextMeshProUGUI batCount;
    public TextMeshProUGUI iceCreatureCount;
    public TextMeshProUGUI wyrmCount;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Cam.enabled = false;
        PM.enabled = false;
        Magic.enabled = false;
    }
    public void Update()
    {
        batCount.text = numOfBats.ToString();
        iceCreatureCount.text = numOfIceCreatures.ToString();
        wyrmCount.text = numOfWyrms.ToString();
    }

    public void AddBats()
    {
        numOfBats++;
    }
    public void AddIceCreatures()
    {
        numOfIceCreatures++;
    }
    public void AddWyrms()
    {
        numOfWyrms++;
    }

    public void RemoveBats()
    {
        if(numOfBats !< 0)
            numOfBats--;
    }
    public void RemoveIceCreatures()
    {
        if(numOfIceCreatures !< 0)
            numOfIceCreatures--;
    }
    public void RemoveWyrms()
    {
        if(numOfWyrms !< 0)
            numOfWyrms--;
    }

    public void Spawn()
    {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Cam.enabled = true;
        PM.enabled = true;
        Magic.enabled = true;

        for(int i = 0; i < numOfBats; i++)
        {
            Instantiate(Bat, GetRandomSpawnPosition(), Quaternion.identity);
        }
        for(int i = 0; i < numOfIceCreatures; i++)
        {
            Instantiate(IceCreature, GetRandomSpawnPosition(), Quaternion.identity);
        }
        for(int i = 0; i < numOfWyrms; i++)
        {
            Instantiate(Wyrm, GetRandomSpawnPosition(), Quaternion.identity);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        int randomIndex = Random.Range(0, Spawns.Length);
        return Spawns[randomIndex].position;
    }
}
