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
    private List<Skill> extraList;
    public Skill nullSkill;

    private GameObject skillType;
    private Image skillImage;
    private TMP_Text skillPointsText;
    public int skillPoints = 0;

    public bool choseSkill = false;
    public bool socketing = false;
    public int index = 0;
    public string skillName;

    public Skill chosenSkill;

    public string chosenAbilityName;
    public PlayerMagic playerMagic;
  
    public GameObject infoSection;

    public GameManager gm;
    public Monolith monolith;

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
            sprite = Resources.Load<Sprite>("UI/" + displayname + "_Default");
            isRune = rune;
            unlocked = false;
            infoBlurb = name + "\n\nBlurb about ability and what it does.\n\n...\n\n Damage: \n\nCooldown: \n\n";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        nullSkill = new Skill("Null", "Null", true);
        nullSkill.sprite = Resources.Load<Sprite>("UI/transparent");

        attacksList = new List<Skill>()
        {
            new Skill("FireBlast", "Sowilo", true),
            new Skill("RadialFireBurst", "Radial Fire Blast", false),
            new Skill("EnergyBlast", "Thurisaz", true),
            new Skill("LightningSmites", "Lightning Smites", false),
            new Skill("Storm", "Halagaz", true),
            new Skill("Hail", "Hail", false),
            new Skill("Ice", "Isa", true),
            new Skill("Wall", "Wall", false),
            new Skill("EarthSpike", "Ehwaz", true),
        };

        utilityList = new List<Skill>()
        {
            new Skill("Light", "Kenaz", true),
            new Skill("Damage", "Uruz", true),
            new Skill("DamageForceField", "Damage in Forcefield", false),
            new Skill("Shield", "Algiz", true),
            new Skill("HealingForceField", "Heal in Forcefield", false),
            new Skill("Heal", "Wunjo", true),
            new Skill("Odin Sight", "Ansuz", true),
        };

        passiveList = new List<Skill>()
        {
            new Skill("StamRegen", "Nauthiz", true),
            new Skill("Raidho", "Raidho", true),
            new Skill("Perthro", "Perthro", true),
        };

        extraList = new List<Skill>()
        {
            new Skill("Radial Blast", "Blasts Back", false),
            new Skill("Sun", "Blind in Radius", false),
        };

        skillList = new List<List<Skill>>
        {
            attacksList, utilityList, passiveList, extraList,
        };

        for (int i = 0; i < 4; i++)
        {
            skillType = transform.GetChild(i+1).gameObject;
            for (int j = 0; j < skillList[i].Count; j++)
            {
                skillImage = skillType.transform.GetChild(j).GetComponent<Image>();
                skillImage.sprite = skillList[i][j].sprite;
                skillImage.name = skillList[i][j].displayName;
                if(skillList[i][j].name == "EnergyBlast" || skillList[i][j].name == "Shield")
                {
                    skillList[i][j].unlocked = true;
                    skillList[i][i].sprite = Resources.Load<Sprite>("UI/" + skillList[i][j].displayName + "_Activated");
                }
                if (!skillList[i][j].unlocked)
                {
                    string path = "UI/" + skillList[i][j].displayName + "_Default";
                    skillImage.sprite = Resources.Load<Sprite>(path);
                }
            }
        }

        for (int i = 0; i < 2; i++)
        {
            skillImage = skillType.transform.GetChild(i).GetComponent<Image>();
            skillImage.sprite = extraList[i].sprite;
            skillImage.name = extraList[i].displayName;

            if (!extraList[i].unlocked)
            {
                string path = "UI/" + extraList[i].displayName + "_Default";
                skillImage.sprite = Resources.Load<Sprite>(path);
            }
            else
            {
                string path = "UI/" + extraList[i].displayName + "_Activated";
                skillImage.sprite = Resources.Load<Sprite>(path);
            }
        }

        skillPointsText = transform.GetChild(5).GetComponent<TMP_Text>();

        infoSection = transform.GetChild(7).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.finishedQuest)
        {
            skillList[1][0].unlocked = true;
        }

        if (monolith.raidoUnlocked)
        {
            skillList[2][1].unlocked = true;
        }

        if (monolith.sowiloUnlocked)
        {
            skillList[0][0].unlocked = true;
        }

        if (monolith.isaUnlocked)
        {
            skillList[0][6].unlocked = true;
        }

        if (monolith.halagazUnlocked)
        {
            skillList[0][4].unlocked = true;
        }

        if (monolith.ehwazUnlocked)
        {
            skillList[0][8].unlocked = true;
        }

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
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < skillList[i].Count; j++)
            {
                if (chosenSkill.displayName == "Sun" && skillPoints > 0 && !skillList[i][j].unlocked)
                {
                    if (skillList[0][0].unlocked && skillList[1][0].unlocked)
                    {
                        socketing = false;
                        chosenSkill.unlocked = true;
                        skillPoints--;
                        infoSection.transform.GetChild(2).gameObject.SetActive(false);
                        infoSection.transform.GetChild(3).gameObject.SetActive(true);
                        infoSection.transform.GetChild(4).GetComponent<Image>().sprite = chosenSkill.sprite;
                    }
                }
                else if (chosenSkill.displayName == "Radial Blast" && skillPoints > 0 && !skillList[i][j].unlocked)
                {
                    if (skillList[0][2].unlocked && skillList[1][1].unlocked)
                    {
                        socketing = false;
                        chosenSkill.unlocked = true;
                        skillPoints--;
                        infoSection.transform.GetChild(2).gameObject.SetActive(false);
                        infoSection.transform.GetChild(3).gameObject.SetActive(true);
                        infoSection.transform.GetChild(4).GetComponent<Image>().sprite = chosenSkill.sprite;
                    }
                }
                else
                {
                    if (skillList[i][j].displayName == chosenSkill.displayName && skillPoints > 0 && !skillList[i][j].unlocked)
                    {
                        if (!chosenSkill.isRune && (skillList[i][j - 1].unlocked && skillList[i][j + 1].unlocked) || (chosenSkill.isRune))
                        {
                            socketing = false;
                            chosenSkill.unlocked = true;

                            skillPoints--;
                            infoSection.transform.GetChild(2).gameObject.SetActive(false);
                            infoSection.transform.GetChild(3).gameObject.SetActive(true);
                            infoSection.transform.GetChild(4).GetComponent<Image>().sprite = chosenSkill.sprite;
                        }
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
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < skillList[i].Count; j++)
            {
                if (skillList[i][j].displayName == EventSystem.current.currentSelectedGameObject.name)
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
                    infoSection.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = chosenSkill.displayName;
                    infoSection.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = chosenSkill.infoBlurb;
                }
            }
        }

        for (int i = 0; i < playerMagic.allAbilities.Count(); i++)
        {
            if (chosenSkill.displayName == playerMagic.allAbilities[i].Name)
            {                
                chosenAbilityName = playerMagic.allAbilities[i].Name;
            }

        }

        choseSkill = true;
    }

}