using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Prologue", menuName = "Prologue")]
public class PrologueBase : ScriptableObject
{
    [System.Serializable]
    public class Info
    {
        public int prologueId;
        public string prologueName;
        public Sprite portrait;
        [TextArea(3, 8)]
        public string prologueText;
    }

    [Header("Insert Dialogue Information Below")]
    public Info[] prologueInfo;

}
