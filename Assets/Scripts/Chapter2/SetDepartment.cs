using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class SetDepartment : ScriptableObject
{
    public void SetSoft()
    {
        Debug.Log("소프트과");
        PlayerPrefs.SetInt("Department",0);
        PlayerPrefs.Save(); 
    }

    public void Setsolu()
    {
        Debug.Log("솔루션과");
        PlayerPrefs.SetInt("Department",1);
        PlayerPrefs.Save(); 
    }

    public void Setde()
    {
        Debug.Log("디자인과");
        PlayerPrefs.SetInt("Department",2);
        PlayerPrefs.Save(); 
    }
}
