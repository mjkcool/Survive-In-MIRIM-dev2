using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MiddleFadeOutScript : MonoBehaviour
{
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("fix this" + gameObject.name);
        }
        else
        {
            instance = this;
        }
    }
    public static MiddleFadeOutScript instance;
    public Image FadeImage;
    float time = 0f;
    float F_time = 2f;

    public void Fade()
    {
        StartCoroutine(FadeFlow());  
    }

    IEnumerator FadeFlow()
    {
        FadeImage.gameObject.SetActive(true);
        time=0f;
        Color alpha = FadeImage.color;
        while(alpha.a<1f)
        {
            time+=Time.deltaTime/F_time;
            alpha.a = Mathf.Lerp(0,1,time);
            FadeImage.color = alpha;
            yield return null;
        }
        yield return null;
        FadeImage.gameObject.SetActive(false);
    }
}
