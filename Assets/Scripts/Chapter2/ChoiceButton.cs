using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceButton : MonoBehaviour
{
    public QuestStarter2 questStarter;

    public void TriggerChoosing(int choiceNumber)
    {
        switch (questStarter.questnum)
        {
            case 1:
                Ch2_Quest1Manager.instance.chooseAnswer(choiceNumber);
                break;
            case 2:
                Ch2_Quest2Manager.instance.chooseAnswer(choiceNumber);
                break;
            case 3:
                Ch2_Quest3Manager.instance.chooseAnswer(choiceNumber);
                break;
            case 4:
                Ch2_Quest4Manager.instance.chooseAnswer(choiceNumber);
                break;
            case 5:
                Ch2_Quest5Manager.instance.chooseAnswer(choiceNumber);
                break;
            default: break;
        }
        Debug.Log((choiceNumber)+"번을 선택함");
    }
}
