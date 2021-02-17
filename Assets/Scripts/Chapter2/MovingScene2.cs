using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class MovingScene2 : MonoBehaviour
{
    public TMP_InputField UserName;
    public int NextSceneNumber;
    public DialogueBase dialogueBase;
    
    public void move()
    {
        SceneManager.LoadScene(NextSceneNumber);
    }
    
    public int nowQuestNum;
    public void setUsername()
    {
        string name;
        if((UserName.text.ToString()).Length == 0 || UserName.text.ToString() == null) name = "User";
        else name = UserName.text.ToString();
        PrologueManager.UserName = name; 
        Debug.Log("유저내임 첨 입력: " + PrologueManager.UserName);
        DialogueManager2.UserName = name; 
        Debug.Log("유저내임 첨 입력: " + DialogueManager2.UserName);
    }

}
