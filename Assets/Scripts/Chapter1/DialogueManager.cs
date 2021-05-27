using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System.Linq;

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

    public GameObject DialogueBox;
    public TextMeshProUGUI dialogueName;
    public TextMeshProUGUI dialogueText;
    public Image dialoguePortrait;
    public Image backgroundPortrait;
    public Sprite emptySprite;

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
        if(PlayerPrefs.HasKey("Name"))
        {
            UserName = PlayerPrefs.GetString("Name");
        }
        else
        {
            UserName = "User";
        }
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

                    dialoguePortrait.gameObject.SetActive(true);

                    
                    if (info.portrait == null) dialoguePortrait.sprite = emptySprite;
                    else dialoguePortrait.sprite = info.portrait;
                    


                    Sprite thisBg = backgroundPortrait.sprite; //기존 이미지
                    if (thisId < 4) thisBg = bg[0];
                    else if (thisId < 6) thisBg = bg[1];
                    else if (thisId < 8) thisBg = bg[5];
                    else if (thisId < 10) thisBg = bg[1];
                    else if (thisId < 11) thisBg = bg[5];
                    else if (thisId < 12) thisBg = bg[10];
                    else if (thisId < 15) thisBg = bg[5];
                    else if (thisId < 16) thisBg = bg[1];
                    else if (thisId < 19) thisBg = bg[9];
                    else if (thisId < 22) thisBg = bg[7];
                    else if (thisId < 26) thisBg = bg[4];
                    else if (thisId < 27) thisBg = bg[2];
                    else if (thisId < 28) thisBg = bg[5];
                    else if (thisId < 29) thisBg = bg[2];
                    else if (thisId < 43) thisBg = bg[7];
                    else if (thisId < 47) thisBg = bg[0];
                    else if (thisId < 49) thisBg = bg[4];
                    else if (thisId < 52) thisBg = bg[2];
                    else if (thisId < 56) thisBg = bg[3];
                    else if (thisId < 58) thisBg = bg[5];
                    else if (thisId < 61) thisBg = bg[2];
                    else if (thisId < 66) thisBg = bg[5];
                    else if (thisId < 68) thisBg = bg[6];
                    else if (thisId < 70) thisBg = bg[2];
                    else if (thisId < 71) thisBg = bg[10];
                    else if (thisId < 73) thisBg = bg[6];
                    else if (thisId < 80) thisBg = bg[8];
                    else if (thisId < 83) thisBg = bg[7];
                    else if (thisId < 85) thisBg = bg[6];
                    else if (thisId < 87) thisBg = bg[0];
                    else if (thisId < 96) thisBg = bg[4];
                    else if (thisId < 98) thisBg = bg[7];
                    else if (thisId < 100) thisBg = bg[5];
                    else if (thisId < 109) thisBg = bg[3];
                    else thisBg = bg[11];

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