using UnityEngine;

public class Monolith : MonoBehaviour
{
    private Transform player;
    private MonolithManager MM;
    private bool Found;
    private GameObject text;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        MM = GameObject.Find("Monoliths").GetComponent<MonolithManager>();
        text = MM.text;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance < 2)
        {
            if(!Found)
            {
                MM.FoundMonoliths.Add(gameObject);
                text.GetComponent<MonolithTextFade>().StartFade();
                Found = true;
            }
        }
    }
}
