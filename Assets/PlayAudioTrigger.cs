using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioTrigger : MonoBehaviour
{
    public AudioSource triggerSource0;
    public AudioSource triggerSource1;
    public AudioSource triggerSource2;
    public AudioSource triggerSource3;
    public AudioSource triggerSource4;
    public AudioSource triggerSource5;
    public AudioSource triggerSource6;
    public AudioSource triggerSource7;

    public AudioSource gallerySource;
    public AudioSource gallerySource1;
    public AudioSource gallerySource2;
    public AudioSource gallerySource3;

    public AudioClip triggerClip0;
    public AudioClip triggerClip1;
    public AudioClip triggerClip2;
    public AudioClip triggerClip3;
    public AudioClip triggerClip4;
    public AudioClip triggerClip5;
    public AudioClip triggerClip6;
    public AudioClip triggerClip7;

    public AudioClip galleryClip;
    public AudioClip galleryClip1;
    public AudioClip galleryClip2;
    public AudioClip galleryClip3;

    public GameObject Player;

    private void Start()
    {
        triggerSource0.clip = triggerClip0;
        triggerSource1.clip = triggerClip1;
        triggerSource2.clip = triggerClip2;
        triggerSource3.clip = triggerClip3;
        triggerSource4.clip = triggerClip4;
        triggerSource5.clip = triggerClip5;
        triggerSource6.clip = triggerClip6;
        triggerSource7.clip = triggerClip7;

        gallerySource.clip = galleryClip;
        gallerySource.clip = galleryClip1;
        gallerySource.clip = galleryClip2;
        gallerySource.clip = galleryClip3;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            triggerSource0.Play();
            triggerSource1.Play();
            triggerSource2.Play();
            triggerSource3.Play();
            triggerSource4.Play();
            triggerSource5.Play();
            triggerSource6.Play();
            triggerSource7.Play();


            gallerySource.Stop();
            gallerySource1.Stop();
            gallerySource2.Stop();
            gallerySource3.Stop();
        }
    }

    
}
