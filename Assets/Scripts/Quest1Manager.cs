using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Quest1Manager : MonoBehaviour
{
    //Dialog Objects
    public GameObject QuestDialogBox;
    public TextMeshProUGUI dialogueName;
    public TextMeshProUGUI dialogueText;
    public Image Portrait;
    public Image SuccessPortrait;
    public Sprite portraitImage;
    public Sprite successImage;
    //Q1-1
    public TMP_InputField InputF_1;
    public GameObject Input_1;
    //Q1-2
    public TMP_InputField InputF_2;
    public GameObject Input_2;

    private int dialogtotalcnt;
    public Queue<QuestBase.Info> QuestInfo;

    public static Quest1Manager instance;

    public void Awake()
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

    public void Start()
    {
        QuestInfo = new Queue<QuestBase.Info>();  //초기화

    }

    public void EnqueueQuest(QuestBase db)
    {
        SuccessPortrait.sprite = successImage;
        Portrait.sprite = portraitImage;
        SuccessPortrait.gameObject.SetActive(false);
        //이미지 사이즈 지정
        RectTransform rt = (RectTransform)Portrait.transform;
        rt.sizeDelta = new Vector2(1048, 1400);
        QuestDialogBox.SetActive(true);
        QuestInfo.Clear();

        foreach (QuestBase.Info info in db.QuestInfo)
        {
            QuestInfo.Enqueue(info);
        }
        dialogtotalcnt = QuestInfo.Count;

        InputF_1.characterLimit = 2;
        InputF_2.characterLimit = 52;

        DequeueQuest();
    }

    private bool flag = true; //기본값은 true
    private QuestBase.Info Qinfo_1, Qinfo_2;

    public void DequeueQuest()
    {
        if (QuestInfo.Count == dialogtotalcnt-3)
        {
            if (!flag) //문제 틀린 직후
            {
                Input_1.SetActive(true);
                dialogueName.text = Qinfo_1.myName;
                dialogueText.text = Qinfo_1.myText;
                flag = true;
            }
            else //문제 답 입력
            {
                if ((InputF_1.text.ToString()).Equals("4")) //정답 입력시
                {
                    QuestBase.Info info = QuestInfo.Dequeue();
                    dialogueName.text = info.myName;
                    dialogueText.text = info.myText;
                    Input_1.SetActive(false);
                }
                else if ((InputF_1.text.ToString()).Trim().Equals("") || (InputF_1.text.ToString()) == null)
                {
                    return; //미입력시 아무 반응 안함
                }
                else //오답 입력시
                {
                    Input_1.SetActive(false);
                    dialogueName.text = null;
                    dialogueText.text = "이곳은 아닌 것 같아";
                    flag = false;
                }
            }
            InputF_1.text = null;
        }
        else if (QuestInfo.Count == dialogtotalcnt - 5)
        {
            if (!flag) //문제 틀린 직후
            {
                Input_2.SetActive(true);
                dialogueName.text = Qinfo_2.myName;
                dialogueText.text = null;
                flag = true;
            }
            else //문제 답 입력
            {
                if ((InputF_2.text.ToString()).Trim().Equals("") || (InputF_2.text.ToString()) == null) //
                {
                    return; //미입력시 아무 반응 안함
                }
                else
                {
                    QuestManager.instance.startLoading(isCorrect(InputF_2.text.ToString()));

                    if (isCorrect(InputF_2.text.ToString()))
                    {
                        //정답의경우
                        Input_2.SetActive(false);
                        QuestBase.Info info = QuestInfo.Dequeue();
                        dialogueName.text = info.myName;
                        dialogueText.text = info.myText;
                        SuccessPortrait.gameObject.SetActive(true);
                        Portrait.gameObject.SetActive(false);
                        Input_2.SetActive(false);
                    }
                    else //오답 입력시
                    {
                        Input_2.SetActive(false);
                        dialogueName.text = null;
                        dialogueText.text = "잘못된 문장인가봐\n제대로 작동하지 않아";
                        flag = false;
                    }
                }
                
            }
        }
        else if (QuestInfo.Count == 0) //Quest 다이얼로그 끝나면
        {
            SuccessPortrait.gameObject.SetActive(false);
            Portrait.gameObject.SetActive(false);
            QuestManager.instance.spinStar();
            Invoke("EndofQuest", 4.5f);
            return;
        }
        else
        {
            QuestBase.Info info = QuestInfo.Dequeue();
            if (QuestInfo.Count == dialogtotalcnt - 3) //input 1 최초 로드
            {
                Input_1.SetActive(true);
                Qinfo_1 = info;
            }
            else if (QuestInfo.Count == dialogtotalcnt - 5) //input 2 최초 로드 
            {
                Input_2.SetActive(true);
                Qinfo_2 = info;
            }
            dialogueName.text = info.myName;
            dialogueText.text = info.myText;
        }

    }

    private string Correct_answer = "= new File ( \"final+2semester+2020/Korean.pdf\" ) ;";

    private bool isCorrect(string answer)
    {
        answer = answer.Trim();

        //1. 전체 문자열 비교
        if (!answer.Replace(" ", "").Equals(Correct_answer.Replace(" ", "")))
        {
            return false;
        }

        string[] raw_list = Correct_answer.Split('\x020');

        //2. 필수 단어의 여부
        if (answer.IndexOf(raw_list[1]+" ") == -1 || answer.IndexOf(raw_list[2]) == -1 || answer.IndexOf(raw_list[4]) == -1)
        {
            return false;
        }

        //3. 글자들의 위치 순서 비교
        int pos = -1, nowpos;
        for(int i=0; i<raw_list.Length; i++)
        {
            nowpos = answer.IndexOf(raw_list[i]);
            if (nowpos > -1 && nowpos > pos)
            {
                pos = nowpos;
            }
            else
            {
                return false;
            }
        }

        return true; //모든 검증 통과시 true반환
    }


    private void EndofQuest()
    {
        InputF_2.text = null;
        QuestDialogBox.SetActive(false);//화면에서 없앰
        DialogueManager.instance.Q1completed = true;
        (DialogueManager.instance.DialogueBox).SetActive(true);
        DialogueManager.instance.DequeueDialogue();
    }

}
