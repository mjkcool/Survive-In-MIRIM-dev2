using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Popup2 : MonoBehaviour
{ 
    public static Popup2 instance2;
    private Animator animator;
    public Queue<PrologueBase.Info> prologueInfo;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (instance2 != null)
        {
            Debug.LogWarning("fix this" + gameObject.name);
        }
        else
        {
            instance2 = this;
        }
        
    }

    public void Start()
    {
        //audio = GetComponent<AudioSource>();
        GameLoad(); 
    }

    //로드/세이브
    public void GameSave(){
        PlayerPrefs.SetInt("LoadId2",DialogueManager2.instance2.thisId2);
        PlayerPrefs.Save(); 
        Debug.Log("저장된 아이디: " + DialogueManager2.instance2.thisId2);
    }

    public void GameLoad(){
        if (!PlayerPrefs.HasKey("LoadId2")){
            return;
        } 
        int thisId2 = PlayerPrefs.GetInt("LoadId2");
        DialogueManager2.instance2.thisId2 = thisId2;
        Debug.Log("현재 위치 아이디: " + DialogueManager2.instance2.thisId2);
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
