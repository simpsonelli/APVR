using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.palantiri.unity.videoplayer
{
    public interface IExoPlayer
    {
        // event listeners
        void addEventListener(IPlayerEventListener listener);
        void removeEventListener(IPlayerEventListener listener);

        // components
        IPlayerAudioComponent getAudioComponent();
        IPlayerVideoComponent getVideoComponent();
        IPlayerTextComponent getTextComponent();
        IPlayerDeviceComponent getDeviceComponent();

        // MediaItem handling
        void addMediaItem(int index, ExoPlayerTypes.MediaItem mediaItem);
        void addMediaItem(ExoPlayerTypes.MediaItem mediaItem);
        //void addMediaItems(int index, List<ExoPlayerTypes.MediaItem> mediaItems);
        //void addMediaItems(List<ExoPlayerTypes.MediaItem> mediaItems);
        void setMediaItem(ExoPlayerTypes.MediaItem mediaItem);
        void setMediaItem(ExoPlayerTypes.MediaItem mediaItem, bool resetPosition);
        void setMediaItem(ExoPlayerTypes.MediaItem mediaItem, long startPositionMs);
        //void setMediaItems(List<ExoPlayerTypes.MediaItem> mediaItems);
        //void setMediaItems(List<ExoPlayerTypes.MediaItem> mediaItems, bool resetPosition);
        //void setMediaItems(List<ExoPlayerTypes.MediaItem> mediaItems, int startWindowIndex, long startPositionMs);
        void moveMediaItem(int currentIndex, int newIndex);
        void moveMediaItems(int fromIndex, int toIndex, int newIndex);
        void removeMediaItem(int index);
        void removeMediaItems(int fromIndex, int toIndex);
        void clearMediaItems();
        ExoPlayerTypes.MediaItem getCurrentMediaItem();
        ExoPlayerTypes.MediaItem getMediaItemAt(int index);
        int getMediaItemCount();

        // playback control
        void next();
        void pause();
        void play();
        void prepare();
        void previous();
        void release();
        void stop();
        void stop(bool reset);
        void seekTo(int windowIndex, long positionMs);
        void seekTo(long positionMs);
        void seekToDefaultPosition();
        void seekToDefaultPosition(int windowIndex);

        // playback status query
        bool hasNext();
        bool hasPrevious();
        bool isCurrentWindowDynamic();
        bool isCurrentWindowLive();
        bool isCurrentWindowSeekable();
        bool isLoading();
        bool isPlaying();
        bool isPlayingAd();
        int getBufferedPercentage();
        long getBufferedPosition();
        long getContentBufferedPosition();
        long getContentDuration();
        long getContentPosition();
        int getCurrentAdGroupIndex();
        int getCurrentAdIndexInAdGroup();
        long getCurrentLiveOffset();
        int getCurrentPeriodIndex();
        long getCurrentPosition();
        int getCurrentWindowIndex();
        long getDuration();
        int getNextWindowIndex();
        int getPlaybackState();
        int getPlaybackSuppressionReason();
        int getPreviousWindowIndex();
        int getRendererCount();
        int getRendererType(int index);
        long getTotalBufferedDuration();

        // getters/setters
        ExoPlayerTypes.PlaybackParameters getPlaybackParameters();
        void setPlaybackParameters(ExoPlayerTypes.PlaybackParameters playbackParameters);
        bool getPlayWhenReady();
        void setPlayWhenReady(bool playWhenReady);
        int getRepeatMode();
        void setRepeatMode(int repeatMode);
        bool getShuffleModeEnabled();
        void setShuffleModeEnabled(bool shuffleModeEnabled);

        //  ExoPlayer methods
        bool getPauseAtEndOfMediaItems();
        void setPauseAtEndOfMediaItems(bool pauseAtEndOfMediaItems);
        ExoPlayerTypes.SeekParameters getSeekParameters();
        void setSeekParameters(ExoPlayerTypes.SeekParameters seekParameters);
        void setForegroundMode(bool foregroundMode);
    }
}
