using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Ch2_Quest1Manager : MonoBehaviour
{
    //Dialog Objects
    public GameObject Quest, DialogBox;
    public TextMeshProUGUI dialogueName;
    public TextMeshProUGUI dialogueText;
    public Image Portrait, Character;
    public GameObject ChoicesPack;
    public TextMeshProUGUI[] choices = new TextMeshProUGUI[5];
    public Sprite portraitImage;
    public Sprite characterPortrait;
    public Sprite[] bgPortrait;
    public Image background;

    private int answerNumber, dialogtotalcnt;
    public Queue<QuestBase.Info> QuestInfo;

    public static Ch2_Quest1Manager instance;

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
        QuestInfo = new Queue<QuestBase.Info>();  
    }

    public void EnqueueQuest(QuestBase db)
    {
        Portrait.sprite = portraitImage;
        Character.sprite = characterPortrait;
        //이미지 사이즈 지정
        RectTransform rt = (RectTransform)Portrait.transform;
        rt.sizeDelta = new Vector2(0, 818);
        Quest.SetActive(true);
        Portrait.gameObject.SetActive(false); //초기엔 코드 이미지 NOT show
        QuestInfo.Clear();

        foreach (QuestBase.Info info in db.QuestInfo)
        {
            QuestInfo.Enqueue(info);
        }
        dialogtotalcnt = QuestInfo.Count;
        answerNumber = Random.Range(0, 4); //정답-매번 순서 섞임 / 정답 번호 부여
        Debug.Log("정답은 "+answerNumber);

        background.sprite = bgPortrait[0];

        setChoiceText(); //선택지에 글씨 배치
        DequeueQuest();
    }

    private bool flag = true; //기본값 true

    public void DequeueQuest()
    {
        if (QuestInfo.Count.Equals(dialogtotalcnt - 1))
        {
            Portrait.gameObject.SetActive(true); //문제 최초 등장
        }
        else if (QuestInfo.Count.Equals(dialogtotalcnt - 2))
        {
            Character.gameObject.SetActive(true); //디버거 캐릭터 등장
        }
        else if (QuestInfo.Count.Equals(3)) //선택지 등장
        {
            Portrait.gameObject.SetActive(true);
            Character.gameObject.SetActive(false);
            DialogBox.SetActive(false);
            ChoicesPack.SetActive(true); //선택지묶음
            return;
        }
        else if (QuestInfo.Count.Equals(1))
        {
            Portrait.gameObject.SetActive(false);
            Character.gameObject.SetActive(false);
            background.sprite = bgPortrait[1];
        }
        else if (QuestInfo.Count.Equals(0)) //Quest 다이얼로그 끝나면
        {
            Character.gameObject.SetActive(false);
            QuestManager.instance.spinStar();
            Invoke("EndofQuest", 4.5f);
            return;
        }

        QuestBase.Info info = QuestInfo.Dequeue();
        string username = (string)DialogueManager2.UserName;
        Debug.Log(username);
        string name = info.myName;
        name = name.Replace("[User]", username);
        dialogueName.text = name;
        string txt = info.myText;
        txt = txt.Replace("[User]", username);
        dialogueText.text = txt;

    }

    private string[] examples = new string[4]
        {"opening = Part.__init__(this, t1, \"개막사\")",
            "opening = Part(self, t1, \"개막사\")",
            "opening = Part.__init__(t1, \"개막사\")",
            "opening = Part(this, t1, \"개막사\")"};
    private string answer = "opening = Part(t1, \"개막사\")";

    private void setChoiceText()
    {
        int j = 0;
        for (int i = 0; i < 5; i++)
        {
            if (i.Equals(answerNumber)) choices[i].text = answer;
            else choices[i].text = examples[j++]; //j<4
        }
    }

    public void chooseAnswer(int choiceNumber) //Trigger choice one
    {
        QuestManager.instance.startLoading(choiceNumber.Equals(answerNumber));

        //컴파일 애니메이션
        if (choiceNumber.Equals(answerNumber)) //정답 맞춘 경우
        {
            Portrait.gameObject.SetActive(false);
            Character.gameObject.SetActive(true);
            QuestBase.Info info = QuestInfo.Dequeue();
            DialogBox.SetActive(true);
            dialogueName.text = info.myName;
            dialogueText.text = info.myText;
            ChoicesPack.gameObject.SetActive(false);
        }
        else
        {
            Portrait.gameObject.SetActive(false);
            ChoicesPack.gameObject.SetActive(false);
            DialogBox.SetActive(true);
            dialogueName.text = "디버거";
            dialogueText.text = "잘못된 정답인것같아!";
            flag = false;
        }
    }

    private void EndofQuest()
    {
        Quest.SetActive(false);
        DialogueManager2.instance.Qcompleted[0] = true;
        (DialogueManager2.instance.DialogueBox).SetActive(true);
        DialogueManager2.instance.DequeueDialogue();
    }
}