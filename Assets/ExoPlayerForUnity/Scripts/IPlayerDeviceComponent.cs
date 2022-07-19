namespace com.palantiri.unity.videoplayer
{
    public interface IPlayerDeviceComponent
    {
        void addDeviceListener(IPlayerDeviceListener listener);
        void removeDeviceListener(IPlayerDeviceListener listener);
        void setDeviceVolume(int volume);
        int getDeviceVolume();
        void increaseDeviceVolume();
        void decreaseDeviceVolume();
        void setDeviceMuted(bool muted);
        bool isDeviceMuted();
        ExoPlayerTypes.DeviceInfo getDeviceInfo();
    }
}
