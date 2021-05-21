using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DialogStarter2 : MonoBehaviour
{
    public DialogueBase dialogue;
    public Queue<PrologueBase.Info> prologueInfo;
    public GameObject chapterIndex;
    private Animator animator;
    private AudioSource audio;
    

    private void Start()
    {
        Debug.Log(Screen.width);
        //아이디 값이 0, 즉 세이브가 없는 경우
        if(DialogueManager2.instance.thisId.Equals(0))
        {
            chapterIndex.SetActive(true);
            Invoke("TriggerDialogue", 5f);
        }

        else 
        {
            GetComponent<AudioSource>().Stop();
            DialogueManager2.instance.LoadDialogue(dialogue);
        }
    }

    public void TriggerDialogue()
    {
        chapterIndex.SetActive(false);
        DialogueManager2.instance.EnqueueDialogue(dialogue);
    }
    
}
