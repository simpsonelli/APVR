using System.Collections.Generic;

namespace com.palantiri.unity.videoplayer
{
    public interface IPlayerTextOutputListener
    {
        void onCues(List<ExoPlayerTypes.Cue> cues);
    }
}
