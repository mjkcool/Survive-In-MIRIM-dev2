using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues")]
public class DialogueBase : ScriptableObject
{
    [System.Serializable]
    public class Info
    {
        public int id;
        public string myName;
        public Sprite portrait;
        public Sprite background;
        [TextArea(3, 8)]
        public string myText;
    }

    [Header("Insert Dialogue Information Below")]
    public Info[] dialogueInfo;

}
