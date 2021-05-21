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
    public TMP_InputField PrologueUserName;
    public int NextSceneNumber;
    public DialogueBase dialogueBase;
    
    public void move()
    {
        LoadingSceneManager.LoadScene(NextSceneNumber);
    }
    
    public int nowQuestNum;
    public void setUsername()
    {
        PlayerPrefs.SetString("Name", UserName.text);
        PlayerPrefs.SetString("PrologueName", PrologueUserName.text);
        PlayerPrefs.Save(); 
        if(PlayerPrefs.HasKey("Name"))
        {
            PrologueUserName.text = PlayerPrefs.GetString("Name");
            UserName.text = PlayerPrefs.GetString("Name");
            PrologueManager.PrologueUserName = PrologueUserName.text; 
            DialogueManager2.UserName = UserName.text; 
            Debug.Log("프롤로그내임 첨 입력: " +  PrologueUserName.text);
            Debug.Log("유저내임 첨 입력: " +  UserName.text);
        }
        else
        {
            PrologueManager.PrologueUserName = "User";
            DialogueManager2.UserName = "User";
        }
    }

}
