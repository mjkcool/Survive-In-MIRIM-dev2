using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Toast : MonoBehaviour
{
    public static Toast instance;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (instance != null)
        {
            Debug.Log("ÆË¾÷ Á¸Àç");
        }
        else
        {
            instance = this;
            Debug.Log("ÆË¾÷ ÀÎ½ºÅÏ½º »ý¼º");
        }

    }

    // Start is called before the first frame update
    public void CloaseToast()
    {
        
        //StartCoroutine(CloseThis());
        Invoke("CloseThis", 2f);
    }

    private void CloseThis()
    {
        Debug.Log("Toast¶ä");
        animator.SetTrigger("close");
        gameObject.SetActive(false);
        animator.ResetTrigger("close");
    }

}
