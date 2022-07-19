using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class PlayFilm : MonoBehaviour
{
    public VideoClip[] videoClips;
    private VideoPlayer videoPlayer;
    private int videoClipIndex;
    public GameObject directionalLight;
    public GameObject pointLights;
   


    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    private void Start()
    {
        videoPlayer.clip = videoClips[0];
    }

    public void PlayNext()
    {
        videoClipIndex++;
        if (videoClipIndex >= videoClips.Length)
        {
            videoClipIndex = videoClipIndex % videoClips.Length;
        }
        videoPlayer.clip = videoClips[videoClipIndex];
        videoPlayer.Play();
        directionalLight.SetActive(false);
        pointLights.SetActive(false);
        
    }

    public void PlayPrevious()
    {
        videoClipIndex--;
        if (videoClipIndex >= videoClips.Length)
        {
            videoClipIndex = videoClipIndex % videoClips.Length;
        }
        videoPlayer.clip = videoClips[videoClipIndex];
        videoPlayer.Play();
        directionalLight.SetActive(false);
        pointLights.SetActive(false);
    }

    public void PauseVideo()
    {
        videoPlayer.Pause();
        directionalLight.SetActive(true);
        pointLights.SetActive(true);

    }

    public void PlayVideo()
    {
        videoPlayer.Play();
        directionalLight.SetActive(false);
        pointLights.SetActive(false);
    }
}
