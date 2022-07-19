using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.palantiri.unity.videoplayer
{
    public class VideoTextureController : MonoBehaviour
    {
        private Material videoMaterial;
        private SimpleExoPlayer exoPlayer = null;
        private bool initialized = false;
        private Texture2D videoTexture = null;

        private void Awake()
        {
#if UNITY_EDITOR
            exoPlayer = new WindowsExoPlayer();
#elif UNITY_ANDROID
            exoPlayer = new AndroidExoPlayer();
#endif
        }

        // Start is called before the first frame update
        void Start()
        {
            exoPlayer?.onStart();
        }

        // Update is called once per frame
        void Update()
        {
            if (exoPlayer == null) return;
            if (exoPlayer.updateVideoTexture() && (videoMaterial != null))
            {
                videoMaterial.mainTextureScale = exoPlayer.getVideoTextureScale();
            }
            exoPlayer.onUpdate();
        }

        private void OnApplicationPause(bool pause)
        {
            exoPlayer?.onApplicationPause();
        }

        private void OnApplicationQuit()
        {
            exoPlayer?.onApplicationQuit();
        }

        private void OnDestroy()
        {
            exoPlayer?.onDestroy();
        }

        public void initialize(Material material)
        {
            videoMaterial = material;
#if UNITY_EDITOR
            RenderTexture videoRenderTexture = (RenderTexture)exoPlayer?.initialize(gameObject);
            videoMaterial.mainTexture = videoRenderTexture;
#elif UNITY_ANDROID
            videoTexture = (Texture2D)exoPlayer?.initialize(gameObject);
            videoTexture.Apply();
            videoMaterial.mainTexture = videoTexture;
#endif
        }

        //-----------------------------------------------------------------------
        //  Unity related methods
        public SimpleExoPlayer getPlayer()
        {
            return exoPlayer;
        }

        public Texture getVideoTexture()
        {
            return videoTexture;
        }
    }
}
