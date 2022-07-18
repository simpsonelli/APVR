using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerManager : MonoBehaviour
{
    public GameObject speaker;
    public AudioSource speakerClip;

    void Start()
    {
        speaker.GetComponent<AudioSource>();
        speakerClip.Play();
    }

   
}
