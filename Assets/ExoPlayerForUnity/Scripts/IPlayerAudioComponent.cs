namespace com.palantiri.unity.videoplayer
{
    public interface IPlayerAudioComponent
    {
        void addAudioListener(IPlayerAudioListener listener);
        void removeAudioListener(IPlayerAudioListener listener);
        ExoPlayerTypes.AudioAttributes getAudioAttributes();
        void setAudioAttributes(ExoPlayerTypes.AudioAttributes audioAttributes, bool handleAudioFocus);
        int getAudioSessionId();
        void setAudioSessionId(int audioSessionId);
        float getVolume();
        void setVolume(float audioVolume);
        bool getSkipSilenceEnabled();
        void setSkipSilenceEnabled(bool skipSilenceEnabled);
        void setAuxEffectInfo(ExoPlayerTypes.AuxEffectInfo auxEffectInfo);
        void clearAuxEffectInfo();
    }
}
