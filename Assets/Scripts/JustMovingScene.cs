using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class JustMovingScene : MonoBehaviour
{
    public int NextSceneNumber;
   public void justMove()
    {
        SceneManager.LoadScene(NextSceneNumber);
    }
}
