using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Ch2_Quest5Manager : MonoBehaviour
{
    //Dialog Objects
    public GameObject Quest, DialogBox;
    public TextMeshProUGUI dialogueName;
    public TextMeshProUGUI dialogueText;
    public Image Portrait, Character;
    public GameObject ChoicesPack;
    public TextMeshProUGUI[] choices = new TextMeshProUGUI[5];
    public Sprite[] portraitImages = new Sprite[2];
    public Sprite characterPortrait;

    private int answerNumber, dialogtotalcnt;
    public Queue<QuestBase.Info> QuestInfo;

    public static Ch2_Quest5Manager instance;

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
        Portrait.sprite = portraitImages[0];
        Character.sprite = characterPortrait;
        //이미지 사이즈 지정
        RectTransform rt = (RectTransform)Portrait.transform;
        rt.sizeDelta = new Vector2(0, 1243);
        Quest.SetActive(true);
        Portrait.gameObject.SetActive(false); //초기엔 코드 이미지 NOT show
        QuestInfo.Clear();

        foreach (QuestBase.Info info in db.QuestInfo)
        {
            QuestInfo.Enqueue(info);
        }
        dialogtotalcnt = QuestInfo.Count;
        answerNumber = Random.Range(0, 5);

        DequeueQuest();
    }

    private bool flag = true; //기본값은 true

    public void DequeueQuest()
    {
        if (QuestInfo.Count == dialogtotalcnt - 1)
        {
            Portrait.gameObject.SetActive(true);
        }

        QuestBase.Info info = QuestInfo.Dequeue();
        dialogueName.text = info.myName;
        dialogueText.text = info.myText;
    }

    private string[] examples = new string[4]
        {"scores.item", "scores.item()", "scores.key()", "scores.keys()"};
    private string answer = "scores.items()";

    public void chooseAnswer(int number) //Trigger choice one
    {
        QuestManager.instance.startLoading(number == answerNumber);

        //컴파일 애니메이션
        if (number == answerNumber) //정답 맞춘 경우
        {
            QuestBase.Info info = QuestInfo.Dequeue();
            dialogueName.text = info.myName;
            dialogueText.text = info.myText;
            ChoicesPack.gameObject.SetActive(false);
        }
        else
        {
            ChoicesPack.gameObject.SetActive(false);
            Quest.SetActive(true);
            dialogueName.text = "디버거";
            dialogueText.text = "잘못된 정답인것같아!";
            flag = false;
        }
    }
}
