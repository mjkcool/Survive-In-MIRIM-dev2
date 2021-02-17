using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class SetDepartment : ScriptableObject
{
    string soft = "software";
    string solu = "solution";
    string de = "design";

    public void SetSoft()
    {
        Debug.Log("소프트과");
        PlayerPrefs.SetString("Department",soft);
        PlayerPrefs.Save(); 
    }

    public void Setsolu()
    {
        Debug.Log("솔루션과");
        PlayerPrefs.SetString("Department",solu);
        PlayerPrefs.Save(); 
    }

    public void Setde()
    {
        Debug.Log("디자인과");
        PlayerPrefs.SetString("Department",de);
        PlayerPrefs.Save(); 
    }
}
