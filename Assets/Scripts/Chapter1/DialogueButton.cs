using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueButton : MonoBehaviour
{
    public int questnum;

    public void GetNextLine()
    {
        lock (DialogueManager.instance)
        {
            DialogueManager.instance.DequeueDialogue();
        }
        
    }
    public void GetNextLineQ()
    {
        switch (questnum)
        {
            case 1:
                Ch1_Quest1Manager.instance.DequeueQuest();
                break;
            case 2:
                Ch1_Quest2Manager.instance.DequeueQuest();
                break;
            case 3:
                Ch1_Quest3Manager.instance.DequeueQuest();
                break;
            case 4:
                Ch1_Quest4Manager.instance.DequeueQuest();
                break;
            case 5:
                Ch1_Quest5Manager.instance.DequeueQuest();
                break;
            default: break;
        }
        
    }
}
