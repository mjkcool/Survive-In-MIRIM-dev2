using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedalAnimManager : MonoBehaviour
{
    private Animator animator;
    public AudioClip medalSound;
    AudioSource soundSource;

    public static MedalAnimManager instance;
    public void OnEnable()
    {
        animator = GetComponent<Animator>();
        soundSource = GetComponent<AudioSource>();
        StartCoroutine("Playlist");
    }

    IEnumerator Playlist() {
        soundSource.clip = medalSound;
        soundSource.Play();
        yield return new WaitForSeconds(2f);
    }

    public void Close()
    {
       // StartCoroutine(CloseAfterDelay());
    }

    private IEnumerable CloseAfterDelay()
    {
        animator.SetTrigger("close");
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
        animator.ResetTrigger("close");
    }
}
