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
    public MonolithManager monolith;

    public WhereArtGnome WAG;
    public GameObject notUnlocked;

    public class Skill
    {
        public string name;
        public string displayName;
        public Sprite sprite;
        public bool isRune;
        public bool unlocked;
        public string infoBlurb;
        public string bindRuneName;

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
            new Skill("RadialFireBurst", "ThurisazSowilo", false),
            new Skill("EnergyBlast", "Thurisaz", true),
            new Skill("LightningSmites", "ThurisazHalagaz", false),
            new Skill("Storm", "Halagaz", true),
            new Skill("Hail", "IsaHalagaz", false),
            new Skill("Ice", "Isa", true),
            new Skill("Wall", "IsaEhwaz", false),
            new Skill("EarthSpike", "Ehwaz", true),
        };

        attacksList[1].bindRuneName = "Radial Fire Burst";
        attacksList[3].bindRuneName = "Lightning Smites";
        attacksList[5].bindRuneName = "Hail";
        attacksList[7].bindRuneName = "Wall";

        attacksList[0].infoBlurb = "The sun, Success, Goals, Realized, Honor.\n\nShoot fiery flames in front of you.\n\nDamage: 10\n\nCooldown: 1 second";
        attacksList[1].infoBlurb = "The combined powers of Sowilo and Thurisaz.\n\nBlast fire in a radius around yourself.\n\nDamage: 10\n\nCooldown: 1 second";
        attacksList[2].infoBlurb = "Thorn, Reactive, Force, Defense, Conflict.\n\nShoot out energy orbs that send enemies flying back.\n\nDamage: 10\n\nCooldown: 1 second";
        attacksList[3].infoBlurb = "The combined powers of Thurisaz and Halagaz.\n\nConjure lightning strikes at a distance.\n\nDamage: 10\n\nCooldown: 5 seconds";
        attacksList[4].infoBlurb = "Hail, Wrath of Nature, Uncontrolled Forces.\n\nSummon a tornado around yourself to push enemies back.\n\nDamage: 0\n\nCooldown: 5 seconds";
        attacksList[5].infoBlurb = "The combined powers of Halagaz and Isa.\n\nSend hail falling from the sky on your enemy.\n\nDamage: 10\n\nCooldown: 1 second";
        attacksList[6].infoBlurb = "Ice, Challenge, Frustration, Psychological Blocks.\n\nCast out icey projectiles.\n\nDamage: 10\n\nCooldown: seconds";
        attacksList[7].infoBlurb = "The combined powers of Isa and Ehwaz.\n\n\n\n\n\nAvailable in future updates...";
        attacksList[8].infoBlurb = "Yew Tree, Strength, Reliability, Trustworthiness.\n\nSend a large tree stump shooting out of the earth at your foes.\n\nDamage: 10\n\nCooldown: 5 seconds";

        utilityList = new List<Skill>()
        {
            new Skill("Light", "Kenaz", true),
            new Skill("Damage", "Uruz", true),
            new Skill("DamageForceField", "UruzAlgiz", false),
            new Skill("Shield", "Algiz", true),
            new Skill("HealingForceField", "WunjoAlgiz", false),
            new Skill("Heal", "Wunjo", true),
            new Skill("Odin Sight", "Ansuz", true),
        };

        utilityList[2].bindRuneName = "Damage Forcefield";
        utilityList[4].bindRuneName = "Healing Forcefield";

        utilityList[0].infoBlurb = "Torch, Vision, Revelation, Creativity, Technical Ability.\n\nSummon a light orb to help light your way.\n\nDamage: 0\n\nCooldown: 0 seconds";
        utilityList[1].infoBlurb = "A wild ox, Physical Strength, Speed, Untamed Potential.\n\nGain a multiplier to your damage for a short amount of time.\n\nDamage: x2\n\nCooldown: 10 second";
        utilityList[2].infoBlurb = "The combined powers of Uruz and Algiz.\n\nConjure a forcefield that gives you a damage multiplier while inside of it.\n\nDamage: 0\n\nCooldown: 5 seconds";
        utilityList[3].infoBlurb = "The Elk, Protection, Sheild, Ward Off Evil.\n\nConjure a shield in front of you to block enemy attacks.\n\nDamage: 0\n\nCooldown: 0 seconds";
        utilityList[4].infoBlurb = "The combined powers of Algiz and Wunjo.\n\nSummon a forcefield that heals you when inside of it.\n\nHealing: 10 per second\n\nCooldown: 5 seconds";
        utilityList[5].infoBlurb = "Joy, Comfort, Pleasure.\n\nHeal yourself to regain health.\n\nHealing: 10\n\nCooldown: 5 seconds";
        utilityList[6].infoBlurb = "Odin, Insight, Communication, Inspiration, True Vision.\n\n\n\n\n\nAvailable in future updates...";

        passiveList = new List<Skill>()
        {
            new Skill("StamRegen", "Nauthiz", true),
            new Skill("Raidho", "Raidho", true),
            new Skill("Perthro", "Perthro", true),
        };

        passiveList[0].infoBlurb = "Need, Self Reliance, Endurance, Survival.\n\n\n\n\n\nAvailable in future updates...";
        passiveList[1].infoBlurb = "Chariot, Travel, Journey, Evolution.\n\nDamage: 0\n\nOnce unlocked, you can click on the Rune icons for which you have already visited on the map to teleport to their locations.\n\nCooldown: 5 seconds";
        passiveList[2].infoBlurb = "Die Cup, Mysteries, Secrets, Occulat Abilities.\n\n\n\n\n\nAvailable in future updates...";

        extraList = new List<Skill>()
        {
            new Skill("Radial Blast", "ThurisazUruz", false),
            new Skill("Sun", "SowiloKenaz", false),
        };

        extraList[0].bindRuneName = "Radial Blast";
        extraList[1].bindRuneName = "Sun";

        extraList[0].infoBlurb = "The combined powers of Thurisaz and Uruz.\n\nSend a blast of energy orbs around you to push enemies away.\n\nDamage: 0\n\nCooldown: 1 second";
        extraList[1].infoBlurb = "The combined powers of Sowilo and Kenaz.\n\nConjure a figment of the sun above you to damage surrounding enemies.\n\nDamage: 10\n\nCooldown: 1 second";

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

        if (WAG.healingUnlocked)
        {
            skillList[1][5].unlocked = true;
        }

        if (WAG.damageUnlocked)
        {
            skillList[1][1].unlocked = true;
        }

        for (int i = 0; i < 4; i++)
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
                if (chosenSkill.displayName == "SowiloKenaz" && skillPoints > 0 && !skillList[i][j].unlocked)
                {
                    if (skillList[0][0].unlocked && skillList[1][0].unlocked)
                    {
                        socketing = false;
                        extraList[1].unlocked = true;
                        chosenSkill.unlocked = true;
                        skillPoints--;
                        infoSection.transform.GetChild(2).gameObject.SetActive(false);
                        infoSection.transform.GetChild(3).gameObject.SetActive(true);
                        infoSection.transform.GetChild(4).GetComponent<Image>().sprite = chosenSkill.sprite;
                    }
                }
                else if (chosenSkill.displayName == "ThurisazUruz" && skillPoints > 0 && !skillList[i][j].unlocked)
                {
                    if (skillList[0][2].unlocked && skillList[1][1].unlocked)
                    {
                        socketing = false;
                        extraList[0].unlocked = true;
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
                        notUnlocked.SetActive(false);
                    }
                    else if (chosenSkill.displayName == "Ansuz" || chosenSkill.displayName == "Nauthiz" || chosenSkill.displayName == "Perthro" || chosenSkill.displayName == "IsaEhwaz")
                    {
                        infoSection.transform.GetChild(3).gameObject.SetActive(false);
                        infoSection.transform.GetChild(2).gameObject.SetActive(false);
                    }else if (chosenSkill.displayName == "Sowilo" || chosenSkill.displayName == "Ehwaz" || chosenSkill.displayName == "Raidho" || chosenSkill.displayName == "Isa" || chosenSkill.displayName == "Halagaz" || chosenSkill.displayName == "Uruz" || chosenSkill.displayName == "Wunjo" || chosenSkill.displayName == "Kenaz")
                    {
                        infoSection.transform.GetChild(3).gameObject.SetActive(false);
                        infoSection.transform.GetChild(2).gameObject.SetActive(false);
                        notUnlocked.SetActive(true);
                    }
                    else
                    {
                        infoSection.transform.GetChild(3).gameObject.SetActive(false);
                        infoSection.transform.GetChild(2).gameObject.SetActive(true);
                        notUnlocked.SetActive(false);
                    }
                    
                    if(chosenSkill.isRune == false)
                    {
                        infoSection.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = chosenSkill.bindRuneName;
                    }
                    else
                    {
                        infoSection.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = chosenSkill.displayName;
                    }
                    
                    infoSection.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = chosenSkill.infoBlurb;
                    infoSection.transform.GetChild(4).GetComponent<Image>().sprite = chosenSkill.sprite;
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