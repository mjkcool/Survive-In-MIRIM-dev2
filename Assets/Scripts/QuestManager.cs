using System;
using UnityEngine;
using System.Globalization;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public AudioClip correctSound;
    public AudioClip incorrectSound;
    private AudioSource audio;

    public void Start()
    {
        audio = GetComponent<AudioSource>();
    }

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

    public void DelaySystem(int MS) //시간 지연 함수
    {
        DateTime dtAfter = DateTime.Now;
        TimeSpan dtDuration = new TimeSpan(0, 0, 0, 0, MS);
        DateTime dtThis = dtAfter.Add(dtDuration);

        while (dtThis >= dtAfter)
        {
            dtAfter = DateTime.Now;
        }
    }

    //anim
    public GameObject QuestObj;
    public GameObject LoadingAnimation;
    public GameObject LoadingGround;
    public GameObject StarGround;
    public GameObject SpinStarAnimation;
    public GameObject ExplodeAnimation;
    public GameObject success;
    public GameObject failure;

    private bool correct;

     public void spinStar()
    {
        StarGround.SetActive(true);
        SpinStarAnimation.SetActive(true);
        ExplodeAnimation.SetActive(true);
        Invoke("spinStar2", 4.3f);
        Debug.Log("questSuccess");
    }
    private void spinStar2()
    {
        StarGround.SetActive(false); 
        SpinStarAnimation.SetActive(false);
        ExplodeAnimation.SetActive(false);
    }


    public void startLoading(bool correct)
    {
        QuestObj.SetActive(false);
        this.correct = correct;
        LoadingGround.SetActive(true);
        LoadingAnimation.SetActive(true);
        Invoke("loading2", 3f);
        Debug.Log("iscorrect");
    }
    private void loading2()
    {
        LoadingAnimation.SetActive(false);
        if (correct) {
            success.SetActive(true); 
            GetComponent<AudioSource>().clip = correctSound;
            GetComponent<AudioSource>().Play();
        }
        else {
            failure.SetActive(true); 
            GetComponent<AudioSource>().clip = incorrectSound;
            GetComponent<AudioSource>().Play();
        }
        Invoke("loading3", 2f);
    }
    private void loading3()
    {
        success.SetActive(false); failure.SetActive(false);
        LoadingGround.SetActive(false);
        QuestObj.SetActive(true);
    }


}
