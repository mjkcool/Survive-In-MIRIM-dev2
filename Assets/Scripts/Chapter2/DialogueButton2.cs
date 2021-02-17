using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueButton2 : MonoBehaviour
{
    public int questnum;

    public void GetNextLine()
    {
        lock (DialogueManager2.instance2)
        {
            DialogueManager2.instance2.DequeueDialogue();
        }
        
    }
    public void GetNextLineQ()
    {
        switch (questnum)
        {
            case 1:
               Quest1Manager.instance.DequeueQuest();
               break;
            // case 2:
            //     Quest2Manager.instance2.DequeueQuest();
            //     break;
            // case 3:
            //     Quest3Manager.instance2.DequeueQuest();
            //     break;
            // case 4:
            //     Quest4Manager.instance2.DequeueQuest();
            //     break;
            // case 5:
            //     Quest5Manager.instance2.DequeueQuest();
            //     break;
            default: break;
        }
        
    }
}
