﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Popup : MonoBehaviour
{ 
    public static Popup instance;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (instance != null)
        {
            Debug.Log("팝업 존재");
        }
        else
        {
            instance = this;
            Debug.Log("팝업 인스턴스 생성");
        }
        
    }
    public void Start()
    {
        //audio = GetComponent<AudioSource>();   
        GameLoad(); 
    }

    //로드/세이브
    public void GameSave(){
        PlayerPrefs.SetInt("LoadId",DialogueManager.instance.thisId);
        PlayerPrefs.Save(); 
        Debug.Log("저장된 아이디: " + DialogueManager.instance.thisId);
    }

    public void GameLoad(){
        if (!PlayerPrefs.HasKey("LoadId")){
            return;
        }

        int thisId = PlayerPrefs.GetInt("LoadId");
        
        DialogueManager.instance.thisId = thisId;
        Debug.Log("현재 위치 아이디: " + DialogueManager.instance.thisId);
    }
   
   //데이터 초기화 = 새로시작
    public void newGame(){
        PlayerPrefs.DeleteKey("LoadId");
    }

   public void Close()
   {
       StartCoroutine(CloseAfterDelay());
   }

   private IEnumerator CloseAfterDelay()
   {
       animator.SetTrigger("close");
       yield return new WaitForSeconds(0.0f);
       gameObject.SetActive(false);
       animator.ResetTrigger("close");
   }
}
