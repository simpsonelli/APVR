namespace com.palantiri.unity.videoplayer
{
    public interface IPlayerVideoFrameMetadataListener
    {
        //    void onVideoFrameAboutToBeRendered(long presentationTimeUs, long releaseTimeNs, Format format, @Nullable MediaFormat mediaFormat);
        void onVideoFrameAboutToBeRendered(long presentationTimeUs, long releaseTimeNs, ExoPlayerTypes.Format format);
    }
}
