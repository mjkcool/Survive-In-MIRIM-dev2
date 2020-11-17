using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueButton : MonoBehaviour
{
    public int questnum;

    public void GetNextLine()
    {
        DialogueManager.instance.DequeueDialogue();
    }
    public void GetNextLineQ()
    {
        switch (questnum)
        {
            case 1:
                Quest1Manager.instance.DequeueQuest();
                break;
            case 2:
                Quest2Manager.instance.DequeueQuest();
                break;
            case 3:
                Quest3Manager.instance.DequeueQuest();
                break;
            case 4:
                Quest4Manager.instance.DequeueQuest();
                break;
            case 5:
                Quest5Manager.instance.DequeueQuest();
                break;
            default: break;
        }
        
    }
}
