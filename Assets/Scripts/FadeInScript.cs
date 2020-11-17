using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEditor;

// [InitializeOnLoad]
public class FadeInScript : MonoBehaviour
{
    public Image FadeImage;
    float time = 0f;
    float F_time = 2f;

    public void Awake()
    {
        StartCoroutine(FadeFlow());
        
    }

    IEnumerator FadeFlow()
    {
        FadeImage.gameObject.SetActive(true);
        time=0f;
        Color alpha = FadeImage.color;
        while(alpha.a>0f)
        {
            time+=Time.deltaTime/F_time;
            alpha.a = Mathf.Lerp(1,0,time);
            FadeImage.color = alpha;
            yield return null;
        }
        yield return null;
        FadeImage.gameObject.SetActive(false);
    }
}
