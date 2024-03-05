using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;
using System.IO;

public class newSkillTree : MonoBehaviour
{
    public List<List<Skill>> skillList;
    private List<Skill> attacksList;
    private List<Skill> utilityList;
    private List<Skill> passiveList;

    private GameObject skillType;
    private Image skillImage;
    private TMP_Text skillPointsText;
    private int skillPoints = 20;

    public bool choseSkill = false;
    public bool socketing = false;
    public int index = 0;
    public string skillName;

    public Skill chosenSkill;

    public string chosenAbilityName;
    public PlayerMagic playerMagic;
  
    public GameObject infoSection;
    public GameObject playerMagicGO;

    public class Skill
    {
        public string name;
        public string displayName;
        public Sprite sprite;
        public bool isRune;
        public bool unlocked;
        public string infoBlurb;

        public Skill(string skillName, string displayname, bool rune)
        {
            name = skillName;
            displayName = displayname;
            sprite = Resources.Load<Sprite>("UI/Algiz_Default");
            isRune = rune;
            unlocked = false;
            infoBlurb = name + "\n\nBlurb about ability and what it does.\n\n...\n\n Damage: \n\nCooldown: \n\n";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        attacksList = new List<Skill>()
        {
            new Skill("Fire Blast", "Sowilo", true),
            new Skill("Radial Fire Burst", "Radial Fire Blast", false),
            new Skill("Energy Blast", "Thurisaz", true),
            new Skill("Lightning Smites", "Lightning Smites", false),
            new Skill("Storm", "Halagaz", true),
            new Skill("Hail", "Hail", false),
            new Skill("Ice", "Isa", true),
            new Skill("Wall", "Wall", false),
            new Skill("Earth Spike", "Ehwaz", true),
        };

        utilityList = new List<Skill>()
        {
            new Skill("Ansuz", "Ansuz", true),
            new Skill("Heal", "Wunjo", true),
            new Skill("Heal in Forcefield", "Heal in Forcefield", false),
            new Skill("Shield", "Algiz", true),
            new Skill("Damage in Forcefield", "Damage in Forcefield", false),
            new Skill("Uruz", "Uruz", true),
            new Skill("Light", "Kenaz", true),
        };

        passiveList = new List<Skill>()
        {
            new Skill("Nauthiz", "Nauthiz", true),
            new Skill("Raidho", "Raidho", true),
            new Skill("Perthro", "Perthro", true),
        };

        skillList = new List<List<Skill>>
        {
            attacksList, utilityList, passiveList,
        };

        for (int i = 0; i < 3; i++)
        {
            skillType = transform.GetChild(i+1).gameObject;
            for (int j = 0; j < skillList[i].Count; j++)
            {
                skillImage = skillType.transform.GetChild(j).GetComponent<Image>();
                skillImage.sprite = skillList[i][j].sprite;
                skillImage.name = skillList[i][j].name;
                if(skillList[i][j].name == "Energy Blast" || skillList[i][j].name == "Shield")
                {
                    skillList[i][j].unlocked = true;
                }
                if (!skillList[i][j].unlocked)
                {
                    string path = "UI/" + skillList[i][j].displayName + "_Default";
                    skillImage.sprite = Resources.Load<Sprite>(path);
                }
                else
                {
                    string path = "UI/" + skillList[i][j].displayName + "_Activated";
                    skillImage.sprite = Resources.Load<Sprite>(path);
                }
            }
        }
        skillPointsText = transform.GetChild(4).GetComponent<TMP_Text>();

        infoSection = transform.GetChild(6).gameObject;

        /*
        allAbilities = new List<Ability>
        {
            new Ability("Halagaz_Default", "Halagaz_Activated", 10, 1, "Storm", 1),
            new Ability("Isa_Default","Isa_Activated", 10, 1, "Ice", 1),
            new Ability("Sowilo_Default", "Sowilo_Activated", 10, 1, "Fire Blast", 1.5f),
            new Ability("ThurisazSowilo_Default", "ThurisazSowilo_Activated", 10, 1, "Radial Fire Burst", 5.5f),
            new Ability("Ehwaz_Default", "Ehwaz_Activated", 10, 1, "Earth Spike", 1.5f),
            new Ability("Algiz_Default", "Algiz_Activated", 0, 0, "Shield", 0f),
            new Ability("Kennaz_Default", "Kennaz_Activated", 0, 0, "Light", 0f),
            new Ability("IsaEhwaz_Default", "IsaEhwaz_Activated", 0, 0, "Wall", 0f),
            new Ability("IsaHalagaz_Default", "IsaHalagaz_Activated", 0, 5, "Hail", 0f),
            new Ability("ThurisazHalagaz_Default", "ThurisazHalagaz_Activated", 0, 5, "Lightning Smites", 0f),
            new Ability("Thurisaz_Default", "Thurisaz_Activated", 0, 1, "Energy Blast", 1f),
            new Ability("ThurisazUruz_Default", "ThurisazUruz_Activated", 0, 1, "Radial Blast", 1f)
        };*/
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            skillType = transform.GetChild(i+1).gameObject;
            for (int j = 0; j < skillList[i].Count; j++)
            {
                skillImage = skillType.transform.GetChild(j).GetComponent<Image>();
                if (!skillList[i][j].unlocked)
                {
                    string path = "UI/" + skillList[i][j].displayName + "_Default";
                    skillImage.sprite = Resources.Load<Sprite>(path);
                    skillList[i][j].sprite = skillImage.sprite;
                }
                else
                {
                    string path = "UI/" + skillList[i][j].displayName + "_Activated";
                    skillImage.sprite = Resources.Load<Sprite>(path);
                    skillList[i][j].sprite = skillImage.sprite;
                }
            }
        }

