using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class QuestBase : ScriptableObject
{
    [System.Serializable]
    public class Info
    {
        public string myName;
        [TextArea(3, 8)]
        public string myText;
    }

    [Header("Insert QuestDialogs Information Below")]
    public Info[] QuestInfo;
}
