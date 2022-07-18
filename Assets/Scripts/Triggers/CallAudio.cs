using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Player" && !audioSource.isPlaying)
        {
            GetComponent<PlaySong>();
        }

        if(gameObject.tag == "Player" && audioSource.isPlaying)
        {
            audioSource.enabled =!enabled;
        }
    }

   
}
