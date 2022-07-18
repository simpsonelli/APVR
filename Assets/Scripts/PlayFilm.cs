using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayFilm : MonoBehaviour
{
    public VideoClip[] videoClips;
    private VideoPlayer videoPlayer;
    private int videoClipIndex;

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
    }

    public void PauseVideo()
    {
        videoPlayer.Pause();
    }

    public void PlayVideo()
    {
        videoPlayer.Play();
    }
}
