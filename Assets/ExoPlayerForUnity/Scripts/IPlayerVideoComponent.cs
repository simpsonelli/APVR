namespace com.palantiri.unity.videoplayer
{
    public interface IPlayerVideoComponent
    {
        void addVideoListener(IPlayerVideoListener listener);
        void removeVideoListener(IPlayerVideoListener listener);
        void setVideoFrameMetadataListener(IPlayerVideoFrameMetadataListener listener);
        void clearVideoFrameMetadataListener(IPlayerVideoFrameMetadataListener listener);
    }
}
