using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
public class PrologueManager : MonoBehaviour
{

    public static PrologueManager prologueInstance;
    private void Awake()
    {
        if (prologueInstance != null)
        {
            Debug.LogWarning("fix this" + gameObject.name);
        }
        else
        {
            prologueInstance = this;
        }
    }

    public static string PrologueUserName = "User";
    //public AudioClip doorSound; //사용오디오

    public Sprite bg001; //배경이미지
    public GameObject DialogueStarter;
    public GameObject DialogueBox;
    public TextMeshProUGUI dialogueName;
    public TextMeshProUGUI dialogueText;
    public Image dialoguePortrait;
    public Image backgroundPortrait;
    public float delay = 2f;
    public PrologueButton DialogBtn;
    public bool isCurrentlyTyping;
    private string completeText, name;
    public int prologueId;
    private bool isDelayturn;

    public int department;

    private string[] F1 = {"시언희", "노재현", "유지영"};
    private string[] F2 = {"강수영", "차우림", "김라면"};
    private string[] F3 = {"김수형", "오자민", "곽노움"};
    private string[] F4 = {"김니은", "방은지", "문재수"};

    private bool isDialogueOption;
    public GameObject dialogueOptionsUI;
    private bool inDialogue;
    public GameObject[] ChoiceBtn;
    private int optionsAmount;
    public TMP_Text choiceText;
    public Queue<PrologueBase.Info> prologueInfo;
    public PrologueBase prologue;
    public DialogueBase dialogue;
    private AudioSource audio; 

     public void Start()
    {
        if (PlayerPrefs.HasKey("LoadId2")){
            dialogueOptionsUI.SetActive(false);
            DialogueBox.SetActive(false);
            GoEnd();
        } 
        audio = GetComponent<AudioSource>();
    }

    //과 선택 후 버튼 비활성화 및 인덱스 불러옴
    public void ChoiceClose()
    {
        dialogueOptionsUI.SetActive(false);
        Invoke("EnqueueDialogue", 5f);
    }

    public void EnqueueDialogue(PrologueBase db)
    {
        if(inDialogue) return;
        inDialogue = true;

        prologueInfo = new Queue<PrologueBase.Info>();  //다이얼로그 초기화
        DialogueBox.SetActive(true); //화면에 띄움
        prologueInfo.Clear();

        foreach (PrologueBase.Info info in db.prologueInfo)
        {
            prologueInfo.Enqueue(info);
        }
        isDelayturn = false;
        DequeueDialogue();

        if(db is DialogueOptions)
        {
            isDialogueOption = true;
            DialogueOptions dialogueOptions = db as DialogueOptions;
            optionsAmount = dialogueOptions.optionsInfo.Length;
            choiceText.text = dialogueOptions.choiceText;
            
            for(int i=0; i<ChoiceBtn.Length; i++)
            {
                ChoiceBtn[i].SetActive(false);
            }
            
            for(int i=0; i<optionsAmount; i++)
            {
                ChoiceBtn[i].SetActive(true);
                ChoiceBtn[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = dialogueOptions.optionsInfo[i].choiceBtn;
                UnityEventHandler userEventHandler = ChoiceBtn[i].GetComponent<UnityEventHandler>();
                userEventHandler.eventHandler = dialogueOptions.optionsInfo[i].userEvent;
                if(dialogueOptions.optionsInfo[i].nextDialogue != null)
                {
                    userEventHandler.Prologue = dialogueOptions.optionsInfo[i].nextDialogue;
                }   
                else
                {
                    userEventHandler.Prologue = null;
                }
            }
        }
    }

    public void LoadDialogue(PrologueBase db)
    {
        prologueInfo = new Queue<PrologueBase.Info>();
        DialogueBox.SetActive(true); //화면에 띄움
        foreach (PrologueBase.Info info in db.prologueInfo)
        {
            prologueInfo.Enqueue(info);
        }
        for(int i=0; i<prologueId; i++)
        {
            prologueInfo.Dequeue(); //prologueId보다 작은 수의 prologueId 삭제
        }
        DequeueDialogue();
    }

    public void DequeueDialogue()
    {
        if (prologueInfo.Count.Equals(0)) //프롤로그 종료
        {
            DialogueBox.SetActive(false);
            EndofDialogue();
            
        }else{ //다이얼로그 진행
            if (isDelayturn)
            {
                delayDialog(); return;
            }

            DialogueBox.SetActive(true);

            lock (prologueInfo)
            {
                if (isCurrentlyTyping.Equals(true))
                {
                    CompleteText();
                    StopAllCoroutines();
                    isCurrentlyTyping = false;
                    return;
                }

                PrologueBase.Info info = prologueInfo.Dequeue();
                prologueId = info.prologueId;
                completeText = info.prologueText;
                name = info.prologueName;
                completeText = completeText.Replace("[User]", PrologueUserName);
                name = name.Replace("[User]", PrologueUserName);
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
            
                dialogueText.text = completeText;
                dialogueName.text = name;
                dialoguePortrait.sprite = info.portrait;
                Sprite thisBg = bg001; //기존배경, 임시값 bg001
                if(prologueId>1) thisBg = backgroundPortrait.sprite; //기존 이미지
                switch (prologueId) //변경
                {
                    //배경 설정
                    // case 1: case 43: case 85: thisBg = bg001; break;
                    // case 4: case 8: case 15: thisBg = bg002; break;
                }
                backgroundPortrait.sprite = thisBg;

                ////////오디오 설정
                // if (prologueId.Equals(7))
                // {
                //     GetComponent<AudioSource>().clip = paperSound;
                //     GetComponent<AudioSource>().Play();
                // }else if (prologueId>7){GetComponent<AudioSource>().Stop();}

                dialogueText.text = "";
                StartCoroutine(TypeText(completeText));
            }//end of lock
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
        inDialogue = false;
        DialogueBox.SetActive(false);
        MiddleFadeOutScript.instance.Fade();
        MiddleFadeInScript.instance.Fade();
        Invoke("GoEnd", 4f);
    }

    private void GoEnd()
    {
        Debug.Log("프롤로그 매니저 끝");
        
        DialogueStarter.SetActive(true);
        if(isDialogueOption.Equals(true))
        {
            dialogueOptionsUI.SetActive(true);
        }
    }

    //대사 2초 자동 뜸들이기 함수
    private void delayDialog()
    {
        DialogueBox.SetActive(false);
        isDelayturn = false;
        Invoke("DequeueDialogue", 2f);
    }

}
