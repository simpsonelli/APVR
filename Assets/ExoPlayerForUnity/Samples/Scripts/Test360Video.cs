using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.palantiri.unity.videoplayer;

public class Test360Video : MonoBehaviour
{
    public Shader videoTextureShader;
    public MeshRenderer videoMeshRenderer;
    public VideoTextureController videoTextureController;

    private string[] vr360Streams =
    {
        "https://bitmovin-a.akamaihd.net/content/playhouse-vr/m3u8s/105560.m3u8",
        "https://bitmovin-a.akamaihd.net/content/playhouse-vr/mpds/105560.mpd",
    };

    private string[] sampleVideoClips =
    {
        "https://jounggj.github.io/files/SampleVideoClip1.mp4",
        "https://jounggj.github.io/files/SampleVideoClip2.mp4",
        "https://jounggj.github.io/files/SampleVideoClip3.mp4",
    };

    public float dragSpeed = 2;
    private Vector3 dragOrigin;

    void Start()
    {
        videoMeshRenderer.material = new Material(videoTextureShader);
        videoTextureController.initialize(videoMeshRenderer.material);
        //  to flip video texture horizontally
        //  if wrapping the texture inside of the sphere, we don't need this
        videoMeshRenderer.material.mainTextureScale = new Vector2(-1.0f, 1.0f);
        videoMeshRenderer.material.mainTextureOffset = new Vector2(1.0f, 0.0f);

#if UNITY_EDITOR
        string sample360Video = sampleVideoClips[2];
        videoTextureController.getPlayer().addMediaItem(ExoPlayerTypes.MediaItem.fromUri(sample360Video));
#elif UNITY_ANDROID
        foreach (string stream in vr360Streams)
        {
            videoTextureController.getPlayer().addMediaItem(ExoPlayerTypes.MediaItem.fromUri(stream));
        }
#endif
        videoTextureController.getPlayer().prepare();
        videoTextureController.getPlayer().setPlayWhenReady(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }
        if (!Input.GetMouseButton(0)) return;
        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(0.0f, pos.x * dragSpeed, 0);
        transform.Rotate(move, Space.World);
    }
}
