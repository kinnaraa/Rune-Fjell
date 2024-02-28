using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class SocketSkill : MonoBehaviour
{
    public newSkillTree newSkillTree;
    public newSkillTree.Skill[] socketedSkills = new newSkillTree.Skill[7];
    public Image skillToSocket;

    void Start()
    {
        newSkillTree = newSkillTree.GetComponent<newSkillTree>();

        for (int i = 0; i < 7; i++)
        {
            transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/transparent");
        }
    }

    public void Socket()
    {
        Debug.Log(newSkillTree.socketing);
        if (newSkillTree.socketing)
        {
            int index = int.Parse(EventSystem.current.currentSelectedGameObject.transform.GetChild(0).name) - 1;
            skillToSocket = transform.GetChild(index).GetChild(0).GetComponent<Image>();
            Debug.Log("index: " + index);
            if (newSkillTree.chosenSkill.unlocked && !socketedSkills.Contains(newSkillTree.chosenSkill))
            {
                socketedSkills[index] = newSkillTree.chosenSkill;
                skillToSocket.sprite = newSkillTree.chosenSkill.sprite;
            }
        }
    }
}
