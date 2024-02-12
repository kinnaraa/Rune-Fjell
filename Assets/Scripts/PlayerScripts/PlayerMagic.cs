using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
            StartCoroutine(CooldownVisual(currentAbility.cooldown));
            Debug.Log(currentAbility.cooldown);
        }
    }

    public IEnumerator PauseMovement(float pauseTime)
    {
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        yield return new WaitForSeconds(pauseTime);
        gameObject.GetComponent<PlayerMovement>().enabled = true;
    }

    public IEnumerator CooldownVisual(float cooldownLength)
    {
        Image cooldownImage = AbilityUI[index]
            .GetComponentsInChildren<Image>()
            .FirstOrDefault(child => child.name == "Cooldown");

        if (cooldownImage != null)
        {
            float elapsedTime = 0f;

            while (elapsedTime < cooldownLength)
            {
                float fillAmount = 1 - (elapsedTime / cooldownLength);
                cooldownImage.fillAmount = fillAmount;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            cooldownImage.fillAmount = 0f;  // Ensure that the fill amount is set to zero after cooldown
        }
    }


    public void SetAbilityUI()
    {
        for(int i = 0; i < AbilityUI.Length; i++)
        {
            AbilityUI[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, AbilityUI[i].GetComponent<RectTransform>().anchoredPosition.y);
            string path = "UI/" + abilities[i].DefaultIcon;
            if(AbilityUI[i].GetComponentInChildren<Image>().name == "Icon")
            {
                AbilityUI[i].GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(path);
            }
        }
        AbilityUI[index].GetComponent<RectTransform>().anchoredPosition = new Vector2(-25, AbilityUI[index].GetComponent<RectTransform>().anchoredPosition.y);
        string path2 = "UI/" + abilities[index].ActivatedIcon;
        if(AbilityUI[index].GetComponentInChildren<Image>().name == "Icon")
        {
            AbilityUI[index].GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(path2);
        }
    }
}
