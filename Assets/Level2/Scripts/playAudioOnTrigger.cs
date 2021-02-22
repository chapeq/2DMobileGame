using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAudioOnTrigger : MonoBehaviour
{
    public Animator animAudio1;
    public AudioSource Audio2;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            animAudio1.SetFloat("animSpeed", 1.5f);
            animAudio1.SetTrigger("FadeOut");
            StartCoroutine(playAudio());
        }
    }

    IEnumerator playAudio()
    {
        yield return new WaitForSeconds(3f);
        Audio2.Play();
    }
}
