using UnityEngine;

public class ResetGame : MonoBehaviour
{
    public void Start()
    {
        if(GameObject.Find("Player"))
        {
            Destroy(GameObject.Find("Player"));
        }
        if(GameObject.Find("CombatCameraController"))
        {
            Destroy(GameObject.Find("CombatCameraController"));
        }
        if(GameObject.Find("DefaultCameraController"))
        {
            Destroy(GameObject.Find("DefaultCameraController"));
        }
        if(GameObject.Find("TabMenu"))
        {
            Destroy(GameObject.Find("TabMenu"));
        }
        if(GameObject.Find("EscMenu"))
        {
            Destroy(GameObject.Find("EscMenu"));
        }
        if(GameObject.Find("Are You Sure?"))
        {
            Destroy(GameObject.Find("Are You Sure?"));
        }
    }
}
