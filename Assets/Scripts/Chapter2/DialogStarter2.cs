using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DialogStarter2 : MonoBehaviour
{
    public DialogueBase2 dialogue;
    public Queue<PrologueBase.Info> prologueInfo;
    public GameObject chapterIndex;
    private Animator animator;
    private AudioSource audio;
    

    private void Start()
    {
        Debug.Log(Screen.width);
        //아이디 값이 0, 즉 세이브가 없는 경우
        if(DialogueManager2.instance2.thisId2 == 0)
        {
            chapterIndex.SetActive(true);
            Invoke("TriggerDialogue", 5f);
        }
        //퀘스트 중에 세이브했을 경우
        else if(DialogueManager2.instance2.thisId2 == 3)
        {
            GetComponent<AudioSource>().Stop();
            DialogueManager2.instance2.QuestDialogue(dialogue);
        }
        //일반적인 다이얼로그에서 세이브했을경우
        else 
        {
            GetComponent<AudioSource>().Stop();
            DialogueManager2.instance2.LoadDialogue(dialogue);
        }
    }

    public void TriggerDialogue()
    {
        chapterIndex.SetActive(false);
        DialogueManager2.instance2.EnqueueDialogue(dialogue);
    }
    
}
