using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

// [InitializeOnLoad]
public class SaveNameFadeIn : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI SaveName;
    public Image FadeImage;
    float time = 0f;
    float F_time = 2f;
    public void Awake()
    {
        StartCoroutine(FadeFlow());  
    }

    IEnumerator FadeFlow()
    {
        if(!PlayerPrefs.HasKey("Name"))
        {
            SaveName.text= "User";
            Debug.Log("유저내임 로드: " +  SaveName.text);
        } else {
            string UserName = PlayerPrefs.GetString("Name");
            SaveName.text = UserName;
            Debug.Log("유저내임 로드: " +  SaveName.text);
        }
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
