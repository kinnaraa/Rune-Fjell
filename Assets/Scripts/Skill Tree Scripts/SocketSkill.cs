using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class SocketSkill : MonoBehaviour
{
    public newSkillTree newSkillTree;
    public newSkillTree.Skill[] socketedSkills = new newSkillTree.Skill[7];
    public Image skillToSocket;
    public PlayerMagic playerMagic;
    bool gameStart = false;

    void Start()
    {
        newSkillTree = newSkillTree.GetComponent<newSkillTree>();

        for (int i = 0; i < 7; i++)
        {
            transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/transparent");
        }

        if (!gameStart)
        {
            transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = newSkillTree.nullSkill.sprite;
            transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = newSkillTree.nullSkill.sprite;
            transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = newSkillTree.skillList[0][2].sprite;
            transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = newSkillTree.nullSkill.sprite;
            transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = newSkillTree.skillList[1][3].sprite;
            transform.GetChild(5).GetChild(0).GetComponent<Image>().sprite = newSkillTree.nullSkill.sprite;
            transform.GetChild(6).GetChild(0).GetComponent<Image>().sprite = newSkillTree.nullSkill.sprite;

            socketedSkills[0] = newSkillTree.nullSkill; //storm
            socketedSkills[1] = newSkillTree.nullSkill;
            socketedSkills[2] = newSkillTree.skillList[0][2]; // energyblast
            socketedSkills[3] = newSkillTree.nullSkill;
            socketedSkills[4] = newSkillTree.skillList[1][3]; // shield
            socketedSkills[5] = newSkillTree.nullSkill;
            socketedSkills[6] = newSkillTree.nullSkill;
            gameStart = true;
        }
        
    }

    public void Socket()
    {
        if (newSkillTree.socketing)
        {
            int index = int.Parse(EventSystem.current.currentSelectedGameObject.transform.GetChild(0).name) - 1;
            skillToSocket = transform.GetChild(index).GetChild(0).GetComponent<Image>();
            
            if (newSkillTree.chosenSkill.unlocked && !socketedSkills.Contains(newSkillTree.chosenSkill))
            {
                socketedSkills[index] = newSkillTree.chosenSkill;
                skillToSocket.sprite = newSkillTree.chosenSkill.sprite;

                for(int i = 0; i < playerMagic.allAbilities.Count(); i++)
                {
                    if (newSkillTree.chosenSkill.name == playerMagic.allAbilities[i].Name)
                    {
                        playerMagic.abilities[index] = playerMagic.allAbilities[i];
                    }
                }
                
            }else if (newSkillTree.chosenSkill.unlocked && socketedSkills.Contains(newSkillTree.chosenSkill))
            {
                for (int i = 0; i < socketedSkills.Count(); i++)
                {
                    if(socketedSkills[i].name == newSkillTree.chosenSkill.name)
                    {
                        socketedSkills[i] = newSkillTree.nullSkill;
                        transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/transparent");
                    }
                }

                for(int i = 0; i < playerMagic.abilities.Count(); i++)
                {
                    if (playerMagic.abilities[i].Name == newSkillTree.chosenSkill.name)
                    {
                        playerMagic.abilities[i] = playerMagic.allAbilities[0];
                    }
                }

                for (int i = 0; i < playerMagic.allAbilities.Count(); i++)
                {
                    if (newSkillTree.chosenSkill.name == playerMagic.allAbilities[i].Name)
                    {
                        playerMagic.abilities[index] = playerMagic.allAbilities[i];
                    }
                }

                socketedSkills[index] = newSkillTree.chosenSkill;
                skillToSocket.sprite = newSkillTree.chosenSkill.sprite;

                Debug.Log("index: " + index + " skill:" + playerMagic.abilities[index]);
            }
            newSkillTree.socketing = false;
        }
    }
}