        skillPointsText.text = "Skill Points: " + skillPoints;
    }

    public void UnlockSkill()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < skillList[i].Count; j++)
            {
                if (skillList[i][j].name == chosenSkill.name && skillPoints > 0 && !skillList[i][j].unlocked)
                {
                    if (!chosenSkill.isRune && (skillList[i][j-1].unlocked && skillList[i][j+1].unlocked) || (chosenSkill.isRune))
                    {
                        socketing = false;
                        chosenSkill.unlocked = true;
                        //Debug.Log("unlocked " + chosenSkill.name);
                        skillPoints--;
                        infoSection.transform.GetChild(2).gameObject.SetActive(false);
                        infoSection.transform.GetChild(3).gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    public void startSocketing()
    {
        socketing = true;
    }

    public void ChooseSkill()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < skillList[i].Count; j++)
            {
                if (skillList[i][j].name == EventSystem.current.currentSelectedGameObject.name)
                {
                    chosenSkill = skillList[i][j];
                    if(chosenSkill.unlocked)
                    {
                        infoSection.transform.GetChild(3).gameObject.SetActive(true);
                        infoSection.transform.GetChild(2).gameObject.SetActive(false);
                    }
                    else
                    {
                        infoSection.transform.GetChild(3).gameObject.SetActive(false);
                        infoSection.transform.GetChild(2).gameObject.SetActive(true);
                    }   
                    infoSection.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = chosenSkill.name;
                    infoSection.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = chosenSkill.infoBlurb;
                }
            }
        }

        //Debug.Log(playerMagicGO.GetComponent<PlayerMagic>().allAbilities.Count());

        for (int i = 0; i < playerMagicGO.GetComponent<PlayerMagic>().allAbilities.Count(); i++)
        {
            Debug.Log(chosenSkill.name);
            if (chosenSkill.name == playerMagicGO.GetComponent<PlayerMagic>().allAbilities[i].Name)
            {                
                chosenAbilityName = playerMagicGO.GetComponent<PlayerMagic>().allAbilities[i].Name;
            }

        }
        //Debug.Log("chosenSkill: " + chosenSkill.name);
        //Debug.Log("chosenAbility: " + chosenAbilityName);

        choseSkill = true;
    }

}