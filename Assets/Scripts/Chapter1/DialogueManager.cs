﻿using System.Collections;
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

    public Sprite[] bg = new Sprite[12];
    public Sprite[] portraits;

    public GameObject DialogueBox;
    public TextMeshProUGUI dialogueName;
    public TextMeshProUGUI dialogueText;
    public Image dialoguePortrait;
    public Image backgroundPortrait;
    public float delay = 2f;
    public QuestStarter questStarter;
    public DialogueButton DialogBtn;

    public bool isCurrentlyTyping;
    private string completeText, name;
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
        if (!isCurrentlyTyping) //전 대사 타이핑이 끝나면 실행
        {
            if (dialogueInfo.Count.Equals(0)) //챕터 1 종료
            {
                DialogueBox.SetActive(false);
                EndofDialogue();

            }
            else
            { //다이얼로그 진행
                if (isDelayturn)
                {
                    delayDialog(); return;
                }

                DialogueBox.SetActive(true);

                lock (dialogueInfo)
                {
                    if ((thisId.Equals(12)) && (!Q1completed)) //퀘스트 1 시작
                    {
                        DialogueBox.SetActive(false);
                        DialogBtn.questnum = 1;
                        questStarter.questnum = 1;
                        questStarter.start();
                    }
                    else if ((thisId.Equals(36)) && (!Q2completed)) //퀘스트 2 시작
                    {
                        DialogueBox.SetActive(false);
                        DialogBtn.questnum = 2;
                        questStarter.questnum = 2;
                        questStarter.start();
                    }
                    else if ((thisId.Equals(61)) && (!Q3completed))//퀘스트 3 시작
                    {
                        DialogueBox.SetActive(false);
                        DialogBtn.questnum = 3;
                        questStarter.questnum = 3;
                        questStarter.start();
                    }
                    else if ((thisId.Equals(82)) && (!Q4completed))//퀘스트 4 시작
                    {
                        DialogueBox.SetActive(false);
                        DialogBtn.questnum = 4;
                        questStarter.questnum = 4;
                        questStarter.start();
                    }
                    else if ((thisId.Equals(108)) && (!Q5completed))//퀘스트 5 시작
                    {
                        DialogueBox.SetActive(false);
                        DialogBtn.questnum = 5;
                        questStarter.questnum = 5;
                        questStarter.start();
                    }


                    dialogueText.text = "";

                    //다이얼로그 Dequeue
                    DialogueBase.Info info = dialogueInfo.Dequeue();
                    completeText = info.myText;
                    name = info.myName;
                    completeText = completeText.Replace("[User]", UserName);
                    name = name.Replace("[User]", UserName);
                    thisId = info.id;

                    dialogueName.text = name;

                    Sprite thisPortrait = null;
                    dialoguePortrait.gameObject.SetActive(true);
                    switch (thisId) //포트레잇
                    {
                        case 1: case 2: thisPortrait = portraits[0]; break; //F1_1
                        case 3: thisPortrait = portraits[1]; break; //F1_2
                        case 5: thisPortrait = portraits[18]; break; //T1_1
                        case 7: thisPortrait = portraits[15]; break; //mathPaper
                        case 8: case 9: case 16: thisPortrait = portraits[19]; break; //T1_2
                        case 13: thisPortrait = portraits[14]; break; //koreanPaper
                        case 20: case 25: case 39: case 41: case 87: thisPortrait = portraits[3]; break; //F2_1
                        case 23: case 28: thisPortrait = portraits[7]; break; //F3_1
                        case 26: thisPortrait = portraits[8]; break; //F3_2
                        case 31: thisPortrait = portraits[6]; break; //F2_4
                        case 33: thisPortrait = portraits[4]; break; //F2_2
                        case 35: thisPortrait = portraits[5]; break; //F2_3
                        case 45: case 46: case 48: case 50: case 51: case 97: thisPortrait = portraits[2]; break; //F1_3
                        case 47: case 59: case 60: thisPortrait = portraits[24]; break;
                        case 52: case 53: case 55: case 69: thisPortrait = portraits[25]; break;
                        case 54: thisPortrait = portraits[17]; break;
                        case 74: case 75: case 78: thisPortrait = portraits[23]; break;
                        case 76: case 77: thisPortrait = portraits[22]; break;
                        case 89: case 90: case 91: case 92: case 94: thisPortrait = portraits[20]; break;
                        case 99: thisPortrait = portraits[12]; break;
                        case 109: case 110: case 111: case 112: thisPortrait = portraits[9]; break;
                        case 113: case 114: thisPortrait = portraits[10]; break;
                        case 115: thisPortrait = portraits[11]; break;
                        default:
                            dialoguePortrait.gameObject.SetActive(false); break;
                    }
                    if (thisId >= 100 && thisId <= 108)
                    {
                        dialoguePortrait.gameObject.SetActive(true);
                        thisPortrait = portraits[21];
                    }

                    dialoguePortrait.sprite = thisPortrait;


                    Sprite thisBg = backgroundPortrait.sprite; //기존 이미지
                    switch (thisId) //배경사진
                    {
                        case 1: case 43: case 85:
                            thisBg = bg[0]; break;
                        case 4:
                        case 8:
                        case 15:
                            thisBg = bg[1]; break;
                        case 16: thisBg = bg[9]; break;
                        case 6:
                        case 10:
                        case 12:
                        case 27:
                        case 56:
                        case 61:
                        case 98:
                            thisBg = bg[5]; break;
                        case 11:
                        case 70:
                            thisBg = bg[10]; break;
                        case 19:
                        case 29:
                        case 80:
                        case 96:
                            thisBg = bg[7]; break;
                        case 22:
                        case 47:
                        case 87:
                            thisBg = bg[4]; break;
                        case 26:
                        case 28:
                        case 49:
                        case 58:
                        case 68:
                            thisBg = bg[2]; break;
                        case 52:
                        case 100:
                            thisBg = bg[3]; break;
                        case 66:
                        case 71:
                        case 83:
                            thisBg = bg[6]; break;
                        case 73: thisBg = bg[8]; break;
                        case 109: thisBg = bg[11]; break;
                        default: break;
                    }
                    backgroundPortrait.sprite = thisBg;


                    ////////오디오 설정
                    if (thisId.Equals(7))
                    {
                        GetComponent<AudioSource>().clip = paperSound;
                        GetComponent<AudioSource>().Play();
                    }
                    else if (thisId > 7) { GetComponent<AudioSource>().Stop(); }

                    if (thisId.Equals(14))
                    {
                        GetComponent<AudioSource>().clip = pencilSound;
                        GetComponent<AudioSource>().Play();
                    }
                    else if (thisId > 14)
                    {
                        GetComponent<AudioSource>().Stop();
                    }
                    if (thisId.Equals(15))
                    {
                        GetComponent<AudioSource>().clip = examRingSound;
                        GetComponent<AudioSource>().Play();
                    }
                    else if (thisId > 15)
                    {
                        GetComponent<AudioSource>().Stop();
                    }
                    if (thisId.Equals(22))
                    {
                        GetComponent<AudioSource>().clip = doorSound;
                        GetComponent<AudioSource>().Play();
                    }
                    else if (thisId > 22)
                    {
                        GetComponent<AudioSource>().Stop();
                    }
                    if (thisId.Equals(28))
                    {
                        GetComponent<AudioSource>().clip = messengerSound;
                        GetComponent<AudioSource>().Play();
                    }
                    else if (thisId > 28)
                    {
                        GetComponent<AudioSource>().Stop();
                    }
                    if (thisId.Equals(66))
                    {
                        GetComponent<AudioSource>().clip = minuteSound;
                        GetComponent<AudioSource>().Play();
                    }
                    else if (thisId > 66)
                    {
                        GetComponent<AudioSource>().Stop();
                    }
                    if (thisId.Equals(84))
                    {
                        GetComponent<AudioSource>().clip = minuteSound;
                        GetComponent<AudioSource>().Play();
                    }
                    else if (thisId > 84)
                    {
                        GetComponent<AudioSource>().Stop();
                    }

                    StartCoroutine(TypeText(completeText));

                    switch (thisId) //딜레이
                    {
                        case 3:
                        case 14:
                        case 18:
                        case 21:
                        case 28:
                        case 42:
                        case 56:
                        case 65:
                        case 72:
                        case 78:
                        case 84:
                            isDelayturn = true; break;
                        default: break;
                    }
                }//end of lock
            }
        }//end of if(isCurrentlyTyping)
        else //앞 대사 타이핑이 끝나기 전에 클릭할 경우
        {
            StopAllCoroutines();
            isCurrentlyTyping = false;
            dialogueText.text = completeText;
            return;
        }

    }

    IEnumerator TypeText(string completeText)
    {
        isCurrentlyTyping = true;
        string text = completeText;
        foreach (char c in text.ToCharArray())
        {
            yield return new WaitForSeconds(delay);
            dialogueText.text += c;
        }
        isCurrentlyTyping = false;
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