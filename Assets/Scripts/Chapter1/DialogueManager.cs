using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("fix this" + gameObject.name);
        }
        else
        {
            instance = this;
        }
    }

    public static string UserName = "User";
    public AudioClip doorSound;
    public AudioClip pencilSound;
    public AudioClip computerpenSound;
    public AudioClip classSound;
    public AudioClip paperSound;
    public AudioClip schoolRingSound;
    public AudioClip examRingSound;
    public AudioClip minuteSound;
    public AudioClip messengerSound;

    public GameObject DialogueBox;
    public TextMeshProUGUI dialogueName;
    public TextMeshProUGUI dialogueText;
    public Image dialoguePortrait;
    public Image backgroundPortrait;
    public float delay = 2f;
    public QuestStarter questStarter;
    public DialogueButton DialogBtn;

    public bool isCurrentlyTyping;
    private string completeText;
    public int thisId;
    private bool isDelayturn;
    public GameObject MedalAnimation;
    public GameObject endingAnimation;
    public GameObject MedalGround;

    public Queue<DialogueBase.Info> dialogueInfo;

    public bool Q1completed = false, Q2completed = false, Q3completed = false, Q4completed = false, Q5completed=false;
    private AudioSource audio; //사용할 오디오 소스 컴포넌트

    public void Start()
    {
        audio = GetComponent<AudioSource>();
    }


    public void EnqueueDialogue(DialogueBase db)
    {
        dialogueInfo = new Queue<DialogueBase.Info>();  //다이얼로그 초기화
        DialogueBox.SetActive(true); //화면에 띄움
        dialogueInfo.Clear();

        foreach (DialogueBase.Info info in db.dialogueInfo)
        {
            dialogueInfo.Enqueue(info);
        }
        isDelayturn = false;
        DequeueDialogue();
    }

    //세이브된 thisId데이터가 퀘스트부분일때
    public void QuestDialogue(DialogueBase db)
    {
        dialogueInfo = new Queue<DialogueBase.Info>();
        foreach (DialogueBase.Info info in db.dialogueInfo)
        {
            dialogueInfo.Enqueue(info);
        }
        for(int i=0; i<thisId; i++)
        {
            dialogueInfo.Dequeue(); //thisId보다 작은 수의 thisId 삭제
        }
        DequeueDialogue();
        DialogueBox.SetActive(false);
    }

    //세이브된 thisId데이터 로드
    public void LoadDialogue(DialogueBase db)
    {
        dialogueInfo = new Queue<DialogueBase.Info>();
        DialogueBox.SetActive(true); //화면에 띄움
        foreach (DialogueBase.Info info in db.dialogueInfo)
        {
            dialogueInfo.Enqueue(info);
        }
        for(int i=0; i<thisId; i++)
        {
            dialogueInfo.Dequeue(); //thisId보다 작은 수의 thisId 삭제
        }
        DequeueDialogue();
    }

    public void DequeueDialogue()
    {
        if (dialogueInfo.Count == 0) //챕터 1 종료
        {
            DialogueBox.SetActive(false);
            EndofDialogue();
            
        }else{ //다이얼로그 진행
            if (isDelayturn)
        {
            delayDialog(); return;
        }

        DialogueBox.SetActive(true);

        lock (dialogueInfo)
        {
            if ((thisId==12) && (!Q1completed)) //퀘스트 1 시작
            {
                DialogueBox.SetActive(false);
                DialogBtn.questnum = 1;
                questStarter.questnum = 1;
                questStarter.start();
            }
            else if ((thisId==36) && (!Q2completed)) //퀘스트 2 시작
            {
                DialogueBox.SetActive(false);
                DialogBtn.questnum = 2;
                questStarter.questnum = 2;
                questStarter.start();
            }
            else if ((thisId==61) && (!Q3completed))//퀘스트 3 시작
            {
                DialogueBox.SetActive(false);
                DialogBtn.questnum = 3;
                questStarter.questnum = 3;
                questStarter.start();
            }
            else if ((thisId==82) && (!Q4completed))//퀘스트 4 시작
            {
                DialogueBox.SetActive(false);
                DialogBtn.questnum = 4;
                questStarter.questnum = 4;
                questStarter.start();
            }
            else if ((thisId==108) && (!Q5completed))//퀘스트 5 시작
            {
                DialogueBox.SetActive(false);
                DialogBtn.questnum = 5;
                questStarter.questnum = 5;
                questStarter.start();
            }

            if (isCurrentlyTyping == true)
            {
                CompleteText();
                StopAllCoroutines();
                isCurrentlyTyping = false;
                return;
            }

            
            DialogueBase.Info info = dialogueInfo.Dequeue();
            completeText = info.myText;
            completeText = completeText.Replace("유저", UserName);
            thisId = info.id;

            //유저 이름
            if(info.myName.Equals("유저")) dialogueName.text = UserName;
            else dialogueName.text = info.myName;

            dialogueText.text = completeText;
            dialoguePortrait.sprite = info.portrait;
            backgroundPortrait.sprite = null;

            ////////오디오 설정
                if (thisId==7)
            {
                GetComponent<AudioSource>().clip = paperSound;
                GetComponent<AudioSource>().Play();
            }else if (thisId>7){GetComponent<AudioSource>().Stop();}

            if (thisId==14)
            {
                GetComponent<AudioSource>().clip = pencilSound;
                GetComponent<AudioSource>().Play();
            }
            else if (thisId>14)
            {
                GetComponent<AudioSource>().Stop();
            }
            if (thisId==15)
            {
                GetComponent<AudioSource>().clip = examRingSound;
                GetComponent<AudioSource>().Play();
            }
            else if (thisId>15)
            {
                GetComponent<AudioSource>().Stop();
            }
            if(thisId==22)
            {
               GetComponent<AudioSource>().clip = doorSound;
                GetComponent<AudioSource>().Play();
            }
            else if (thisId>22)
            {
                GetComponent<AudioSource>().Stop();
            }
            if (thisId==28)
            {
                GetComponent<AudioSource>().clip = messengerSound;
                GetComponent<AudioSource>().Play();
            }
             else if (thisId>28)
            {
                GetComponent<AudioSource>().Stop();
            }
            if (thisId==66)
            {
                GetComponent<AudioSource>().clip = minuteSound;
                GetComponent<AudioSource>().Play();
            }
             else if (thisId>66)
            {
                GetComponent<AudioSource>().Stop();
            }
            if (thisId==84)
            {
                GetComponent<AudioSource>().clip = minuteSound;
                GetComponent<AudioSource>().Play();
            }
             else if (thisId>84)
            {
                GetComponent<AudioSource>().Stop();
            }

            dialogueText.text = "";
            StartCoroutine(TypeText(completeText));
        }//end of lock

        switch (thisId)
        {
            case 3:
            case 13:
            case 18:
            case 21:
            case 28:
            case 42: //왜안되니
            case 56:
            case 65:
            case 72:
            case 78:
            case 84:
                isDelayturn = true; break;
            default: break;
        }
        }
    }

    IEnumerator TypeText(string completeText)
    {
        isCurrentlyTyping = true;
        foreach (char c in completeText.ToCharArray())
        {
            yield return new WaitForSeconds(delay);
            dialogueText.text += c;
        }
        isCurrentlyTyping = false;
    }

    private void CompleteText()
    {
        dialogueText.text = completeText;
    }

    private void EndofDialogue()
    {
        
        MedalGround.SetActive(true);
        MedalAnimation.SetActive(true);
        endingAnimation.SetActive(true);
        Invoke("GoEnd", 5f);
    }

    private void GoEnd()
    {
        MedalGround.SetActive(false); 
        MedalAnimation.SetActive(false);
        endingAnimation.SetActive(true);
        FadeOutScript.instance.Fade();
    }

    //대사 2초 자동 뜸들이기 함수
    private void delayDialog()
    {
        DialogueBox.SetActive(false);
        isDelayturn = false;
        Invoke("DequeueDialogue", 2f);
    }
    
}