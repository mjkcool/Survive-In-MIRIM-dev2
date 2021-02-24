using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DialogueManager2 : MonoBehaviour
{
    public static DialogueManager2 instance2;
    private void Awake()
    {
        if (instance2 != null)
        {
            Debug.LogWarning("fix this" + gameObject.name);
        }
        else
        {
            instance2 = this;
        }
        
    }

    public static string UserName = "User";
    //public AudioClip doorSound; //사용오디오

    public Sprite bg001; //배경이미지

    public GameObject DialogueBox;
    public TextMeshProUGUI dialogueName;
    public TextMeshProUGUI dialogueText;
    public Image dialoguePortrait;
    public Image backgroundPortrait;
    public float delay = 2f;
    public QuestStarter2 questStarter;
    public DialogueButton2 DialogBtn;

    public bool isCurrentlyTyping;
    private string completeText;
    public int thisId2;
    private bool isDelayturn;
    public GameObject MedalAnimation;
    public GameObject endingAnimation;
    public GameObject MedalGround;

    public Queue<DialogueBase2.Info> dialogueInfo2;
    public Queue<PrologueBase.Info> prologueInfo;

    public bool Q1completed = false;
    private AudioSource audio; //사용할 오디오 소스 컴포넌트

    public void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void EnqueueDialogue(DialogueBase2 db)
    {
        dialogueInfo2 = new Queue<DialogueBase2.Info>();  //다이얼로그 초기화
        DialogueBox.SetActive(true); //화면에 띄움
        dialogueInfo2.Clear();

        foreach (DialogueBase2.Info info in db.dialogueInfo2)
        {
            dialogueInfo2.Enqueue(info);
        }
        isDelayturn = false;
        DequeueDialogue();
    }

    //세이브된 thisId2데이터가 퀘스트부분일때
    // public void QuestDialogue(DialogueBase2 db)
    // {
    //     dialogueInfo2 = new Queue<DialogueBase2.Info>();
    //     foreach (DialogueBase2.Info info in db.dialogueInfo2)
    //     {
    //         dialogueInfo2.Enqueue(info);
    //     }
    //     for(int i=0; i<thisId2; i++)
    //     {
    //         dialogueInfo2.Dequeue(); //thisId2보다 작은 수의 thisId2 삭제
    //     }
    //     DequeueDialogue();
    //     DialogueBox.SetActive(false);
    // }

    //세이브된 thisId2데이터 로드
    public void LoadDialogue(DialogueBase2 db)
    {
        dialogueInfo2 = new Queue<DialogueBase2.Info>();
        DialogueBox.SetActive(true); //화면에 띄움
        foreach (DialogueBase2.Info info in db.dialogueInfo2)
        {
            dialogueInfo2.Enqueue(info);
        }
        for(int i=0; i<thisId2; i++)
        {
            dialogueInfo2.Dequeue(); //thisId2보다 작은 수의 thisId2 삭제
        }
        DequeueDialogue();
    }

    public void DequeueDialogue()
    {
        if (dialogueInfo2.Count == 0) //챕터 2 종료
        {
            DialogueBox.SetActive(false);
            EndofDialogue();
            
        }else{ //다이얼로그 진행
            if (isDelayturn)
            {
                delayDialog(); return;
            }

            DialogueBox.SetActive(true);

            lock (dialogueInfo2)
            {
                // if ((thisId2==5) && (!Q1completed)) //퀘스트 1 시작
                // {
                //     DialogueBox.SetActive(false);
                //     DialogBtn.questnum = 1;
                //     questStarter.questnum = 1;
                //     questStarter.start();
                // }

                if (isCurrentlyTyping == true)
                {
                    CompleteText();
                    StopAllCoroutines();
                    isCurrentlyTyping = false;
                    return;
                }

            
                DialogueBase2.Info info = dialogueInfo2.Dequeue();
                completeText = info.myText2;
                completeText = completeText.Replace("[User]", UserName);
                thisId2 = info.id2;

                //유저 이름
                if(info.myName2.Equals("[User]")) dialogueName.text = UserName;
                else dialogueName.text = info.myName2;

                dialogueText.text = completeText;
                dialoguePortrait.sprite = info.portrait2;
                Sprite thisBg = bg001; //기존배경, 임시값 bg001
                if(thisId2>1) thisBg = backgroundPortrait.sprite; //기존 이미지
                switch (thisId2) //변경
                {
                    // case 1: case 43: case 85: thisBg = bg001; break;
                    // case 4: case 8: case 15: thisBg = bg002; break;
                    // case 6: case 10: case 12: case 27: case 56: case 61: case 98: backgroundPortrait.sprite = bg006; break;
                    // case 11: case 70: thisBg = bg011; break;
                    // case 19: case 29: case 80: case 96: thisBg = bg008; break;
                    // case 22: case 47: case 87: thisBg = bg005; break;
                    // case 26: case 28: case 49: case 58: case 68: thisBg = bg003; break;
                    // case 52: case 100: thisBg = bg004; break;
                    // case 66: case 71: case 83: thisBg = bg007; break;
                    // case 73: thisBg = bg009; break;
                    // case 109: thisBg = bg012; break;
                }
                backgroundPortrait.sprite = thisBg;


                ////////오디오 설정
                // if (thisId2==7)
                // {
                //     GetComponent<AudioSource>().clip = paperSound;
                //     GetComponent<AudioSource>().Play();
                // }else if (thisId2>7){GetComponent<AudioSource>().Stop();}

                dialogueText.text = "";
                StartCoroutine(TypeText(completeText));
            }//end of lock

            // switch (thisId2)
            // {
            //     case 3:
            //     case 13:
            //     case 18:
            //     case 21:
            //     case 28:
            //     case 42:
            //     case 56:
            //     case 65:
            //     case 72:
            //     case 78:
            //     case 84:
            //         isDelayturn = true; break;
            //     default: break;
            // }
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