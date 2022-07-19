namespace com.palantiri.unity.videoplayer
{
    public interface IPlayerEventListener
    {
        void onIsLoadingChanged(bool isLoading);
        void onIsPlayingChanged(bool isPlaying);
        void onPlaybackStateChanged(int state);
        void onPlaybackSuppressionReasonChanged(int playbackSuppressionReason);
        void onRepeatModeChanged(int repeatMode);
        void onShuffleModeEnabledChanged(bool shuffleModeEnabled);
        void onPositionDiscontinuity(int reason);
        void onPlayWhenReadyChanged(bool playWhenReady, int reason);
        void onPlayerError(ExoPlayerTypes.ExoPlaybackException error);
        void onPlaybackParametersChanged(ExoPlayerTypes.PlaybackParameters playbackParameters);
        void onMediaItemTransition(ExoPlayerTypes.MediaItem mediaItem, int reason);

        //    void onTimelineChanged(Timeline timeline, int reason);
        //    void onTimelineChanged(Timeline timeline, Object manifest, int reason);
        //    void onTracksChanged(TrackGroupArray trackGroups, TrackSelectionArray trackSelections);
        //    void onExperimentalOffloadSchedulingEnabledChanged(boolean offloadSchedulingEnabled);
    }
}
