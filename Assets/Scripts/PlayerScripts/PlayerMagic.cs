using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        abilities[0] = gameObject.AddComponent<Storm>();
        abilities[1] = gameObject.AddComponent<Ice>();
        abilities[2] = gameObject.AddComponent<FireBlast>();
        abilities[3] = gameObject.AddComponent<Storm>();
        abilities[4] = gameObject.AddComponent<Storm>();
        abilities[5] = gameObject.AddComponent<Storm>();
        abilities[6] = gameObject.AddComponent<Storm>();

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
        if(Input.GetKeyDown(fireKey))
        {
            StartCoroutine(currentAbility.Cast());
            StartCoroutine(PauseMovement());
        }
    }

    public IEnumerator PauseMovement()
    {
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<PlayerMovement>().enabled = true;
    }

    public void SetAbilityUI()
    {
        for(int i = 0; i < AbilityUI.Length; i++)
        {
            AbilityUI[i].GetComponentInChildren<Image>().color = Color.white;
            AbilityUI[i].GetComponentInChildren<TextMeshProUGUI>().text = abilities[i].Name;
            AbilityUI[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        }
        AbilityUI[index].GetComponentInChildren<Image>().color = Color.grey;
        AbilityUI[index].GetComponentInChildren<TextMeshProUGUI>().color = Color.yellow;
    }
}
