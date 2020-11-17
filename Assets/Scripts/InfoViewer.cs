using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoViewer : MonoBehaviour
{
    public GameObject logo;
    public GameObject infoBox;
    public GameObject btn1, btn2;

    public void showInfo()
    {
        infoBox.SetActive(true);
        logo.SetActive(false);
        btn1.SetActive(false);
        btn2.SetActive(false);
    }

    public void hideInfo()
    {
        infoBox.SetActive(false);
        logo.SetActive(true);
        btn1.SetActive(true);
        btn2.SetActive(true);
    }
}
