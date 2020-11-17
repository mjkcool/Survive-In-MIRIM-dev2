using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LoadingAnimManager : MonoBehaviour
{
    private Animator animator;

    public static LoadingAnimManager instance;
    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Close()
    {
       // StartCoroutine(CloseAfterDelay());
    }

    private IEnumerable CloseAfterDelay()
    {
        animator.SetTrigger("close");
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
        animator.ResetTrigger("close");
    }
}