using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class SoundBtnColorControl : MonoBehaviour
{

    public Sprite[] mutePort = new Sprite[2], unmutedPort = new Sprite[2];
    public Image onBtn, offBtn;

    public static SoundBtnColorControl instance;

    public void Awake()
    {
        if (instance != null)
        {

        }
        else
        {
            instance = this;
        }
    }

    

    public void unmuteSound() //ON버튼 클릭시
    {
        onBtn.sprite = unmutedPort[1];
        offBtn.sprite = mutePort[0];
    }
    public void muteSound() //OFF버튼 클릭시
    {
        onBtn.sprite = unmutedPort[0];
        offBtn.sprite = mutePort[1];
    }
}
