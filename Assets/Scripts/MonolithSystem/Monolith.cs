using UnityEngine;
using System.Linq;

public class Monolith : MonoBehaviour
{
    private Transform player;
    private MonolithManager MM;
    private bool Found;
    private GameObject text;
    private AudioSource AS;

    public bool raidoUnlocked = false;
    public bool ehwazUnlocked = false;
    public bool halagazUnlocked = false;
    public bool isaUnlocked = false;
    public bool sowiloUnlocked = false;

    public newSkillTree skillTree;

    void Start()
    {
        MM = GameObject.Find("Monoliths").GetComponent<MonolithManager>();
        AS = GetComponent<AudioSource>(); 
        text = MM.text;
    }

    void Update()
    {
        if(GameObject.Find("Player"))
        {
            player = GameObject.Find("Player").transform;
            if(player)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if(distance < 2)
                {
                    if(!Found)
                    {
                        MM.FoundMonoliths.Add(gameObject);
                        text.GetComponent<MonolithTextFade>().StartFade();
                        Found = true;
                        // unlock monolith in skill tree
                        if(gameObject.name == "Sowilo")
                        {
                            sowiloUnlocked = true;
                        }else if (gameObject.name == "Ehwaz")
                        {
                            ehwazUnlocked = true;
                        }else if (gameObject.name == "Halagaz")
                        {
                            halagazUnlocked = true;
                        }else if (gameObject.name == "Isa")
                        {
                            isaUnlocked = true;
                        }else if (gameObject.name == "Raidho")
                        {
                            raidoUnlocked = true;
                        }

                        AS.Play();
                    }
                }
            }
        }
    }
}
