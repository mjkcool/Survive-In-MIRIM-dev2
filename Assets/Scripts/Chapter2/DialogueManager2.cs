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
    public AudioClip crowdshoutSound; //사용오디오
    public AudioClip outcrowdSound;
    public AudioClip wheeSound;
    public AudioClip twothreeSound;
    public AudioClip jumpropeSound;
    public AudioClip duguduguSound;

    public Sprite bg001; //배경이미지

    public GameObject DialogueBox;
    public TextMeshProUGUI dialogueName;
    public TextMeshProUGUI dialogueText;
    public Image dialoguePortrait;
    public Image backgroundPortrait;
    public float delay = 2f;
    public QuestStarter2 questStarter;
    public DialogueButton2 DialogBtn;

    public int department;

    private string[] F1 = { "시언희", "노재현", "유지영" };
    private string[] F2 = { "강수영", "차우림", "김라면" };
    private string[] F3 = { "김수형", "오자민", "곽노움" };
    private string[] F4 = { "김니은", "방은지", "문재수" };
    private string[] T2 = { "백현정 선생님", "백현정 선생님", "새하얀 선생님" };
    private string[] T2_1 = { "참을성을 가지고 기다려. 곧 내려오실거야.", "참을성을 가지고 기다려. 곧 내려오실거야.", "흠, 나도 잘 모르겠네. 교장선생님이 잠깐 어디 가셨나봐." };
    private string[] T2_2 = { "밥을 왜 안 먹고와! 많이 배고프면 지금 얼른 매점 갖다와. 이따 힘써야되는데.", "밥을 왜 안 먹고와! 많이 배고프면 지금 얼른 매점 갖다와. 이따 힘써야되는데.", "아침밥 안 먹었어? 오늘같은 날은 꼭 먹어야지~" };
    private string[] text1 = { "와아아아!! 인~미과 인미과 최강 인미과~!", "와아아!!!! 웹솔!!", "허어어얼! 대애애애박!!" };
    private string[] text2 = { "그간의 노력이 빛을 발했구나.. 눈물난다 흑", "빛이! 나는! 솔루!! 빛이 난다!", "와아아!!!! 1위 먹는 디자인이 챔피언!!!" };
    private string[] text3 = { "흑흑 ㅠㅠ", "역시! 웹솔이 질리가 없지~ 우리의 위상은 계속된다!", "이야~ 역시 재능맨들! 운동까지 잘하면 어떡하냐~!" };
    private string[] text4 = { "와 감동실화ㅠㅠ 수고많았어 최강 인미과~!", "연습 죽어라 한 보상이 여기있네ㅋㅋ", "즐기며 뛴 보람이 있다!" };
    private string[] text5 = { "편의점이 웬말이냐, 미분 고!", "끝나고 미분으로 달려! 오늘은 먹고 죽자!!", "단체로 회식 가자~~~!!!" };

    public bool isCurrentlyTyping;
    private string completeText, name;
    public int thisId2;
    private bool isDelayturn;
    public GameObject MedalAnimation;
    public GameObject endingAnimation;
    public GameObject MedalGround;

    public Queue<DialogueBase.Info> dialogueInfo;
    public Queue<PrologueBase.Info> prologueInfo;

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

    //세이브된 thisId2데이터가 퀘스트부분일때
    public void QuestDialogue(DialogueBase db)
    {
        dialogueInfo = new Queue<DialogueBase.Info>();
        foreach (DialogueBase.Info info in db.dialogueInfo)
        {
            dialogueInfo.Enqueue(info);
        }
        for(int i=0; i<thisId2; i++)
        {
            dialogueInfo.Dequeue(); //thisId2보다 작은 수의 thisId2 삭제
        }
        DequeueDialogue();
        DialogueBox.SetActive(false);
    }

    //세이브된 thisId2데이터 로드
    public void LoadDialogue(DialogueBase db)
    {
        dialogueInfo = new Queue<DialogueBase.Info>();
        DialogueBox.SetActive(true); //화면에 띄움
        foreach (DialogueBase.Info info in db.dialogueInfo)
        {
            dialogueInfo.Enqueue(info);
        }
        for (int i = 0; i < thisId2; i++)
        {
            dialogueInfo.Dequeue(); //thisId2보다 작은 수의 thisId2 삭제
        }
        DequeueDialogue();
    }

    public void DequeueDialogue()
    {
        if (dialogueInfo.Count == 0) //챕터 2 종료
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
                if ((thisId2==13) && (!Q1completed)) //퀘스트 1 시작
                {
                    DialogueBox.SetActive(false);
                    DialogBtn.questnum = 1;
                    questStarter.questnum = 1;
                    questStarter.start();
                }
                else if ((thisId2==29) && (!Q2completed)) //퀘스트 2 시작
                {
                    DialogueBox.SetActive(false);
                    DialogBtn.questnum = 2;
                    questStarter.questnum = 2;
                    questStarter.start();
                }
                else if ((thisId2==46) && (!Q3completed)) //퀘스트 2 시작
                {
                    DialogueBox.SetActive(false);
                    DialogBtn.questnum = 3;
                    questStarter.questnum = 3;
                    questStarter.start();
                }
                else if ((thisId2==87) && (!Q4completed)) //퀘스트 2 시작
                {
                    DialogueBox.SetActive(false);
                    DialogBtn.questnum = 4;
                    questStarter.questnum = 4;
                    questStarter.start();
                }
                else if ((thisId2==103) && (!Q5completed)) //퀘스트 2 시작
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
                thisId2 = info.id;
                completeText = info.myText;
                name = info.myName;
                completeText = completeText.Replace("[User]", UserName);
                name = name.Replace("[User]", UserName);
                int department = PlayerPrefs.GetInt("Department");

                //과에 따른 이름값 변형
                completeText = completeText.Replace("[F1]", F1[department]);
                name = name.Replace("[F1]", F1[department]);
                completeText = completeText.Replace("[F2]", F2[department]);
                name = name.Replace("[F2]", F2[department]);
                completeText = completeText.Replace("[F3]", F3[department]);
                name = name.Replace("[F3]", F3[department]);
                completeText = completeText.Replace("[F4]", F4[department]);
                name = name.Replace("[F4]", F4[department]);
                name = name.Replace("[T2]", T2[department]);
                completeText = completeText.Replace("[T2_1]", T2_1[department]);
                completeText = completeText.Replace("[T2_2]", T2_2[department]);
                completeText = completeText.Replace("[text1]", text1[department]);
                completeText = completeText.Replace("[text2]", text2[department]);
                completeText = completeText.Replace("[text3]", text3[department]);
                completeText = completeText.Replace("[text4]", text4[department]);
                completeText = completeText.Replace("[text5]", text5[department]);


                dialogueText.text = completeText;
                dialogueName.text = name;
                //인물 portrait
                Sprite thisBg = bg001; //기존배경, 임시값 bg001
                if (thisId2 > 1) thisBg = backgroundPortrait.sprite; //기존 이미지
                switch (thisId2) //변경
                {
                }
                backgroundPortrait.sprite = thisBg;


                //오디오 설정
                if (thisId2 == 1)
                {
                    GetComponent<AudioSource>().clip = outcrowdSound;
                    GetComponent<AudioSource>().Play();
                }
                else if (thisId2 > 13) { GetComponent<AudioSource>().Stop(); }

                if (thisId2 == 56)
                {
                    GetComponent<AudioSource>().clip = wheeSound;
                    GetComponent<AudioSource>().Play();
                }
                else if (thisId2 > 56) { GetComponent<AudioSource>().Stop(); }

                if (thisId2 == 59)
                {
                    GetComponent<AudioSource>().clip = jumpropeSound;
                    GetComponent<AudioSource>().Play();
                }
                else if (thisId2 > 59) { GetComponent<AudioSource>().Stop(); }

                if (thisId2 == 62)
                {
                    GetComponent<AudioSource>().clip = duguduguSound;
                    GetComponent<AudioSource>().Play();
                }
                else if (thisId2 > 62) { GetComponent<AudioSource>().Stop(); }

                if (thisId2 == 70)
                {
                    GetComponent<AudioSource>().clip = crowdshoutSound;
                    GetComponent<AudioSource>().Play();
                }
                else if (thisId2 > 73) { GetComponent<AudioSource>().Stop(); }

                if (thisId2 == 74)
                {
                    GetComponent<AudioSource>().clip = wheeSound;
                    GetComponent<AudioSource>().Play();
                }
                else if (thisId2 > 74) { GetComponent<AudioSource>().Stop(); }

                if (thisId2 == 75)
                {
                    GetComponent<AudioSource>().clip = crowdshoutSound;
                    GetComponent<AudioSource>().Play();
                }
                else if (thisId2 > 77) { GetComponent<AudioSource>().Stop(); }

                if (thisId2 == 80)
                {
                    GetComponent<AudioSource>().clip = twothreeSound;
                    GetComponent<AudioSource>().Play();
                }
                else if (thisId2 > 80) { GetComponent<AudioSource>().Stop(); }

                if (thisId2 == 102)
                {
                    GetComponent<AudioSource>().clip = duguduguSound;
                    GetComponent<AudioSource>().Play();
                }
                else if (thisId2 > 102) { GetComponent<AudioSource>().Stop(); }

                if (thisId2 == 104)
                {
                    GetComponent<AudioSource>().clip = duguduguSound;
                    GetComponent<AudioSource>().Play();
                }
                else if (thisId2 > 104) { GetComponent<AudioSource>().Stop(); }

                if (thisId2 == 106)
                {
                    GetComponent<AudioSource>().clip = crowdshoutSound;
                    GetComponent<AudioSource>().Play();
                }
                else if (thisId2 > 111) { GetComponent<AudioSource>().Stop(); }

                dialogueText.text = "";
                StartCoroutine(TypeText(completeText));
            }//end of lock

            switch (thisId2)
            {
                case 10:
                case 16:
                case 25:
                case 38:
                case 39:
                case 50:
                case 60:
                case 67:
                case 78:
                case 92:
                case 102:
                case 104:
                case 111:
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