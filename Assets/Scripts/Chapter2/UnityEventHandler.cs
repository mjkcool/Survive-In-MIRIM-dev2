using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UnityEventHandler : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent eventHandler;
    public PrologueBase Prologue; 

    public void OnPointerDown(PointerEventData PointerEventData)
    {
        eventHandler.Invoke();
        PrologueManager.prologueInstance.ChoiceClose();

        if(Prologue != null)
        {
            PrologueManager.prologueInstance.EnqueueDialogue(Prologue);
        }
    }
}
