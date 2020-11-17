using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StarAnimManager : MonoBehaviour
{
    private Animator animator;
    public AudioClip[] star_effectSound;
    AudioSource soundSource;

    public static StarAnimManager instance;
    public void OnEnable()
    {
        animator = GetComponent<Animator>();
        soundSource = GetComponent<AudioSource>();
        StartCoroutine("Playlist");
    }

    IEnumerator Playlist() {
        soundSource.clip = star_effectSound[0];
        soundSource.Play();
        while(true){
            yield return new WaitForSeconds(1.0f);
            if(!soundSource.isPlaying) {
                soundSource.clip = star_effectSound[1];
                soundSource.Play();
                yield return new WaitForSeconds(2f);
            }
        }
        
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