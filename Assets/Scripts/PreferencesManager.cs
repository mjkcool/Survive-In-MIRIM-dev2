using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreferencesManager : MonoBehaviour
{
    public Sprite[] mutePort = new Sprite[2], unmutedPort = new Sprite[2];
    public Image onBtn, offBtn;

    public void MuteAllSound()
    {
        AudioListener.volume = 0; //음소거
        SoundBtnColorControl.instance.muteSound();
        onBtn.sprite = unmutedPort[0];
        offBtn.sprite = mutePort[1];

    }
    public void UnMuteAllSound()
    {
        AudioListener.volume = 1; //음소거해제
        onBtn.sprite = unmutedPort[1];
        offBtn.sprite = mutePort[0];

    }

    public void DeletaAll()
    {
        PlayerPrefs.DeleteAll(); //데이터 초기화

    }

}
