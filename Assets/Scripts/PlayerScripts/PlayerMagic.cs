using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PlayerMagic : MonoBehaviour
{
    public Player player;
    public Ability currentAbility;
    public GameObject[] AbilityUI;
    
    [Header("Abilities")]
    public Ability[] abilities;

    [Header("Keybinds")]
    public KeyCode E = KeyCode.E;
    public KeyCode fireKey = KeyCode.Q;
    public KeyCode combatCameraMode = KeyCode.Mouse1;

    public int index = 0;

    public void Start()
    {
        index = 3;

        abilities = new Ability[7];
        abilities[0] = gameObject.AddComponent<Shield>();
        abilities[1] = gameObject.AddComponent<Shield>();
        abilities[2] = gameObject.AddComponent<Shield>();
        abilities[3] = gameObject.AddComponent<Shield>();
        abilities[4] = gameObject.AddComponent<Shield>();
        abilities[5] = gameObject.AddComponent<Shield>();
        abilities[6] = gameObject.AddComponent<Shield>();

        SetAbilityUI();
    }

    public void Update() 
    {
        currentAbility = abilities[index];

        // Use the scroll wheel to change the index
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel < 0f)
        {
            // Scroll up
            index = (index + 1) % abilities.Length;
            SetAbilityUI();
        }
        else if (scrollWheel > 0f)
        {
            // Scroll down
            index = (index - 1 + abilities.Length) % abilities.Length;
            SetAbilityUI();
        }

        //Cast Ability
        if(Input.GetKey(fireKey))
        {
            StartCoroutine(currentAbility.Cast());
            StartCoroutine(PauseMovement(currentAbility.pauseTime));
        }
    }

    public IEnumerator PauseMovement(float pauseTime)
    {
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        yield return new WaitForSeconds(pauseTime);
        gameObject.GetComponent<PlayerMovement>().enabled = true;
    }

    public void SetAbilityUI()
    {
        for(int i = 0; i < AbilityUI.Length; i++)
        {
            //UI/Algiz_Default
            //UI/Algiz_Default.PNG
            AbilityUI[i].GetComponentInChildren<Image>().sprite = Resources.Load("UI/" + abilities[i].DefaultIcon + ".PNG") as Sprite;
            Debug.Log("UI/" + abilities[i].DefaultIcon);
            AbilityUI[i].transform.position.Set(AbilityUI[i].transform.position.x - 50, AbilityUI[i].transform.position.y, AbilityUI[i].transform.position.z);
        }
        AbilityUI[index].GetComponentInChildren<Image>().sprite = Resources.Load("UI/" + abilities[index].ActivatedIcon + ".PNG") as Sprite;
        AbilityUI[index].transform.position.Set(AbilityUI[index].transform.position.x + 50, AbilityUI[index].transform.position.y, AbilityUI[index].transform.position.z);
    }
}
