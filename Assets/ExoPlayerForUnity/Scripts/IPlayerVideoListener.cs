namespace com.palantiri.unity.videoplayer
{
    public interface IPlayerVideoListener
    {
        void onRenderedFirstFrame();
        //void onSurfaceSizeChanged(int width, int height);     //  not used in Unity environment
        void onVideoSizeChanged(int width, int height, int unappliedRotationDegrees, float pixelWidthHeightRatio);
    }
}
