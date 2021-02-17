using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Dialogue Options", menuName = "DialogueOptions")]
public class DialogueOptions : PrologueBase
{
    [TextArea(2, 10)]
    public string choiceText; 

    [System.Serializable]
    public class Options
    {
        public string choiceBtn;
        public PrologueBase nextDialogue;
        public UnityEvent userEvent;
    }
    public Options[] optionsInfo;

}
