using System.Collections.Generic;

namespace com.palantiri.unity.videoplayer
{
    public interface IPlayerTextComponent
    {
        void addTextOutputListener(IPlayerTextOutputListener listener);
        void removeTextOutputListener(IPlayerTextOutputListener listener);
        List<ExoPlayerTypes.Cue> getCurrentCues();
    }
}
