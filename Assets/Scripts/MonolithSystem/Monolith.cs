using UnityEngine;

public class Monolith : MonoBehaviour
{
    private Transform player;
    private MonolithManager MM;
    private bool Found;
    private GameObject text;
    private AudioSource AS;

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
                        AS.Play();
                    }
                }
            }
        }
    }
}
