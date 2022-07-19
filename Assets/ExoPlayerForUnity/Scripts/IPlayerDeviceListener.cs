namespace com.palantiri.unity.videoplayer
{
    public interface IPlayerDeviceListener
    {
        void onDeviceInfoChanged(ExoPlayerTypes.DeviceInfo deviceInfo);
        void onDeviceVolumeChanged(int volume, bool muted);
    }
}
