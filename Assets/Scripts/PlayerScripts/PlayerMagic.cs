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
    public Ability[] allAbilities;

    [Header("Keybinds")]
    public KeyCode E = KeyCode.E;
    public KeyCode fireKey = KeyCode.Mouse0;
    public KeyCode combatCameraMode = KeyCode.Mouse1;

    public int index = 0;
    public int damageModifier = 1;

    private Animator animator;

    public AudioClip magicSound;
    private AudioSource SpecialSounds;


    public void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        index = 3;

        // this is just for me so i can find the right ability to add
        allAbilities = new Ability[19];
        allAbilities[0] = gameObject.AddComponent<Null>();
        allAbilities[1] = gameObject.AddComponent<Light>();
        allAbilities[2] = gameObject.AddComponent<FireBlast>();
        allAbilities[3] = gameObject.AddComponent<Storm>();
        allAbilities[4] = gameObject.AddComponent<RadialBlast>();
        allAbilities[5] = gameObject.AddComponent<RadialFireBurst>();
        allAbilities[6] = gameObject.AddComponent<EarthSpike>();
        allAbilities[7] = gameObject.AddComponent<Wall>();
        allAbilities[8] = gameObject.AddComponent<Ice>();
        allAbilities[9] = gameObject.AddComponent<Shield>();
        allAbilities[10] = gameObject.AddComponent<Light>();
        allAbilities[11] = gameObject.AddComponent<Hail>();
        allAbilities[12] = gameObject.AddComponent<LightningSmites>();
        allAbilities[13] = gameObject.AddComponent<Heal>();
        allAbilities[14] = gameObject.AddComponent<EnergyBlast>();
        allAbilities[15] = gameObject.AddComponent<Sun>();
        allAbilities[16] = gameObject.AddComponent<HealingForceField>();
        allAbilities[17] = gameObject.AddComponent<DamageForceField>();
        allAbilities[18] = gameObject.AddComponent<Damage>();

        //this is the same but the SocketSkill script replaces the abilities with correct ones based on the skill tree
        abilities = new Ability[7];
        
        abilities[0] = allAbilities[0];
        abilities[1] = allAbilities[0];
        abilities[2] = allAbilities[14];
        abilities[3] = allAbilities[0];
        abilities[4] = allAbilities[9];
        abilities[5] = allAbilities[0];
        abilities[6] = allAbilities[0];

        SetAbilityUI();

        SpecialSounds = GameObject.Find("SpecialPlayerAudio").GetComponent<AudioSource>();
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
            if(currentAbility.Name != "Null")
            {
                SpecialSounds.clip = magicSound;
                SpecialSounds.Play();
                StartCoroutine(Wait());
            }
        }
    }

    public IEnumerator Wait()
    {
        Debug.Log("waiting");
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && currentAbility.pauseTime != 0.0f)
        {
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            animator.SetTrigger("Attackin");
        }
        else if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && currentAbility.pauseTime == 0.0f)
        {
            animator.SetTrigger("Attackin");
        }
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(currentAbility.Cast()); // Wait for ability casting to finish
        if (currentAbility.pauseTime != 0.0f)
        {
            StartCoroutine(PauseMovement(currentAbility.pauseTime-0.2f));
        }
    }

    public IEnumerator PauseMovement(float pauseTime)
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        yield return new WaitForSeconds(pauseTime);
        gameObject.GetComponent<PlayerMovement>().enabled = true;
        GameObject.Find("Player").GetComponent<PlayerMovement>().stepCoolDown = 0;
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

            // Check if the ability has been activated again while the cooldown is active
            if (elapsedTime >= cooldownLength)
            {
                // Reset the cooldown visual and exit the coroutine if the ability has been activated again
                cooldownImage.fillAmount = 0f;
                yield break;
            }

            yield return null;
        }

        cooldownImage.fillAmount = 0f;  // Ensure that the fill amount is set to zero after cooldown
    }
}

    public void SetAbilityUI()
    {
        for(int i = 0; i < AbilityUI.Length; i++)
        {
            string path = "UI/" + abilities[i].DefaultIcon;
            AbilityUI[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, AbilityUI[i].GetComponent<RectTransform>().anchoredPosition.y);
            if(AbilityUI[i].GetComponentInChildren<Image>().name == "Icon")
            {
                AbilityUI[i].GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(path);
            }
            AbilityUI[i].GetComponentsInChildren<Image>().FirstOrDefault(child => child.name == "Cooldown").sprite = Resources.Load<Sprite>(path);
        }
        AbilityUI[index].GetComponent<RectTransform>().anchoredPosition = new Vector2(-25, AbilityUI[index].GetComponent<RectTransform>().anchoredPosition.y);
        string path2 = "UI/" + abilities[index].ActivatedIcon;
        if(AbilityUI[index].GetComponentInChildren<Image>().name == "Icon")
        {
            AbilityUI[index].GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(path2);
        }
    }
}
