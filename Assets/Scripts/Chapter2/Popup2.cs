using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Popup2 : MonoBehaviour
{ 
    public static Popup2 instance;
    private Animator animator;
    public Queue<PrologueBase.Info> prologueInfo;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (instance != null)
        {
            Debug.LogWarning("fix this" + gameObject.name);
        }
        else
        {
            instance = this;
        }
        
    }

    public void Start()
    {
        //audio = GetComponent<AudioSource>();
        GameLoad(); 
    }

    //로드/세이브
    public void GameSave(){
        PlayerPrefs.SetInt("LoadId2",DialogueManager2.instance.thisId);
        PlayerPrefs.Save(); 
        Debug.Log("저장된 아이디: " + DialogueManager2.instance.thisId);
    }

    public void GameLoad(){
        if (!PlayerPrefs.HasKey("LoadId2")){
            return;
        } 
        int thisId = PlayerPrefs.GetInt("LoadId2");
        DialogueManager2.instance.thisId = thisId;
        Debug.Log("현재 위치 아이디: " + DialogueManager2.instance.thisId);
    }
   
   //데이터 초기화 = 새로시작
    public void newGame(){
        PlayerPrefs.DeleteKey("LoadId2");
    }

   public void Close()
   {
       StartCoroutine(CloseAfterDelay());
   }

   private IEnumerator CloseAfterDelay()
   {
       animator.SetTrigger("close");
       yield return new WaitForSeconds(0.5f);
       gameObject.SetActive(false);
       animator.ResetTrigger("close");
   }
}
