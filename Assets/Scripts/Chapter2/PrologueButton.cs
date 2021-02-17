using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrologueButton : MonoBehaviour
{
    public void GetNextLine()
    {
        lock (PrologueManager.prologueInstance)
        {
            PrologueManager.prologueInstance.DequeueDialogue();
        }
        
    }
}
