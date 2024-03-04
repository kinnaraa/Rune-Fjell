using System.Collections;
using Cinemachine;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlayerUI;
    public GameObject Camera;
    public GameObject Portal;

    void Start()
    {
        StartCoroutine(OnStart());
    }

    public IEnumerator OnStart()
    {
        yield return new WaitForSeconds(1f);
        Player.SetActive(true);
        yield return new WaitForSeconds(2f);
        Player.GetComponent<Rigidbody>().useGravity = true;
        Player.GetComponent<PlayerMovement>().enabled = true;
        Camera.GetComponent<CinemachineBrain>().enabled = true;
        PlayerUI.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        Portal.SetActive(false);
    }
}
