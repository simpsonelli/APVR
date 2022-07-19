namespace com.palantiri.unity.videoplayer
{
    public interface ILifecycleListener
    {
        void onStart();
        void onUpdate();
        void onApplicationPause();
        void onDestroy();
        void onApplicationQuit();
    }
}
