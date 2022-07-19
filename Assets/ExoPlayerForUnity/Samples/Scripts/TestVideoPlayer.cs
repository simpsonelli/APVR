using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using com.palantiri.unity.videoplayer;
using System;

public class TestVideoPlayer : MonoBehaviour
{
    public VideoPlayerController videoController;
    public Slider volumeSlider;

    private string[] hlsStreams =
    {
        "https://bitdash-a.akamaihd.net/content/MI201109210084_1/m3u8s/f08e80da-bf1d-4e3d-8899-f0f6155f6efa.m3u8",
        "https://bitdash-a.akamaihd.net/content/sintel/hls/playlist.m3u8",
    };

    private string[] vr360Streams =
    {
        "https://bitmovin-a.akamaihd.net/content/playhouse-vr/m3u8s/105560.m3u8",
        "https://bitmovin-a.akamaihd.net/content/playhouse-vr/mpds/105560.mpd",
    };

    private string[] bitmovinStreams =
    {
        "https://bitmovin-a.akamaihd.net/content/art-of-motion_drm/m3u8s/11331.m3u8",
        "https://test.playready.microsoft.com/smoothstreaming/SSWSS720H264/SuperSpeedway_720.ism/manifest",
    };

    private string[] sampleVideoClips =
    {
        "https://jounggj.github.io/files/SampleVideoClip1.mp4",
        "https://jounggj.github.io/files/SampleVideoClip2.mp4",
        "https://jounggj.github.io/files/SampleVideoClip3.mp4",
    };

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    class TestEventListener : IPlayerEventListener
    {
        private VideoPlayerController mController = null;
        public TestEventListener(VideoPlayerController controller)
        {
            mController = controller;
        }

        public void onIsLoadingChanged(bool isLoading)
        {
        }

        public void onIsPlayingChanged(bool isPlaying)
        {
        }

        public void onMediaItemTransition(ExoPlayerTypes.MediaItem mediaItem, int reason)
        {
        }

        public void onPlaybackParametersChanged(ExoPlayerTypes.PlaybackParameters playbackParameters)
        {
        }

        public void onPlaybackStateChanged(int state)
        {
        }

        public void onPlaybackSuppressionReasonChanged(int playbackSuppressionReason)
        {
        }

        public void onPlayerError(ExoPlayerTypes.ExoPlaybackException error)
        {
            //Debug.Log("onPlayerError:  error.type = " + ((ExoPlayerTypes.ExoPlaybackException.Type)error.type).ToString());
            switch (error.type)
            {
                case (int)ExoPlayerTypes.ExoPlaybackException.Type.SOURCE:
                    break;
                case (int)ExoPlayerTypes.ExoPlaybackException.Type.RENDERER:
                    break;
                case (int)ExoPlayerTypes.ExoPlaybackException.Type.UNEXPECTED:
                    break;
                case (int)ExoPlayerTypes.ExoPlaybackException.Type.REMOTE:
                    break;
                case (int)ExoPlayerTypes.ExoPlaybackException.Type.OUT_OF_MEMORY:
                    break;
                case (int)ExoPlayerTypes.ExoPlaybackException.Type.TIMEOUT:
                    break;
            }
            mController.getPlayer().stop(false);
            mController.getPlayer().next();
            mController.getPlayer().prepare();
        }

        public void onPlayWhenReadyChanged(bool playWhenReady, int reason)
        {
        }

        public void onPositionDiscontinuity(int reason)
        {
        }

        public void onRepeatModeChanged(int repeatMode)
        {
        }

        public void onShuffleModeEnabledChanged(bool shuffleModeEnabled)
        {
        }
    }

    class TestVideoListener : IPlayerVideoListener
    {
        private VideoPlayerController mController = null;
        public TestVideoListener(VideoPlayerController controller)
        {
            mController = controller;
        }

        public void onRenderedFirstFrame()
        {
            if (mController == null) return;
            ExoPlayerTypes.MediaItem mediaItem = mController.getPlayer().getCurrentMediaItem();
            if (mediaItem == null) return;
            mController.videoUrl.text = mediaItem.mediaId;
        }

        public void onVideoSizeChanged(int width, int height, int unappliedRotationDegrees, float pixelWidthHeightRatio)
        {
        }
    }

    public void initializePlayer()
    {
        videoController.initialize();
        volumeSlider.onValueChanged.AddListener(delegate { onVolumeChanged(); });
        videoController.getPlayer().addEventListener(new TestEventListener(videoController));
        videoController.getPlayer().addVideoListener(new TestVideoListener(videoController));
    }

    public void loadVideo()
    {
#if UNITY_EDITOR
        string testVideoFilename = sampleVideoClips[0];
        videoController.getPlayer().addMediaItem(ExoPlayerTypes.MediaItem.fromUri(testVideoFilename));
#elif UNITY_ANDROID
        foreach (string stream in hlsStreams)
        {
            videoController.getPlayer().addMediaItem(ExoPlayerTypes.MediaItem.fromUri(stream));
        }
#endif
        videoController.getPlayer().prepare();
        videoController.getPlayer().setPlayWhenReady(true);
    }

    private void onVolumeChanged()
    {
        videoController.getPlayer().setVolume(volumeSlider.value);
    }

    public void finalizePlayer()
    {
        videoController.getPlayer().finalize();
    }

    public void quitApplication()
    {
        Application.Quit();
    }
}
