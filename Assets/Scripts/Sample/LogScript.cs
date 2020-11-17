using UnityEngine;
using UnityEngine.UI;

public class LogScript : MonoBehaviour
{
    public GameObject scrollLog;
    public GameObject logView;

    Text logText;
    bool isShowing;
    
    void Start()
    {
        logText = logView.GetComponent<Text>();
        isShowing = scrollLog.activeSelf;
    }

    public void Log(string message)
    {
        Log(null, message);
    }

    public void Log(string tag, string message)
    {
        string result;
        if (tag == null || tag.Length == 0)
            result = message;
        else
            result = "[" + tag + "]: " + message;

        Debug.Log(result);

        if (logText != null)
            logText.text += "\n[" + System.DateTime.Now.ToString("hh:mm:ss") + "]::" + result;
    }

    public void ShowHideLog()
    {
        isShowing = !isShowing;
        scrollLog.SetActive(isShowing);
    }

    public void ClearLog()
    {
        if (logText != null)
            logText.text = "";
    }

    public void AddEnter()
    {
        if (logText != null)
        {
            logText.text += "\n";
            scrollLog.GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
        }
    }
}
