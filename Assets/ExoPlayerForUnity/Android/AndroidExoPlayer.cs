using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.palantiri.unity.videoplayer
{
    internal class AndroidExoPlayerMessageListener : MonoBehaviour
    {
        [Serializable]
        private class VideoFrameMetadata
        {
            public long presentationTimeUs;
            public long releaseTimeNs;

            //  common fields
            public string id;
            public string label;
            public string language;
            public int selectionFlags;
            public int roleFlags;
            //    public int averageBitrate;
            //    public int peakBitrate;
            public int bitrate;         //  peakBitrate != NO_VALUE ? peakBitrate : averageBitrate
            public string codecs;
            //    public Metadata metadata;

            //  container related fields
            public string containerMimeType;

            //  sample format related fields
            public string sampleMimeType;
            public int maxInputSize;
            public long subsampleOffsetUs;
            //    public List<byte[]> initializationData;
            //    public DrmInitData drmInitData;
            //    public ExoMediaCrypto exoMediaCryptoType;

            //  video related fields
            public int width;
            public int height;
            public float frameRate;
            public int rotationDegrees;
            public float pixelWidthHeightRatio;
            //    public byte[] projectionData;
            public int stereoMode;
            //    public ColorInfo colorInfo;

            //  audio related fields
            public int channelCount;
            public int sampleRate;
            public int pcmEncoding;
            public int encoderDelay;
            public int encoderPadding;

            //  text related fields
            public int accessibilityChannel;

            public ExoPlayerTypes.Format getFormat()
            {
                ExoPlayerTypes.Format format = new ExoPlayerTypes.Format();
                format.id = this.id;
                format.label = this.label;
                format.language = this.language;
                format.selectionFlags = this.selectionFlags;
                format.roleFlags = this.roleFlags;
                format.bitrate = this.bitrate;
                format.codecs = this.codecs;
                format.containerMimeType = this.containerMimeType;
                format.sampleMimeType = this.sampleMimeType;
                format.maxInputSize = this.maxInputSize;
                format.subsampleOffsetUs = this.subsampleOffsetUs;
                format.width = this.width;
                format.height = this.height;
                format.frameRate = this.frameRate;
                format.rotationDegrees = this.rotationDegrees;
                format.pixelWidthHeightRatio = this.pixelWidthHeightRatio;
                format.stereoMode = this.stereoMode;
                format.channelCount = this.channelCount;
                format.sampleRate = this.sampleRate;
                format.pcmEncoding = this.pcmEncoding;
                format.encoderDelay = this.encoderDelay;
                format.encoderPadding = this.encoderPadding;
                format.accessibilityChannel = this.accessibilityChannel;
                return format;
            }
        }

        private AndroidExoPlayer exoPlayer = null;

        public void initialize(AndroidExoPlayer player)
        {
            exoPlayer = player;
        }

        //-----------------------------------------------------------------------
        //  IPlayerAudioComponent methods
        public void PEL_onIsLoadingChanged(string isLoading)
        {
            bool value = Boolean.Parse(isLoading);
            exoPlayer.onIsLoadingChanged(value);
        }

        public void PEL_onIsPlayingChanged(string isPlaying)
        {
            bool value = Boolean.Parse(isPlaying);
            exoPlayer.onIsPlayingChanged(value);
        }

        public void PEL_onPlaybackStateChanged(string state)
        {
            int value = Int32.Parse(state);
            exoPlayer.onPlaybackStateChanged(value);
        }

        public void PEL_onPlaybackSuppressionReasonChanged(string playbackSuppressionReason)
        {
            int value = Int32.Parse(playbackSuppressionReason);
            exoPlayer.onPlaybackSuppressionReasonChanged(value);
        }

        public void PEL_onRepeatModeChanged(string repeatMode)
        {
            int value = Int32.Parse(repeatMode);
            exoPlayer.onRepeatModeChanged(value);
        }

        public void PEL_onShuffleModeEnabledChanged(string shuffleModeEnabled)
        {
            bool value = Boolean.Parse(shuffleModeEnabled);
            exoPlayer.onShuffleModeEnabledChanged(value);
        }

        public void PEL_onPositionDiscontinuity(string reason)
        {
            int value = Int32.Parse(reason);
            exoPlayer.onPositionDiscontinuity(value);
        }

        public void PEL_onPlayWhenReadyChanged(string playWhenReadyAndReason)
        {
            string[] values = playWhenReadyAndReason.Split(' ');
            bool value1 = Boolean.Parse(values[0]);
            int value2 = Int32.Parse(values[1]);
            exoPlayer.onPlayWhenReadyChanged(value1, value2);
        }

        public void PEL_onPlayerError(string exoPlaybackException)
        {
            ExoPlayerTypes.ExoPlaybackException exception = ExoPlayerTypes.ExoPlaybackException.fromJson(exoPlaybackException);
            exoPlayer.onPlayerError(exception);
        }

        public void PEL_onPlaybackParametersChanged(string playbackParameters)
        {
            ExoPlayerTypes.PlaybackParameters parameters = ExoPlayerTypes.PlaybackParameters.fromJson(playbackParameters);
            exoPlayer.onPlaybackParametersChanged(parameters);
        }

        public void onMediaItemTransition(string reason)
        {
            int reasonValue = Int32.Parse(reason);
            ExoPlayerTypes.MediaItem currentMediaItem = exoPlayer.getCurrentMediaItem();
            exoPlayer.onMediaItemTransition(currentMediaItem, reasonValue);
        }


        //-----------------------------------------------------------------------
        //  IPlayerAudioComponent methods
        public void ACL_onAudioAttributesChanged(string attribute)
        {
            ExoPlayerTypes.AudioAttributes jsonData = JsonUtility.FromJson<ExoPlayerTypes.AudioAttributes>(attribute);
            exoPlayer.onAudioAttributesChanged(jsonData);
        }

        public void ACL_onAudioSessionId(string audioSessionId)
        {
            int value = Int32.Parse(audioSessionId);
            exoPlayer.onAudioSessionId(value);
        }

        public void ACL_onVolumeChanged(string volume)
        {
            float value = float.Parse(volume);
            exoPlayer.onVolumeChanged(value);
        }

        //-----------------------------------------------------------------------
        //  IPlayerVideoListener methods
        public void VCL_onRenderedFirstFrame(string unused)
        {
            exoPlayer.onRenderedFirstFrame();
        }

        public void VCL_onVideoSizeChanged(string param)
        {
            //  int width, int height, int unappliedRotationDegrees, float pixelWidthHeightRatio
            string[] values = param.Split(' ');
            int value1 = Int32.Parse(values[0]);
            int value2 = Int32.Parse(values[1]);
            int value3 = Int32.Parse(values[2]);
            float value4 = float.Parse(values[3]);
            exoPlayer.onVideoSizeChanged(value1, value2, value3, value4);
        }

        //-----------------------------------------------------------------------
        //  IVideoFrameMetadataListener methods
        public void VML_onVideoFrameAboutToBeRendered(string metadata)
        {
            //  long presentationTimeUs, long releaseTimeNs, ExoPlayerTypes.Format format
            VideoFrameMetadata jsonData = JsonUtility.FromJson<VideoFrameMetadata>(metadata);
            exoPlayer.onVideoFrameAboutToBeRendered(
                jsonData.presentationTimeUs, jsonData.releaseTimeNs, jsonData.getFormat());
        }

        //-----------------------------------------------------------------------
        //  IPlayerTextOutputListener methods
        public void TCL_onCues(string cues)
        {
        }
    }

    public class AndroidExoPlayer : SimpleExoPlayer,
        IPlayerEventListener, IPlayerAudioListener, IPlayerVideoListener, IPlayerVideoFrameMetadataListener, IPlayerTextOutputListener, IPlayerDeviceListener
    {
        //  maximum video texture size
        private const int MaxVideoTextureWidth = 3840;
        private const int MaxVideoTextureHeight = 2160;

        private GameObject gameObject = null;
        private GameObject messageListenerGameObject = null;

        private AndroidJavaObject playerObject = null;
        private AndroidExoPlayerMessageListener messageListener = null;

        private List<IPlayerEventListener> eventListeners = null;
        private List<IPlayerAudioListener> audioListeners = null;
        private List<IPlayerVideoListener> videoListeners = null;
        private List<IPlayerTextOutputListener> textOutputListeners = null;
        private List<IPlayerDeviceListener> deviceListeners = null;
        private IPlayerVideoFrameMetadataListener videoFrameMetadataListener = null;

        private ExoPlayerTypes.SeekParameters currentSeekParameters;
        private ExoPlayerTypes.PlaybackParameters currentPlaybackParameters;
        private ExoPlayerTypes.Format currentAudioFormat = null;
        private ExoPlayerTypes.Format currentVideoFormat = null;

        private int nativeTextureId = 0;
        private AndroidJavaObject currentActivity = null;
        private Vector2 videoTextureScale = new Vector2(1.0f, 1.0f);
        private int videoTextureName = -1;
        private Texture2D videoTargetTexture = null;
        private IntPtr videoTargetTextureId = IntPtr.Zero;
        private int mVideoTextureWidth = 0;
        private int mVideoTextureHeight = 0;

        public AndroidExoPlayer()
        {
            eventListeners = new List<IPlayerEventListener>();
            audioListeners = new List<IPlayerAudioListener>();
            videoListeners = new List<IPlayerVideoListener>();
            textOutputListeners = new List<IPlayerTextOutputListener>();
            deviceListeners = new List<IPlayerDeviceListener>();
        }


        //-----------------------------------------------------------------------
        //  ILifecycleListener methods
        public override void onStart() { playerObject?.Call("onStart"); }
        public override void onUpdate() { playerObject?.Call("onUpdate"); }
        public override void onApplicationPause() { playerObject?.Call("onApplicationPause"); }
        public override void onDestroy() { playerObject?.Call("onDestroy"); }
        public override void onApplicationQuit() { playerObject?.Call("onApplicationQuit"); }

        //-----------------------------------------------------------------------
        //  Unity related methods
        public override Texture initialize(GameObject gameObject)
        {
            this.gameObject = gameObject;

            messageListenerGameObject = new GameObject();
            messageListenerGameObject.name = "EPL_" + Path.GetRandomFileName();

            messageListener = messageListenerGameObject.AddComponent<AndroidExoPlayerMessageListener>();
            messageListener.initialize(this);

            videoTargetTexture = new Texture2D(MaxVideoTextureWidth, MaxVideoTextureHeight, TextureFormat.RGB24, false);
            videoTargetTexture.Apply();
            videoTargetTextureId = videoTargetTexture.GetNativeTexturePtr();

            var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            playerObject = new AndroidJavaObject("com.palantiri.android.plugin.videoplayer.UnityExoPlayer");
            bool result = playerObject.Call<bool>("initialize", currentActivity, (int)videoTargetTextureId,
                messageListenerGameObject.name, (int)SystemInfo.graphicsDeviceType);

            if (eventListeners.Count > 0) playerObject?.Call("enableEventListener", true);
            if (audioListeners.Count > 0) playerObject?.Call("enableAudioListener", true);
            if (videoListeners.Count > 0) playerObject?.Call("enableVideoListener", true);
            if (textOutputListeners.Count > 0) playerObject?.Call("enableTextOutputListener", true);

            return videoTargetTexture;
        }

        public override void finalize()
        {
            playerObject?.Call("finalize");
            playerObject = null;
            UnityEngine.Object.Destroy(messageListenerGameObject);
            messageListenerGameObject = null;
            UnityEngine.Object.Destroy(videoTargetTexture);
            videoTargetTexture = null;
        }

        public override bool updateVideoTexture()
        {
            if (videoTargetTexture == null || playerObject == null) return false;

            playerObject.Call("updateVideoTexture");

            //  in GLES30, change video texture scale if video size has changed
            if (SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3)
            {
                int width = playerObject.Call<int>("getVideoWidth");
                int height = playerObject.Call<int>("getVideoHeight");
                if (mVideoTextureWidth != width || mVideoTextureHeight != height)
                {
                    float xScale = (width < MaxVideoTextureWidth) ? (float)width / (float)MaxVideoTextureWidth : 1.0f;
                    float yScale = (height < MaxVideoTextureHeight) ? (float)height / (float)MaxVideoTextureHeight : 1.0f;
                    videoTextureScale.x = xScale;
                    videoTextureScale.y = yScale;
                    mVideoTextureWidth = width;
                    mVideoTextureHeight = height;
                    return true;
                }
            }

            return false;
        }

        public override Vector2 getVideoTextureScale()
        {
            return videoTextureScale;
        }

        //-----------------------------------------------------------------------
        //  IExoPlayer methods
        // event listeners
        public override void addEventListener(IPlayerEventListener listener) {
            eventListeners.Add(listener);
            if (eventListeners.Count > 0) playerObject?.Call("enableEventListener", true);
        }

        public override void removeEventListener(IPlayerEventListener listener) {
            eventListeners.Remove(listener);
            if (eventListeners.Count == 0) playerObject?.Call("enableEventListener", false);
        }

        // components
        public override IPlayerAudioComponent getAudioComponent() { return this; }
        public override IPlayerVideoComponent getVideoComponent() { return this; }
        public override IPlayerTextComponent getTextComponent() { return this; }
        public override IPlayerDeviceComponent getDeviceComponent() { return this; }

        // MediaItem handling
        public override void addMediaItem(int index, ExoPlayerTypes.MediaItem mediaItem) {
            playerObject?.Call("addMediaItem", index, mediaItem.exoObject);
        }

        public override void addMediaItem(ExoPlayerTypes.MediaItem mediaItem) {
            playerObject?.Call("addMediaItem", mediaItem.exoObject);
        }

        //public override void addMediaItems(int index, List<ExoPlayerTypes.MediaItem> mediaItems) {
        //    //  not supported yet
        //}

        //public override void addMediaItems(List<ExoPlayerTypes.MediaItem> mediaItems) {
        //    //  not supported yet
        //}

        public override void setMediaItem(ExoPlayerTypes.MediaItem mediaItem) {
            playerObject?.Call("setMediaItem", mediaItem.exoObject);
        }

        public override void setMediaItem(ExoPlayerTypes.MediaItem mediaItem, bool resetPosition) {
            playerObject?.Call("setMediaItem", mediaItem.exoObject, resetPosition);
        }

        public override void setMediaItem(ExoPlayerTypes.MediaItem mediaItem, long startPositionMs) {
            playerObject?.Call("setMediaItem", mediaItem.exoObject, startPositionMs);
        }

        //public override void setMediaItems(List<ExoPlayerTypes.MediaItem> mediaItems) {
        //    //  not supported yet
        //}

        //public override void setMediaItems(List<ExoPlayerTypes.MediaItem> mediaItems, bool resetPosition) {
        //    //  not supported yet
        //}

        //public override void setMediaItems(List<ExoPlayerTypes.MediaItem> mediaItems, int startWindowIndex, long startPositionMs) {
        //    //  not supported yet
        //}

        public override void moveMediaItem(int currentIndex, int newIndex) {
            playerObject?.Call("moveMediaItem", currentIndex, newIndex);
        }

        public override void moveMediaItems(int fromIndex, int toIndex, int newIndex) {
            playerObject?.Call("moveMediaItems", fromIndex, toIndex, newIndex);
        }

        public override void removeMediaItem(int index) {
            playerObject?.Call("removeMediaItem", index);
        }

        public override void removeMediaItems(int fromIndex, int toIndex) {
            playerObject?.Call("removeMediaItems", fromIndex, toIndex);
        }

        public override void clearMediaItems() {
            playerObject?.Call("clearMediaItems");
        }

        public override ExoPlayerTypes.MediaItem getCurrentMediaItem() {
            if (playerObject == null) return null;
            ExoPlayerTypes.MediaItem mediaItem = new ExoPlayerTypes.MediaItem();
            AndroidJavaObject javaObject = playerObject.Call<AndroidJavaObject>("getCurrentMediaItem");
            if (javaObject == null) return null;
            mediaItem.loadObject(javaObject);
            return (ExoPlayerTypes.MediaItem)mediaItem;
                
        }

        public override ExoPlayerTypes.MediaItem getMediaItemAt(int index) {
            if (playerObject == null) return null;
            ExoPlayerTypes.MediaItem mediaItem = new ExoPlayerTypes.MediaItem();
            AndroidJavaObject javaObject = playerObject.Call<AndroidJavaObject>("getMediaItemAt", index);
            if (javaObject == null) return null;
            mediaItem.loadObject(javaObject);
            return (ExoPlayerTypes.MediaItem)mediaItem;
        }

        public override int getMediaItemCount() {
            return (playerObject != null) ? playerObject.Call<int>("getMediaItemCount") : 0;
        }

        // playback control
        public override void next() { playerObject?.Call("next"); }
        public override void pause() { playerObject?.Call("pause"); }
        public override void play() { playerObject?.Call("play"); }
        public override void prepare() { playerObject?.Call("prepare"); }
        public override void previous() { playerObject?.Call("previous"); }
        public override void release() { playerObject?.Call("release"); }
        public override void stop() { playerObject?.Call("stop"); }
        public override void stop(bool reset) { playerObject?.Call("stop", reset); }
        public override void seekTo(int windowIndex, long positionMs) { playerObject?.Call("seekTo", windowIndex, positionMs); }
        public override void seekTo(long positionMs) { playerObject?.Call("seekTo", positionMs); }
        public override void seekToDefaultPosition() { playerObject?.Call("seekToDefaultPosition"); }
        public override void seekToDefaultPosition(int windowIndex) { playerObject?.Call("seekToDefaultPosition", windowIndex); }

        // playback status query
        public override bool hasNext() {
            return (playerObject != null) ? playerObject.Call<bool>("hasNext") : false;
        }

        public override bool hasPrevious() {
            return (playerObject != null) ? playerObject.Call<bool>("hasPrevious") : false;
        }

        public override bool isCurrentWindowDynamic() {
            return (playerObject != null) ? playerObject.Call<bool>("isCurrentWindowDynamic") : false;
        }

        public override bool isCurrentWindowLive() {
            return (playerObject != null) ? playerObject.Call<bool>("isCurrentWindowLive") : false;
        }

        public override bool isCurrentWindowSeekable() {
            return (playerObject != null) ? playerObject.Call<bool>("isCurrentWindowSeekable") : false;
        }

        public override bool isLoading() {
            return (playerObject != null) ? playerObject.Call<bool>("isLoading") : false;
        }

        public override bool isPlaying() {
            return (playerObject != null) ? playerObject.Call<bool>("isPlaying") : false;
        }

        public override bool isPlayingAd() {
            return (playerObject != null) ? playerObject.Call<bool>("isPlayingAd") : false;
        }

        public override int getBufferedPercentage() {
            return (playerObject != null) ? playerObject.Call<int>("getBufferedPercentage") : 0;
        }

        public override long getBufferedPosition() {
            return (playerObject != null) ? playerObject.Call<long>("getBufferedPosition") : 0;
        }

        public override long getContentBufferedPosition() {
            return (playerObject != null) ? playerObject.Call<long>("getContentBufferedPosition") : 0;
        }

        public override long getContentDuration() {
            return (playerObject != null) ? playerObject.Call<long>("getContentDuration") : 0;
        }

        public override long getContentPosition() {
            return (playerObject != null) ? playerObject.Call<long>("getContentPosition") : 0;
        }

        public override int getCurrentAdGroupIndex() {
            return (playerObject != null) ? playerObject.Call<int>("getCurrentAdGroupIndex") : 0;
        }

        public override int getCurrentAdIndexInAdGroup() {
            return (playerObject != null) ? playerObject.Call<int>("getCurrentAdIndexInAdGroup") : 0;
        }

        public override long getCurrentLiveOffset() {
            return (playerObject != null) ? playerObject.Call<long>("getCurrentLiveOffset") : 0;
        }

        public override int getCurrentPeriodIndex() {
            return (playerObject != null) ? playerObject.Call<int>("getCurrentPeriodIndex") : 0;
        }

        public override long getCurrentPosition() {
            return (playerObject != null) ? playerObject.Call<long>("getCurrentPosition") : 0;
        }

        public override int getCurrentWindowIndex() {
            return (playerObject != null) ? playerObject.Call<int>("getCurrentWindowIndex") : 0;
        }

        public override long getDuration() {
            return (playerObject != null) ? playerObject.Call<long>("getDuration") : 0;
        }

        public override int getNextWindowIndex() {
            return (playerObject != null) ? playerObject.Call<int>("getNextWindowIndex") : 0;
        }

        public override int getPlaybackState() {
            return (playerObject != null) ? playerObject.Call<int>("getPlaybackState") : 0;
        }

        public override int getPlaybackSuppressionReason() {
            return (playerObject != null) ? playerObject.Call<int>("getPlaybackSuppressionReason") : 0;
        }

        public override int getPreviousWindowIndex() {
            return (playerObject != null) ? playerObject.Call<int>("getPreviousWindowIndex") : 0;
        }

        public override int getRendererCount() {
            return (playerObject != null) ? playerObject.Call<int>("getRendererCount") : 0;
        }

        public override int getRendererType(int index) {
            return (playerObject != null) ? playerObject.Call<int>("getRendererType", index) : 0;
        }

        public override long getTotalBufferedDuration() {
            return (playerObject != null) ? playerObject.Call<long>("getTotalBufferedDuration") : 0;
        }

        // getters/setters
        public override ExoPlayerTypes.PlaybackParameters getPlaybackParameters() {
            return (playerObject != null) ? playerObject.Call<ExoPlayerTypes.PlaybackParameters>("getPlaybackParameters") :
                new ExoPlayerTypes.PlaybackParameters();
        }

        public override void setPlaybackParameters(ExoPlayerTypes.PlaybackParameters playbackParameters) {
            playerObject?.Call("setPlaybackParameters", playbackParameters);
        }

        public override bool getPlayWhenReady() {
            return (playerObject != null) ? playerObject.Call<bool>("getPlayWhenReady") : false;
        }

        public override void setPlayWhenReady(bool playWhenReady) {
            playerObject?.Call("setPlayWhenReady", playWhenReady);
        }

        public override int getRepeatMode() {
            return (playerObject != null) ? playerObject.Call<int>("getRepeatMode") : 0;
        }

        public override void setRepeatMode(int repeatMode) {
            playerObject?.Call("setRepeatMode", repeatMode);
        }

        public override bool getShuffleModeEnabled() {
            return (playerObject != null) ? playerObject.Call<bool>("getShuffleModeEnabled") : false;
        }

        public override void setShuffleModeEnabled(bool shuffleModeEnabled) {
            playerObject?.Call("setShuffleModeEnabled", shuffleModeEnabled);
        }

        //  ExoPlayer methods
        public override bool getPauseAtEndOfMediaItems() {
            return (playerObject != null) ? playerObject.Call<bool>("getPauseAtEndOfMediaItems") : false;
        }

        public override void setPauseAtEndOfMediaItems(bool pauseAtEndOfMediaItems) {
            playerObject?.Call("setPauseAtEndOfMediaItems", pauseAtEndOfMediaItems);
        }

        public override ExoPlayerTypes.SeekParameters getSeekParameters() {
            return (playerObject != null) ? playerObject.Call<ExoPlayerTypes.SeekParameters>("getSeekParameters") :
                new ExoPlayerTypes.SeekParameters();
        }

        public override void setSeekParameters(ExoPlayerTypes.SeekParameters seekParameters) {
            playerObject?.Call("setSeekParameters", seekParameters);
        }

        public override void setForegroundMode(bool foregroundMode) {
            playerObject?.Call("setForegroundMode", foregroundMode);
        }

        //-----------------------------------------------------------------------
        //  IPlayerAudioComponent methods
        public override void addAudioListener(IPlayerAudioListener listener) {
            audioListeners.Add(listener);
            if (audioListeners.Count > 0) playerObject?.Call("enableAudioListener", true);
        }

        public override void removeAudioListener(IPlayerAudioListener listener) {
            audioListeners.Remove(listener);
            if (audioListeners.Count == 0) playerObject?.Call("enableAudioListener", false);
        }

        public override ExoPlayerTypes.AudioAttributes getAudioAttributes() {
            return (playerObject != null) ? playerObject.Call<ExoPlayerTypes.AudioAttributes>("getAudioAttributes") :
                new ExoPlayerTypes.AudioAttributes();
        }

        public override void setAudioAttributes(ExoPlayerTypes.AudioAttributes audioAttributes, bool handleAudioFocus) {
            playerObject?.Call("setAudioAttributes", audioAttributes, handleAudioFocus);
        }

        public override int getAudioSessionId() {
            return (playerObject != null) ? playerObject.Call<int>("getAudioSessionId") : 0;
        }

        public override void setAudioSessionId(int audioSessionId) {
            playerObject?.Call("setAudioSessionId", audioSessionId);
        }

        public override float getVolume() {
            return (playerObject != null) ? playerObject.Call<float>("getVolume") : 0.0f;
        }

        public override void setVolume(float audioVolume) {
            playerObject?.Call("setVolume", audioVolume);
        }

        public override bool getSkipSilenceEnabled() {
            return (playerObject != null) ? playerObject.Call<bool>("getSkipSilenceEnabled") : false;
        }

        public override void setSkipSilenceEnabled(bool skipSilenceEnabled) {
            playerObject?.Call("setSkipSilenceEnabled", skipSilenceEnabled);
        }

        public override void setAuxEffectInfo(ExoPlayerTypes.AuxEffectInfo auxEffectInfo) {
            playerObject?.Call("setAuxEffectInfo", auxEffectInfo);
        }

        public override void clearAuxEffectInfo() {
            playerObject?.Call("clearAuxEffectInfo");
        }

        //-----------------------------------------------------------------------
        //  IPlayerVideoComponent methods
        public override void addVideoListener(IPlayerVideoListener listener) {
            videoListeners.Add(listener);
            if (videoListeners.Count > 0) playerObject?.Call("enableVideoListener", true);
        }

        public override void removeVideoListener(IPlayerVideoListener listener) {
            videoListeners.Remove(listener);
            if (videoListeners.Count == 0) playerObject?.Call("enableVideoListener", false);
        }

        public override void setVideoFrameMetadataListener(IPlayerVideoFrameMetadataListener listener) {
            videoFrameMetadataListener = listener;
            playerObject?.Call("enableVideoFrameMetadataListener", ((listener != null) ? true : false));
        }

        public override void clearVideoFrameMetadataListener(IPlayerVideoFrameMetadataListener listener) {
            videoFrameMetadataListener = null;
            playerObject?.Call("enableVideoFrameMetadataListener", false);
        }

        //-----------------------------------------------------------------------
        //  IPlayerTextComponent methods
        public override void addTextOutputListener(IPlayerTextOutputListener listener) {
            if (listener == null) return;
            textOutputListeners.Add(listener);
            if (textOutputListeners.Count > 0) playerObject?.Call("enableTextOutputListener", true);
        }

        public override void removeTextOutputListener(IPlayerTextOutputListener listener) {
            if (listener == null) return;
            textOutputListeners.Remove(listener);
            if (textOutputListeners.Count == 0) playerObject?.Call("enableTextOutputListener", false);
        }

        public override List<ExoPlayerTypes.Cue> getCurrentCues() {
            return (playerObject != null) ? playerObject.Call<List<ExoPlayerTypes.Cue>>("getCurrentCues") :
                new List<ExoPlayerTypes.Cue>();
        }

        //-----------------------------------------------------------------------
        //  IPlayerDeviceComponent methods
        public override void addDeviceListener(IPlayerDeviceListener listener) {
            if (listener == null) return;
            deviceListeners.Add(listener);
            if (deviceListeners.Count > 0) playerObject?.Call("enableDeviceListener", true);
        }

        public override void removeDeviceListener(IPlayerDeviceListener listener) {
            if (listener == null) return;
            deviceListeners.Remove(listener);
            if (deviceListeners.Count == 0) playerObject?.Call("enableDeviceListener", false);
        }

        public override void setDeviceVolume(int volume) {
            playerObject?.Call("setDeviceVolume", volume);
        }

        public override int getDeviceVolume() {
            return (playerObject != null) ? playerObject.Call<int>("getDeviceVolume") : 0;
        }

        public override void increaseDeviceVolume() {
            playerObject?.Call("increaseDeviceVolume");
        }

        public override void decreaseDeviceVolume() {
            playerObject?.Call("decreaseDeviceVolume");
        }

        public override void setDeviceMuted(bool muted) {
            playerObject?.Call("setDeviceMuted", muted);
        }

        public override bool isDeviceMuted() {
            return (playerObject != null) ? playerObject.Call<bool>("isDeviceMuted") : false;
        }

        public override ExoPlayerTypes.DeviceInfo getDeviceInfo() {
            return (playerObject != null) ? playerObject.Call<ExoPlayerTypes.DeviceInfo>("getDeviceInfo") :
                new ExoPlayerTypes.DeviceInfo();
        }


        //-----------------------------------------------------------------------
        //  IPlayerEventListener methods
        public void onIsLoadingChanged(bool isLoading)
        {
            foreach (IPlayerEventListener listener in eventListeners) listener.onIsLoadingChanged(isLoading);
        }

        public void onIsPlayingChanged(bool isPlaying)
        {
            foreach (IPlayerEventListener listener in eventListeners) listener.onIsPlayingChanged(isPlaying);
        }

        public void onPlaybackStateChanged(int state)
        {
            foreach (IPlayerEventListener listener in eventListeners) listener.onPlaybackStateChanged(state);
        }

        public void onPlaybackSuppressionReasonChanged(int playbackSuppressionReason)
        {
            foreach (IPlayerEventListener listener in eventListeners) listener.onPlaybackSuppressionReasonChanged(playbackSuppressionReason);
        }

        public void onRepeatModeChanged(int repeatMode)
        {
            foreach (IPlayerEventListener listener in eventListeners) listener.onRepeatModeChanged(repeatMode);
        }

        public void onShuffleModeEnabledChanged(bool shuffleModeEnabled)
        {
            foreach (IPlayerEventListener listener in eventListeners) listener.onShuffleModeEnabledChanged(shuffleModeEnabled);
        }

        public void onPositionDiscontinuity(int reason)
        {
            foreach (IPlayerEventListener listener in eventListeners) listener.onPositionDiscontinuity(reason);
        }

        public void onPlayWhenReadyChanged(bool playWhenReady, int reason)
        {
            foreach (IPlayerEventListener listener in eventListeners) listener.onPlayWhenReadyChanged(playWhenReady, reason);
        }

        public void onPlayerError(ExoPlayerTypes.ExoPlaybackException error)
        {
            foreach (IPlayerEventListener listener in eventListeners) listener.onPlayerError(error);
        }

        public void onPlaybackParametersChanged(ExoPlayerTypes.PlaybackParameters playbackParameters)
        {
            foreach (IPlayerEventListener listener in eventListeners) listener.onPlaybackParametersChanged(playbackParameters);
        }

        public void onMediaItemTransition(ExoPlayerTypes.MediaItem mediaItem, int reason)
        {
            foreach (IPlayerEventListener listener in eventListeners) listener.onMediaItemTransition(mediaItem, reason);
        }

        //-----------------------------------------------------------------------
        //  IPlayerAudioListener methods
        public void onAudioAttributesChanged(ExoPlayerTypes.AudioAttributes audioAttributes)
        {
            foreach (IPlayerAudioListener listener in audioListeners) listener.onAudioAttributesChanged(audioAttributes);
        }

        public void onAudioSessionId(int audioSessionId)
        {
            foreach (IPlayerAudioListener listener in audioListeners) listener.onAudioSessionId(audioSessionId);
        }

        public void onVolumeChanged(float volume)
        {
            foreach (IPlayerAudioListener listener in audioListeners) listener.onVolumeChanged(volume);
        }

        //-----------------------------------------------------------------------
        //  IPlayerVideoListener methods
        public void onRenderedFirstFrame()
        {
            foreach (IPlayerVideoListener listener in videoListeners) listener.onRenderedFirstFrame();
        }

        public void onVideoSizeChanged(int width, int height, int unappliedRotationDegrees, float pixelWidthHeightRatio)
        {
            foreach (IPlayerVideoListener listener in videoListeners) listener.onVideoSizeChanged(width, height, unappliedRotationDegrees, pixelWidthHeightRatio);
        }

        //-----------------------------------------------------------------------
        //  IPlayerTextOutputListener methods
        public void onCues(List<ExoPlayerTypes.Cue> cues)
        {
            foreach (IPlayerTextOutputListener listener in textOutputListeners) listener.onCues(cues);
        }

        //-----------------------------------------------------------------------
        //  IVideoFrameMetadataListener methods
        public void onVideoFrameAboutToBeRendered(long presentationTimeUs, long releaseTimeNs, ExoPlayerTypes.Format format)
        {
            videoFrameMetadataListener?.onVideoFrameAboutToBeRendered(presentationTimeUs, releaseTimeNs, format);
        }

        //-----------------------------------------------------------------------
        //  IPlayerDeviceListener methods
        public void onDeviceInfoChanged(ExoPlayerTypes.DeviceInfo deviceInfo)
        {
            foreach (IPlayerDeviceListener listener in deviceListeners) listener.onDeviceInfoChanged(deviceInfo);
        }

        public void onDeviceVolumeChanged(int volume, bool muted)
        {
            foreach (IPlayerDeviceListener listener in deviceListeners) listener.onDeviceVolumeChanged(volume, muted);
        }
    }
}
