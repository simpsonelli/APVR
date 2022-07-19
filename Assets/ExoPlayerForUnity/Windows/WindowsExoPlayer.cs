using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace com.palantiri.unity.videoplayer
{
    public class WindowsExoPlayer : SimpleExoPlayer
    {
        private GameObject gameObject = null;
        private UnityEngine.Video.VideoPlayer videoPlayer = null;
        private RenderTexture videoTargetTexture = null;
        private Vector2 videoTextureScale = new Vector2(1.0f, 1.0f);

        private bool isVideoLoading = false;
        private bool isVideoSeeking = false;
        private bool isPlayWhenReady = true;
        private List<IPlayerEventListener> eventListeners = null;
        private List<IPlayerAudioListener> audioListeners = null;
        private List<IPlayerVideoComponent> videoListeners = null;
        private List<IPlayerTextOutputListener> textOutputListeners = null;
        private List<IPlayerDeviceListener> deviceListeners = null;

        private ExoPlayerTypes.SeekParameters currentSeekParameters;
        private ExoPlayerTypes.PlaybackParameters currentPlaybackParameters;
        private ExoPlayerTypes.Format currentAudioFormat = null;
        private ExoPlayerTypes.Format currentVideoFormat = null;


        public WindowsExoPlayer() { }


        //-----------------------------------------------------------------------
        //  ILifecycleListener commands
        public override void onStart() { }
        public override void onUpdate() { }
        public override void onApplicationPause() { }
        public override void onDestroy() { }
        public override void onApplicationQuit() { }

        //-----------------------------------------------------------------------
        //  Unity related methods
        public override Texture initialize(GameObject gameObject)
        {
            this.gameObject = gameObject;

            videoTargetTexture = new RenderTexture(1280, 720, 0);
            videoPlayer = gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
            videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.RenderTexture;
            videoPlayer.targetTexture = videoTargetTexture;

            eventListeners = new List<IPlayerEventListener>();
            currentSeekParameters = new ExoPlayerTypes.SeekParameters();
            currentAudioFormat = new ExoPlayerTypes.Format();
            currentVideoFormat = new ExoPlayerTypes.Format();

            //videoPlayer.clockResyncOccurred += onVideoClockResyncOccurred;
            //videoPlayer.errorReceived += onVideoErrorReceived;
            //videoPlayer.frameDropped += onVideoFrameDropped;
            //videoPlayer.frameReady += onVideoFrameReady;
            //videoPlayer.loopPointReached += onVideoLoopPointReached;
            //videoPlayer.prepareCompleted += onVideoPrepareCompleted;
            //videoPlayer.seekCompleted += onVideoSeekCompleted;
            //videoPlayer.started += onVideoStarted;

            return videoTargetTexture;
        }

        public override void finalize()
        {
            Object.Destroy(videoTargetTexture);
            videoTargetTexture = null;
        }

        public override bool updateVideoTexture()
        {
            return false;
        }

        public override Vector2 getVideoTextureScale()
        {
            return videoTextureScale;
        }

        //-----------------------------------------------------------------------
        //  IExoPlayer methods

        // event listeners
        public override void addEventListener(IPlayerEventListener listener)
        {
            eventListeners.Add(listener);
        }

        public override void removeEventListener(IPlayerEventListener listener)
        {
            eventListeners.Remove(listener);
        }

        // components
        public override IPlayerAudioComponent getAudioComponent() { return this; }
        public override IPlayerVideoComponent getVideoComponent() { return this; }
        public override IPlayerTextComponent getTextComponent() { return this; }
        public override IPlayerDeviceComponent getDeviceComponent() { return this; }


        // MediaItem handling
        public override void addMediaItem(int index, ExoPlayerTypes.MediaItem mediaItem)
        {
            if (videoPlayer != null) videoPlayer.url = mediaItem.mediaId;
        }
        public override void addMediaItem(ExoPlayerTypes.MediaItem mediaItem)
        {
            if (videoPlayer != null) videoPlayer.url = mediaItem.mediaId;
        }
        //public override void addMediaItems(int index, List<ExoPlayerTypes.MediaItem> mediaItems) { }
        //public override void addMediaItems(List<ExoPlayerTypes.MediaItem> mediaItems) { }
        public override void setMediaItem(ExoPlayerTypes.MediaItem mediaItem) { }
        public override void setMediaItem(ExoPlayerTypes.MediaItem mediaItem, bool resetPosition) { }
        public override void setMediaItem(ExoPlayerTypes.MediaItem mediaItem, long startPositionMs) { }
        //public override void setMediaItems(List<ExoPlayerTypes.MediaItem> mediaItems) { }
        //public override void setMediaItems(List<ExoPlayerTypes.MediaItem> mediaItems, bool resetPosition) { }
        //public override void setMediaItems(List<ExoPlayerTypes.MediaItem> mediaItems, int startWindowIndex, long startPositionMs) { }
        public override void moveMediaItem(int currentIndex, int newIndex) { }
        public override void moveMediaItems(int fromIndex, int toIndex, int newIndex) { }
        public override void removeMediaItem(int index) { }
        public override void removeMediaItems(int fromIndex, int toIndex) { }
        public override void clearMediaItems() { }
        public override ExoPlayerTypes.MediaItem getCurrentMediaItem() { return new ExoPlayerTypes.MediaItem(); }
        public override ExoPlayerTypes.MediaItem getMediaItemAt(int index) { return new ExoPlayerTypes.MediaItem(); }
        public override int getMediaItemCount() { return 0; }

        // playback control
        public override void next() { }
        public override void pause() { videoPlayer?.Pause(); }
        public override void play() { videoPlayer?.Play(); }
        public override void prepare() { if (isPlayWhenReady) videoPlayer?.Play(); }
        public override void previous() { }
        public override void release() { }
        public override void stop() { videoPlayer?.Stop(); }
        public override void stop(bool reset) { videoPlayer?.Stop(); }
        public override void seekTo(int windowIndex, long positionMs) { }
        public override void seekTo(long positionMs) { }
        public override void seekToDefaultPosition() { }
        public override void seekToDefaultPosition(int windowIndex) { }

        // playback status query
        public override bool hasNext() { return false; }
        public override bool hasPrevious() { return false; }
        public override bool isCurrentWindowDynamic() { return false; }
        public override bool isCurrentWindowLive() { return false; }
        public override bool isCurrentWindowSeekable() { return false; }
        public override bool isLoading() { return false; }
        public override bool isPlaying() { return false; }
        public override bool isPlayingAd() { return false; }
        public override int getBufferedPercentage() { return 0; }
        public override long getBufferedPosition() { return 0; }
        public override long getContentBufferedPosition() { return 0; }
        public override long getContentDuration() { return 0; }
        public override long getContentPosition() { return 0; }
        public override int getCurrentAdGroupIndex() { return 0; }
        public override int getCurrentAdIndexInAdGroup() { return 0; }
        public override long getCurrentLiveOffset() { return 0; }
        public override int getCurrentPeriodIndex() { return 0; }
        public override long getCurrentPosition() { return 0; }
        public override int getCurrentWindowIndex() { return 0; }
        public override long getDuration() { return 0; }
        public override int getNextWindowIndex() { return 0; }
        public override int getPlaybackState() { return 0; }
        public override int getPlaybackSuppressionReason() { return 0; }
        public override int getPreviousWindowIndex() { return 0; }
        public override int getRendererCount() { return 0; }
        public override int getRendererType(int index) { return 0; }
        public override long getTotalBufferedDuration() { return 0; }

        // getters/setters
        public override ExoPlayerTypes.PlaybackParameters getPlaybackParameters() { return currentPlaybackParameters; }
        public override void setPlaybackParameters(ExoPlayerTypes.PlaybackParameters playbackParameters) { currentPlaybackParameters = playbackParameters; }
        public override bool getPlayWhenReady() { return false; }
        public override void setPlayWhenReady(bool playWhenReady) { }
        public override int getRepeatMode() { return 0; }
        public override void setRepeatMode(int repeatMode) { }
        public override bool getShuffleModeEnabled() { return false; }
        public override void setShuffleModeEnabled(bool shuffleModeEnabled) { }

        //  ExoPlayer methods
        public override bool getPauseAtEndOfMediaItems() { return false; }
        public override void setPauseAtEndOfMediaItems(bool pauseAtEndOfMediaItems) { }
        public override ExoPlayerTypes.SeekParameters getSeekParameters() {
            return currentSeekParameters;
        }

        public override void setSeekParameters(ExoPlayerTypes.SeekParameters seekParameters) {
            currentSeekParameters = seekParameters;
        }

        public override void setForegroundMode(bool foregroundMode) { }

        //-----------------------------------------------------------------------
        //  IPlayerAudioComponent commands
        public override void addAudioListener(IPlayerAudioListener listener) { }
        public override void removeAudioListener(IPlayerAudioListener listener) { }
        public override ExoPlayerTypes.AudioAttributes getAudioAttributes() { return new ExoPlayerTypes.AudioAttributes(); }
        public override void setAudioAttributes(ExoPlayerTypes.AudioAttributes audioAttributes, bool handleAudioFocus) { }
        public override int getAudioSessionId() { return 0; }
        public override void setAudioSessionId(int audioSessionId) { }
        public override float getVolume() { return 0.0f; }
        public override void setVolume(float audioVolume) { }
        public override bool getSkipSilenceEnabled() { return false; }
        public override void setSkipSilenceEnabled(bool skipSilenceEnabled) { }
        public override void setAuxEffectInfo(ExoPlayerTypes.AuxEffectInfo auxEffectInfo) { }
        public override void clearAuxEffectInfo() { }

        //-----------------------------------------------------------------------
        //  IPlayerVideoComponent commands
        public override void addVideoListener(IPlayerVideoListener listener) { }
        public override void removeVideoListener(IPlayerVideoListener listener) { }
        public override void setVideoFrameMetadataListener(IPlayerVideoFrameMetadataListener listener) { }
        public override void clearVideoFrameMetadataListener(IPlayerVideoFrameMetadataListener listener) { }

        //-----------------------------------------------------------------------
        //  IPlayerTextComponent commands
        public override void addTextOutputListener(IPlayerTextOutputListener listener) { }
        public override void removeTextOutputListener(IPlayerTextOutputListener listener) { }
        public override List<ExoPlayerTypes.Cue> getCurrentCues() { return null; }

        //-----------------------------------------------------------------------
        //  IPlayerDeviceComponent commands
        public override void addDeviceListener(IPlayerDeviceListener listener) { }
        public override void removeDeviceListener(IPlayerDeviceListener listener) { }
        public override void setDeviceVolume(int volume) { }
        public override int getDeviceVolume() { return 0; }
        public override void increaseDeviceVolume() { }
        public override void decreaseDeviceVolume() { }
        public override void setDeviceMuted(bool muted) { }
        public override bool isDeviceMuted() { return false; }
        public override ExoPlayerTypes.DeviceInfo getDeviceInfo() { return new ExoPlayerTypes.DeviceInfo(); }
    }
}
