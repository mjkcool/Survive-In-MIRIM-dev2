using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueButton2 : MonoBehaviour
{
    public int questnum;

    public void GetNextLine()
    {
        lock (DialogueManager2.instance)
        {
            DialogueManager2.instance.DequeueDialogue();
        }
        
    }
    public void GetNextLineQ()
    {
        switch (questnum)
        {
            case 1:
                Ch2_Quest1Manager.instance.DequeueQuest();
                break;
            case 2:
                Ch2_Quest2Manager.instance.DequeueQuest();
                break;
            case 3:
                Ch2_Quest3Manager.instance.DequeueQuest();
                break;
            case 4:
                Ch2_Quest4Manager.instance.DequeueQuest();
                break;
            case 5:
                Ch2_Quest5Manager.instance.DequeueQuest();
                break;
            default: break;
        }
        
    }

    public void TriggerChoosing(int choiceNumber)
    {
        switch (questnum)
        {
            case 1: Ch2_Quest1Manager.instance.chooseAnswer(choiceNumber); break;
            case 2: Ch2_Quest2Manager.instance.chooseAnswer(choiceNumber); break;
            case 3: Ch2_Quest3Manager.instance.chooseAnswer(choiceNumber); break;
            case 4: Ch2_Quest4Manager.instance.chooseAnswer(choiceNumber); break;
            case 5: Ch2_Quest5Manager.instance.chooseAnswer(choiceNumber); break;
            default: break;
        }
    }
}
