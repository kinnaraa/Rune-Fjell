using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillTree : MonoBehaviour
{
    private int skillPoints = 8;
    private TMP_Text skillPointsText;

    public List<List<Skill>> skillList;

    public List<Skill> fireSkills;
    public List<Skill> lightningSkills;
    public List<Skill> iceSkills;
    public List<Skill> earthSkills;

    private GameObject skillType;
    private Image skillImage;

    public class Skill
    {
        public string name;
        public Sprite sprite;
        public string element;
        public bool unlocked;

        public Skill(string skiillName, string skillElement)
        {
            name = skiillName;
            sprite = Resources.Load<Sprite>("Shitty Icons/" + name);
            element = skillElement;
            unlocked = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        fireSkills = new List<Skill>
        {
            new Skill("fireball", "fire"),
            new Skill("fireblast", "fire"),
            new Skill("firestorm", "fire"),
        };

        lightningSkills = new List<Skill>
        {
            new Skill("lightning strike", "lightning"),
            new Skill("lightning beam", "lightning"),
            new Skill("lightning smite", "lightning"),
        };

        iceSkills = new List<Skill>
        {
            new Skill("quick shards", "ice"),
            new Skill("hail", "ice"),
            new Skill("exploding ice", "ice"),
        };

        earthSkills = new List<Skill>
        {
            new Skill("earth wall", "earth"),
            new Skill("earthquake", "earth"),
            new Skill("boulder", "earth"),
        };

        skillList = new List<List<Skill>>
        {
            fireSkills, lightningSkills, iceSkills, earthSkills,
        };

        for (int i = 0; i < 4; i++)
        {
            skillType = transform.GetChild(i).gameObject;
            for (int j = 0; j < 3; j++)
            {
                skillImage = skillType.transform.GetChild(j).GetComponent<Image>();
                skillImage.sprite = skillList[i][j].sprite;
                skillImage.name = skillList[i][j].name;
                if (!skillList[i][j].unlocked)
                {
                    skillImage.color = Color.black;
                }
            }
        }

        skillPointsText = transform.GetChild(4).GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            skillType = transform.GetChild(i).gameObject;
            for (int j = 0; j < 3; j++)
            {
                skillImage = skillType.transform.GetChild(j).GetComponent<Image>();
                if (!skillList[i][j].unlocked)
                {
                    skillImage.color = Color.black;
                }
                else
                {
                    skillImage.color = Color.white;
                }
            }
        }

        skillPointsText.text = "Skill Points: " + skillPoints;
    }

    public void UnlockSkill()
    {
        Debug.Log("clicked");
        for (int i = 0;i < 4;i++)
        {
            for (int j = 0;j < 3; j++)
            {
                if (skillList[i][j].name == UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name && skillPoints > 0 && !skillList[i][j].unlocked)
                {
                    if (j == 0 || (skillList[i][0].unlocked && (j == 1 && !skillList[i][2].unlocked || j == 2 && !skillList[i][1].unlocked)))
                    {
                    Debug.Log("unlocked");
                    skillList[i][j].unlocked = true;
                    skillPoints--;
                    }
                }
            }
        }
    }
}
