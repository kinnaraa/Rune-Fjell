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
        abilities[0] = gameObject.AddComponent<Wall>();
        abilities[1] = gameObject.AddComponent<Hail>();
        abilities[2] = gameObject.AddComponent<LightningSmites>();
        abilities[3] = gameObject.AddComponent<RadialFireBurst>();
        abilities[4] = gameObject.AddComponent<EarthSpike>();
        abilities[5] = gameObject.AddComponent<Shield>();
        abilities[6] = gameObject.AddComponent<ForceField>();

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
            AbilityUI[i].GetComponentInChildren<Image>().color = Color.white;
            AbilityUI[i].GetComponentInChildren<TextMeshProUGUI>().text = abilities[i].Name;
            AbilityUI[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        }
        AbilityUI[index].GetComponentInChildren<Image>().color = Color.grey;
        AbilityUI[index].GetComponentInChildren<TextMeshProUGUI>().color = Color.yellow;
    }
}
