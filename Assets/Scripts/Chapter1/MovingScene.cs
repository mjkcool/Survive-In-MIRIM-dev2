using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class MovingScene : MonoBehaviour
{
    public TMP_InputField UserName;
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
        PlayerPrefs.Save(); 
    }

}
