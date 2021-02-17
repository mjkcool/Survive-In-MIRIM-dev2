using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue2", menuName = "Dialogues2")]
public class DialogueBase2 : ScriptableObject
{
    [System.Serializable]
    public class Info
    {
        public int id2;
        public string myName2;
        public Sprite portrait2;
        [TextArea(3, 8)]
        public string myText2;
    }

    [Header("Insert Dialogue Information Below")]
    public Info[] dialogueInfo2;

}
