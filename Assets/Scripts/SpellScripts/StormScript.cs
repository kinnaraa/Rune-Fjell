using UnityEngine;

public class StormScript : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerModel");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y - 1.5f, player.transform.position.z);
        gameObject.transform.position = pos;
    }
}
