using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.palantiri.unity.videoplayer
{
    public class VideoPlayerController : MonoBehaviour,
        IPlayerAudioListener, IPlayerVideoListener, IPlayerVideoFrameMetadataListener, IPlayerTextOutputListener
    {
        public RawImage videoRawImage;
        public Text videoUrl;
        public Slider volumeSlider;

        private SimpleExoPlayer exoPlayer = null;
        private bool initialized = false;
        private readonly long STEP_DURATION = 5000;

        private void Awake()
        {
#if UNITY_EDITOR
            exoPlayer = new WindowsExoPlayer();
#elif UNITY_ANDROID
            exoPlayer = new AndroidExoPlayer();
#endif
            volumeSlider.onValueChanged.AddListener(delegate { setVolume(); });
        }

        // Start is called before the first frame update
        void Start()
        {
            exoPlayer?.onStart();
        }

        // Update is called once per frame
        void Update()
        {
            if (exoPlayer == null) return;

            if (exoPlayer.updateVideoTexture())
            {
                videoRawImage.material.mainTextureScale = exoPlayer.getVideoTextureScale();
            }
            exoPlayer?.onUpdate();
        }

        private void OnApplicationPause(bool pause)
        {
            exoPlayer?.onApplicationPause();
        }

        private void OnApplicationQuit()
        {
            exoPlayer?.onApplicationQuit();
        }

        private void OnDestroy()
        {
            exoPlayer?.onDestroy();
        }

        public void initialize()
        {
#if UNITY_EDITOR
            RenderTexture videoTexture = (RenderTexture)exoPlayer?.initialize(gameObject);
            videoRawImage.texture = videoTexture;
#elif UNITY_ANDROID
            videoRawImage.texture = exoPlayer?.initialize(gameObject);
            exoPlayer?.getAudioComponent().addAudioListener(this);
            //exoPlayer?.getVideoComponent().addVideoListener(this);
            //exoPlayer?.getTextComponent().addTextOutputListener(this);
            //exoPlayer?.getDeviceComponent().addDeviceListener(this);
            //exoPlayer?.getVideoComponent().setVideoFrameMetadataListener(this);
#endif
        }

        //-----------------------------------------------------------------------
        //  Unity related methods
        public SimpleExoPlayer getPlayer()
        {
            return exoPlayer;
        }

        public void loadVideo(string path)
        {
            ExoPlayerTypes.MediaItem mediaItem = ExoPlayerTypes.MediaItem.fromUri(path);
            exoPlayer?.addMediaItem(mediaItem);
            exoPlayer?.setPlayWhenReady(false);
            exoPlayer?.prepare();
        }

        public void play()
        {
            exoPlayer?.play();
        }

        public void pause()
        {
            exoPlayer?.pause();
        }

        public void stop()
        {
            exoPlayer?.stop(false);
        }

        public void replay()
        {
            exoPlayer?.seekTo(0);
            exoPlayer?.setPlayWhenReady(true);
        }

        public void firstTrack()
        {
            exoPlayer?.previous();
        }

        public void fastRewind()
        {
            if (exoPlayer == null) return;
            long currentPosition = exoPlayer.getCurrentPosition();
            currentPosition = (currentPosition >= STEP_DURATION) ? (currentPosition - STEP_DURATION) : 0;
            exoPlayer.seekTo(currentPosition);
        }

        public void fastForward()
        {
            if (exoPlayer == null) return;
            long currentPosition = exoPlayer.getCurrentPosition();
            long duration = exoPlayer.getDuration();
            currentPosition += STEP_DURATION;
            if (currentPosition > duration) currentPosition = duration;
            exoPlayer.seekTo(currentPosition);
            if (currentPosition == duration) exoPlayer.pause();
        }

        public void lastTrack()
        {
            exoPlayer?.next();
        }

        public void setVolume()
        {
            exoPlayer?.setVolume(volumeSlider.value);
        }

        //-----------------------------------------------------------------------
        //  IPlayerAudioListener methods
        public void onAudioAttributesChanged(ExoPlayerTypes.AudioAttributes audioAttributes)
        {
            //Debug.Log("VideoPlayerController:  onAudioAttributesChanged:  called.");
            //Debug.Log("--  audioAttributes.allowedCapturePolicy = " + audioAttributes.allowedCapturePolicy);
            //Debug.Log("--  audioAttributes.contentType          = " + audioAttributes.contentType);
            //Debug.Log("--  audioAttributes.flags                = " + audioAttributes.flags);
            //Debug.Log("--  audioAttributes.usage                = " + audioAttributes.usage);
        }

        public void onAudioSessionId(int audioSessionId)
        {
            //Debug.Log("VideoPlayerController:  onAudioSessionId:  called:  audioSessionId = " + audioSessionId);
        }

        public void onVolumeChanged(float volume)
        {
            //Debug.Log("VideoPlayerController:  onVolumeChanged:  called:  volume = " + volume);
        }

        //-----------------------------------------------------------------------
        //  IPlayerVideoListener methods
        public void onRenderedFirstFrame()
        {
            //Debug.Log("VideoPlayerController:  onRenderedFirstFrame:  called.");
        }

        public void onVideoSizeChanged(int width, int height, int unappliedRotationDegrees, float pixelWidthHeightRatio)
        {
            //Debug.Log("VideoPlayerController:  onVideoSizeChanged:  called:  " +
            //    String.Format("size = [ {0} , {1} ]   Rotation = {2}  Ratio = {3}",
            //    width, height, unappliedRotationDegrees, pixelWidthHeightRatio));
        }

        //-----------------------------------------------------------------------
        //  IPlayerVideoFrameMetadataListener methods
        public void onVideoFrameAboutToBeRendered(long presentationTimeUs, long releaseTimeNs, ExoPlayerTypes.Format format)
        {
            //Debug.Log("VideoPlayerController:  onVideoFrameAboutToBeRendered:  called.");
        }

        //-----------------------------------------------------------------------
        //  IPlayerTextOutputListener methods
        public void onCues(List<ExoPlayerTypes.Cue> cues)
        {
            //Debug.Log("VideoPlayerController:  onCues:  called.");
        }

        //-----------------------------------------------------------------------
        //  IPlayerDeviceListener methods
        public void onDeviceInfoChanged(ExoPlayerTypes.DeviceInfo deviceInfo)
        {
            //Debug.Log("VideoPlayerController:  onDeviceInfoChanged:  called.");
        }

        public void onDeviceVolumeChanged(int volume, bool muted)
        {
            //Debug.Log("VideoPlayerController:  onDeviceVolumeChanged:  called.");
        }
    }
}
