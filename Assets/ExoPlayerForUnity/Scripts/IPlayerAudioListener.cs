namespace com.palantiri.unity.videoplayer
{
    public interface IPlayerAudioListener
    {
        void onAudioAttributesChanged(ExoPlayerTypes.AudioAttributes audioAttributes);
        void onAudioSessionId(int audioSessionId);
        void onVolumeChanged(float volume);
    }
}
