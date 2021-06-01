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
        [TextArea(3, 8)]
        public string myText;
        public Sprite portrait;
        public Sprite bgSprite;
        public bool isDelayed;
    }

    [Header("Insert Dialogue Information Below")]
    public Info[] dialogueInfo;

}
