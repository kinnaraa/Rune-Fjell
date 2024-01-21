using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMagic : MonoBehaviour
{
    public Player player;
    public Ability currentAbility;
    public GameObject[] AbilityUI;

    [Header("Prefabs")]
    public GameObject iceBallPrefab;
    
    [Header("Abilities")]
    public Ability[] abilities;

    [Header("Keybinds")]
    public KeyCode Q = KeyCode.Q;
    public KeyCode E = KeyCode.E;
    public KeyCode combatCameraMode = KeyCode.Mouse1;

    public int index = 0;
    

    public void Start()
    {
        index = 3;

        abilities = new Ability[7];
        abilities[0] = gameObject.AddComponent<Storm>();
        abilities[1] = gameObject.AddComponent<Ice>();
        abilities[2] = gameObject.AddComponent<Storm>();
        abilities[3] = gameObject.AddComponent<Storm>();
        abilities[4] = gameObject.AddComponent<Storm>();
        abilities[5] = gameObject.AddComponent<Storm>();
        abilities[6] = gameObject.AddComponent<Storm>();

        SetAbilityUI();
    }

    public void Update() 
    {
        currentAbility = abilities[index];

        if(Input.GetKey(combatCameraMode))
        {  
            if(Input.GetKeyDown(Q))
            {
                StartCoroutine(currentAbility.Cast());
                StartCoroutine(PauseMovement());
            }
        }
        else
        {
            if(Input.GetKeyDown(Q))
            {
                if(index == abilities.Length - 1)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
                SetAbilityUI();
            }
            else if(Input.GetKeyDown(E))
            {
                if(index == 0)
                {
                    index = abilities.Length - 1;
                }
                else
                {
                    index--;
                }
                SetAbilityUI();
            }
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
