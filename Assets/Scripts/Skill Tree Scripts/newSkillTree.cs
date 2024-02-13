using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

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
    public GameObject socketingShadow;
    public Skill[] socketedSkills = new Skill[7];
    public Image skillToSocket;
    public bool socketing = false;
    public int index = 0;
    public string skillName;

    public Skill chosenSkill;
    public GameObject infoSection;

    public class Skill
    {
        public string name;
        public Sprite sprite;
        public bool isRune;
        public bool unlocked;

        public Skill(string skiillName, bool rune)
        {
            name = skiillName;
            sprite = Resources.Load<Sprite>("UI/Algiz_Default");
            isRune = rune;
            unlocked = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        attacksList = new List<Skill>()
        {
            new Skill("Sowilo", true),
            new Skill("Flamethrower", false),
            new Skill("Thurisaz", true),
            new Skill("Lightning Smite", false),
            new Skill("Hagalaz", true),
            new Skill("Hail", false),
            new Skill("Isa", true),
            new Skill("Ice Rock Wall", false),
            new Skill("Ehwaz", true),
        };

        utilityList = new List<Skill>()
        {
            new Skill("Ansuz", true),
            new Skill("Wunjo", true),
            new Skill("Heal in Forcefield", false),
            new Skill("Algiz", true),
            new Skill("Damage in Forcefield", false),
            new Skill("Uruz", true),
            new Skill("Kenaz", true),
        };

        passiveList = new List<Skill>()
        {
            new Skill("Nauthiz", true),
            new Skill("Raidho", true),
            new Skill("Perthro", true),
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
                if (!skillList[i][j].unlocked)
                {
                    skillImage.sprite = Resources.Load<Sprite>("UI/Algiz_Activated");
                }
            }
        }
        skillPointsText = transform.GetChild(4).GetComponent<TMP_Text>();

        for(int i = 0; i < 7; i++)
        {
            socketedSkills[i] = null;
        }

        for(int i = 0; i < 7; i++)
        {
            transform.GetChild(5).GetChild(i).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/transparent");
        }

        infoSection = transform.GetChild(6).gameObject;

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
                    skillImage.sprite = Resources.Load<Sprite>("UI/Algiz_Default");
                    skillList[i][j].sprite = skillImage.sprite;
                }
                else
                {
                    skillImage.sprite = Resources.Load<Sprite>("UI/Algiz_Activated");
                    skillList[i][j].sprite = skillImage.sprite;
                }
            }
        }

        skillPointsText.text = "Skill Points: " + skillPoints;

        if(chosenSkill != null)
            if (chosenSkill.unlocked)
            {
                infoSection.transform.GetChild(2).gameObject.SetActive(false);
                infoSection.transform.GetChild(3).gameObject.SetActive(true);
            }
            else if (!chosenSkill.unlocked)
            {
                infoSection.transform.GetChild(2).gameObject.SetActive(true);
                infoSection.transform.GetChild(3).gameObject.SetActive(false);
            }

            if(choseSkill)
                infoSection.transform.GetChild(4).GetComponent<Image>().sprite = chosenSkill.sprite;
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
                        chosenSkill.unlocked = true;
                        Debug.Log("unlocked " + chosenSkill.name);
                        skillPoints--;
                    }
                }
            }
        }
    }

    public void Socket()
    {

        skillToSocket = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Image>();
        Debug.Log("skillToSocket: " + skillToSocket);
        index = int.Parse(skillToSocket.name) - 1;
        Debug.Log("index: " + index);
        socketing = true;
    }

    public void ChooseSkill()
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < skillList[i].Count; j++)
            {
                if (skillList[i][j].name == EventSystem.current.currentSelectedGameObject.name)
                {
                    Debug.Log("chose " + skillList[i][j].name);

                    chosenSkill = skillList[i][j];
                    Debug.Log("chosenSkill: " + chosenSkill.name);
                    infoSection.SetActive(true);
                    infoSection.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = chosenSkill.name;

                    //socketedSkills[index] = skillList[i][j];
                    //transform.GetChild(5).GetChild(index).GetChild(0).GetComponent<Image>().sprite = skillList[i][j].sprite;
                }
            }
        }
        choseSkill = true;
    }
}