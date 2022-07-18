using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySong : MonoBehaviour
{
    //public GameObject[] speakers;
    public GameObject galleryLights;
    //public AudioSource[] speakerAudio;
    public AudioSource[] rhodes;
    public AudioSource portalSound;
    public Material skyReg;
    public Material skyCourt;
    public Light light;
    public float highIntensity = .75f;
    public float lowIntensity = .000025f;
    public GameObject[] sparks;
    private bool isCalled = false;
    public Material skybox;

    public void OnTriggerEnter(Collider other)
    {
        ChangeSkybox();
        galleryLights.SetActive(false);

        foreach (GameObject sparklies in sparks)
        {
            sparklies.SetActive(true);
        }
                
        void ChangeSkybox()
        {
        if (RenderSettings.skybox == skyCourt)
        {
            RenderSettings.skybox = skyReg;
        }
        else
        {
            RenderSettings.skybox = skyCourt;
        }
    }
        if (isCalled == false && other.CompareTag("Player"))
        {
            rhodes[0].Stop();
            rhodes[1].Stop();
            rhodes[2].Stop();
            rhodes[3].Stop();
            portalSound.Play();
            light.intensity = lowIntensity;
            StartCoroutine(DelayAudioLoad());
            IEnumerator DelayAudioLoad()
            {
                yield return new WaitForSeconds(1.5f);
                //PlayAll();
                isCalled = true;
            }
        }
        else if (isCalled == true && other.CompareTag("Player"))
        {
            //StopAll();
            portalSound.Play();
            galleryLights.SetActive(true);
            light.intensity = highIntensity;

            foreach (GameObject sparklies in sparks)
            {
                sparklies.SetActive(false);
            }


            StartCoroutine(DelayAudioLoad());

            IEnumerator DelayAudioLoad()
            {
                yield return new WaitForSeconds(1.5f);
                rhodes[0].Play();
                rhodes[1].Play();
                rhodes[2].Play();
                rhodes[3].Play();
                isCalled = false;
            }
        }
    }

    //public void PlayAll()
    //{
    //    foreach (GameObject go in this.speakers)
    //        go.GetComponent<AudioSource>().Play();
    //}

    //public void StopAll()
    //{
    //    foreach (GameObject go in this.speakers)
    //        go.GetComponent<AudioSource>().Stop();
    //}

}
