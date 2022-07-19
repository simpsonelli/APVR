using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.palantiri.unity.videoplayer
{
    public abstract class SimpleExoPlayer
        : ILifecycleListener, IExoPlayer, IPlayerAudioComponent, IPlayerVideoComponent, IPlayerTextComponent, IPlayerDeviceComponent
    {
        //-----------------------------------------------------------------------
        //  ILifecycleListener methods
        public abstract void onStart();
        public abstract void onUpdate();
        public abstract void onApplicationPause();
        public abstract void onDestroy();
        public abstract void onApplicationQuit();

        //-----------------------------------------------------------------------
        //  Unity related methods
        public abstract Texture initialize(GameObject gameObject);
        public abstract void finalize();
        public abstract bool updateVideoTexture();
        public abstract Vector2 getVideoTextureScale();

        //-----------------------------------------------------------------------
        //  IExoPlayer methods
        // event listeners
        public abstract void addEventListener(IPlayerEventListener listener);
        public abstract void removeEventListener(IPlayerEventListener listener);

        // components
        public abstract IPlayerAudioComponent getAudioComponent();
        public abstract IPlayerVideoComponent getVideoComponent();
        public abstract IPlayerTextComponent getTextComponent();
        public abstract IPlayerDeviceComponent getDeviceComponent();

        // MediaItem handling
        public abstract void addMediaItem(int index, ExoPlayerTypes.MediaItem mediaItem);
        public abstract void addMediaItem(ExoPlayerTypes.MediaItem mediaItem);
        //public abstract void addMediaItems(int index, List<ExoPlayerTypes.MediaItem> mediaItems);
        //public abstract void addMediaItems(List<ExoPlayerTypes.MediaItem> mediaItems);
        public abstract void setMediaItem(ExoPlayerTypes.MediaItem mediaItem);
        public abstract void setMediaItem(ExoPlayerTypes.MediaItem mediaItem, bool resetPosition);
        public abstract void setMediaItem(ExoPlayerTypes.MediaItem mediaItem, long startPositionMs);
        //public abstract void setMediaItems(List<ExoPlayerTypes.MediaItem> mediaItems);
        //public abstract void setMediaItems(List<ExoPlayerTypes.MediaItem> mediaItems, bool resetPosition);
        //public abstract void setMediaItems(List<ExoPlayerTypes.MediaItem> mediaItems, int startWindowIndex, long startPositionMs);
        public abstract void moveMediaItem(int currentIndex, int newIndex);
        public abstract void moveMediaItems(int fromIndex, int toIndex, int newIndex);
        public abstract void removeMediaItem(int index);
        public abstract void removeMediaItems(int fromIndex, int toIndex);
        public abstract void clearMediaItems();
        public abstract ExoPlayerTypes.MediaItem getCurrentMediaItem();
        public abstract ExoPlayerTypes.MediaItem getMediaItemAt(int index);
        public abstract int getMediaItemCount();

        // playback control
        public abstract void next();
        public abstract void pause();
        public abstract void play();
        public abstract void prepare();
        public abstract void previous();
        public abstract void release();
        public abstract void stop();
        public abstract void stop(bool reset);
        public abstract void seekTo(int windowIndex, long positionMs);
        public abstract void seekTo(long positionMs);
        public abstract void seekToDefaultPosition();
        public abstract void seekToDefaultPosition(int windowIndex);

        // playback status query
        public abstract bool hasNext();
        public abstract bool hasPrevious();
        public abstract bool isCurrentWindowDynamic();
        public abstract bool isCurrentWindowLive();
        public abstract bool isCurrentWindowSeekable();
        public abstract bool isLoading();
        public abstract bool isPlaying();
        public abstract bool isPlayingAd();
        public abstract int getBufferedPercentage();
        public abstract long getBufferedPosition();
        public abstract long getContentBufferedPosition();
        public abstract long getContentDuration();
        public abstract long getContentPosition();
        public abstract int getCurrentAdGroupIndex();
        public abstract int getCurrentAdIndexInAdGroup();
        public abstract long getCurrentLiveOffset();
        public abstract int getCurrentPeriodIndex();
        public abstract long getCurrentPosition();
        public abstract int getCurrentWindowIndex();
        public abstract long getDuration();
        public abstract int getNextWindowIndex();
        public abstract int getPlaybackState();
        public abstract int getPlaybackSuppressionReason();
        public abstract int getPreviousWindowIndex();
        public abstract int getRendererCount();
        public abstract int getRendererType(int index);
        public abstract long getTotalBufferedDuration();

        // getters/setters
        public abstract ExoPlayerTypes.PlaybackParameters getPlaybackParameters();
        public abstract void setPlaybackParameters(ExoPlayerTypes.PlaybackParameters playbackParameters);
        public abstract bool getPlayWhenReady();
        public abstract void setPlayWhenReady(bool playWhenReady);
        public abstract int getRepeatMode();
        public abstract void setRepeatMode(int repeatMode);
        public abstract bool getShuffleModeEnabled();
        public abstract void setShuffleModeEnabled(bool shuffleModeEnabled);

        //  ExoPlayer methods
        public abstract bool getPauseAtEndOfMediaItems();
        public abstract void setPauseAtEndOfMediaItems(bool pauseAtEndOfMediaItems);
        public abstract ExoPlayerTypes.SeekParameters getSeekParameters();
        public abstract void setSeekParameters(ExoPlayerTypes.SeekParameters seekParameters);
        public abstract void setForegroundMode(bool foregroundMode);

        //-----------------------------------------------------------------------
        //  IPlayerAudioComponent methods
        public abstract void addAudioListener(IPlayerAudioListener listener);
        public abstract void removeAudioListener(IPlayerAudioListener listener);
        public abstract ExoPlayerTypes.AudioAttributes getAudioAttributes();
        public abstract void setAudioAttributes(ExoPlayerTypes.AudioAttributes audioAttributes, bool handleAudioFocus);
        public abstract int getAudioSessionId();
        public abstract void setAudioSessionId(int audioSessionId);
        public abstract float getVolume();
        public abstract void setVolume(float audioVolume);
        public abstract bool getSkipSilenceEnabled();
        public abstract void setSkipSilenceEnabled(bool skipSilenceEnabled);
        public abstract void setAuxEffectInfo(ExoPlayerTypes.AuxEffectInfo auxEffectInfo);
        public abstract void clearAuxEffectInfo();

        //-----------------------------------------------------------------------
        //  IPlayerVideoComponent methods
        public abstract void addVideoListener(IPlayerVideoListener listener);
        public abstract void removeVideoListener(IPlayerVideoListener listener);
        public abstract void setVideoFrameMetadataListener(IPlayerVideoFrameMetadataListener listener);
        public abstract void clearVideoFrameMetadataListener(IPlayerVideoFrameMetadataListener listener);

        //-----------------------------------------------------------------------
        //  IPlayerTextComponent methods
        public abstract void addTextOutputListener(IPlayerTextOutputListener listener);
        public abstract void removeTextOutputListener(IPlayerTextOutputListener listener);
        public abstract List<ExoPlayerTypes.Cue> getCurrentCues();

        //-----------------------------------------------------------------------
        //  IPlayerDeviceComponent methods
        public abstract void addDeviceListener(IPlayerDeviceListener listener);
        public abstract void removeDeviceListener(IPlayerDeviceListener listener);
        public abstract void setDeviceVolume(int volume);
        public abstract int getDeviceVolume();
        public abstract void increaseDeviceVolume();
        public abstract void decreaseDeviceVolume();
        public abstract void setDeviceMuted(bool muted);
        public abstract bool isDeviceMuted();
        public abstract ExoPlayerTypes.DeviceInfo getDeviceInfo();
    }
}
