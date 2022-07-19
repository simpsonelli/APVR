using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.palantiri.unity.videoplayer;

public class TestVideoCube : MonoBehaviour
{
    public Shader videoTextureShader;
    public MeshRenderer videoMeshRenderer1;
    public VideoTextureController videoTextureController1;
    public MeshRenderer videoMeshRenderer2;
    public VideoTextureController videoTextureController2;

    private string[] hlsStreams =
    {
        "https://bitdash-a.akamaihd.net/content/MI201109210084_1/m3u8s/f08e80da-bf1d-4e3d-8899-f0f6155f6efa.m3u8",
        "https://bitdash-a.akamaihd.net/content/sintel/hls/playlist.m3u8",
    };

    private string[] sampleVideoClips =
    {
        "https://jounggj.github.io/files/SampleVideoClip1.mp4",
        "https://jounggj.github.io/files/SampleVideoClip2.mp4",
        "https://jounggj.github.io/files/SampleVideoClip3.mp4",
    };

    void Start()
    {
        videoMeshRenderer1.material = new Material(videoTextureShader);
        videoMeshRenderer2.material = new Material(videoTextureShader);
        videoTextureController1.initialize(videoMeshRenderer1.material);
        videoTextureController2.initialize(videoMeshRenderer2.material);

#if UNITY_EDITOR
        string video1 = sampleVideoClips[0];
        videoTextureController1.getPlayer().addMediaItem(ExoPlayerTypes.MediaItem.fromUri(video1));
        videoTextureController1.getPlayer().prepare();
        videoTextureController1.getPlayer().setPlayWhenReady(true);
        string video2 = sampleVideoClips[1];
        videoTextureController2.getPlayer().addMediaItem(ExoPlayerTypes.MediaItem.fromUri(video2));
        videoTextureController2.getPlayer().prepare();
        videoTextureController2.getPlayer().setPlayWhenReady(true);
#elif UNITY_ANDROID
        videoTextureController1.getPlayer().addMediaItem(ExoPlayerTypes.MediaItem.fromUri(sampleVideoClips[0]));
        videoTextureController1.getPlayer().addMediaItem(ExoPlayerTypes.MediaItem.fromUri(sampleVideoClips[1]));
        videoTextureController1.getPlayer().addMediaItem(ExoPlayerTypes.MediaItem.fromUri(sampleVideoClips[2]));
        videoTextureController1.getPlayer().prepare();
        videoTextureController1.getPlayer().setPlayWhenReady(true);

        foreach (string stream in hlsStreams)
        {
            videoTextureController2.getPlayer().addMediaItem(ExoPlayerTypes.MediaItem.fromUri(stream));
        }
        videoTextureController2.getPlayer().prepare();
        videoTextureController2.getPlayer().setPlayWhenReady(true);
#endif
    }

    void Update()
    {
    }
}
