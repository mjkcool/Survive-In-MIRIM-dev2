using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoViewer : MonoBehaviour
{
    public GameObject bigcloud, cloud;
    public GameObject titleLogo; 
    public GameObject map;
    public GameObject infoBox;
    public GameObject error;
    public GameObject btn;

    public void showInfo()
    {
        infoBox.SetActive(true);
        bigcloud.SetActive(false);
        cloud.SetActive(false);
        titleLogo.SetActive(false);
        error.SetActive(false);
        map.SetActive(false);
        btn.SetActive(false);
    }

    public void hideInfo()
    {
        infoBox.SetActive(false);
        bigcloud.SetActive(true);
        cloud.SetActive(true);
        titleLogo.SetActive(true);
        error.SetActive(true);
        map.SetActive(true);
        btn.SetActive(true);
    }
}
