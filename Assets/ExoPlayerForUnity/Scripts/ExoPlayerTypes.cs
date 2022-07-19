using System;
using UnityEngine;

public class ExoPlayerTypes
{
    public enum LayoutAlignment
    {
        ALIGN_CENTER,
        ALIGN_NORMAL,
        ALIGN_OPPOSITE,
    };

    [Serializable]
    public struct AuxEffectInfo
    {
        public static readonly int NO_AUX_EFFECT_ID = 0;

        public int effectId;
        public float sendLevel;
    }

    [Serializable]
    public struct SeekParameters
    {
        public long toleranceAfterUs;
        public long toleranceBeforeUs;
    }

    [Serializable]
    public struct PlaybackParameters
    {
        public float pitch;
        public float speed;

        public static PlaybackParameters fromJson(string json)
        {
            return JsonUtility.FromJson<PlaybackParameters>(json);
        }
    }

    [Serializable]
    public struct AudioAttributes
    {
        public int allowedCapturePolicy;
        public int contentType;
        public int flags;
        public int usage;
    }

    [Serializable]
    public struct Cue
    {
        //  constants
        public static int   ANCHOR_TYPE_START;
        public static int   ANCHOR_TYPE_MIDDLE;
        public static int   ANCHOR_TYPE_END;
        public static float DIMEN_UNSET;
        public static int   LINE_TYPE_FRACTION;
        public static int   LINE_TYPE_NUMBER;
        public static int   TEXT_SIZE_TYPE_ABSOLUTE;
        public static int   TEXT_SIZE_TYPE_FRACTIONAL;
        public static int   TEXT_SIZE_TYPE_FRACTIONAL_IGNORE_PADDING;
        public static int   TYPE_UNSET;
        public static int   VERTICAL_TYPE_LR;
        public static int   VERTICAL_TYPE_RL;
        public static Cue   EMPTY;

        public float line;
        public int lineAnchor;
        public int lineType;
        public float position;
        public int positionAnchor;
        public float size;
        public string text;
        public LayoutAlignment textAlignment;
        public float textSize;
        public int textSizeType;
        public int verticalType;
        public int windowColor;
        public bool windowColorSet;

        //  not supported yet
        // public Bitmap bitmap;
        // public float bitmapHeight;
    }

    [Serializable]
    public struct DeviceInfo
    {
        public static readonly int PLAYBACK_TYPE_LOCAL = 0;
        public static readonly int PLAYBACK_TYPE_REMOTE = 1;

        public int minVolume;
        public int maxVolume;
        public int playbackType;
    }

    public class Format
    {
        //  common fields
        public string id;
        public string label;
        public string language;
        public int selectionFlags;
        public int roleFlags;
        //    public int averageBitrate;
        //    public int peakBitrate;
        public int bitrate;         //  peakBitrate != NO_VALUE ? peakBitrate : averageBitrate
        public string codecs;
        //    public Metadata metadata;

        //  container related fields
        public string containerMimeType;

        //  sample format related fields
        public string sampleMimeType;
        public int maxInputSize;
        public long subsampleOffsetUs;
        //    public List<byte[]> initializationData;
        //    public DrmInitData drmInitData;
        //    public ExoMediaCrypto exoMediaCryptoType;

        //  video related fields
        public int width;
        public int height;
        public float frameRate;
        public int rotationDegrees;
        public float pixelWidthHeightRatio;
        //    public byte[] projectionData;
        public int stereoMode;
        //    public ColorInfo colorInfo;

        //  audio related fields
        public int channelCount;
        public int sampleRate;
        public int pcmEncoding;
        public int encoderDelay;
        public int encoderPadding;

        //  text related fields
        public int accessibilityChannel;

        public Format()
        {
            id = null;
            label = null;
            language = null;
            selectionFlags = 0;
            roleFlags = 0;
            bitrate = 0;
            codecs = null;

            containerMimeType = null;

            sampleMimeType = null;
            maxInputSize = 0;
            subsampleOffsetUs = 0;

            width = 0;
            height = 0;
            frameRate = 0.0f;
            rotationDegrees = 0;
            pixelWidthHeightRatio = 1.34f;
            stereoMode = 0;

            channelCount = 0;
            sampleRate = 0;
            pcmEncoding = 0;
            encoderDelay = 0;
            encoderPadding = 0;

            accessibilityChannel = 0;
        }
    }

    [Serializable]
    public class MediaItem
    {
        internal AndroidJavaObject exoObject;     //  holds com.google.android.exoplayer2.MediaItem instance

        [Serializable]
        public struct ClippingProperties {
            public long startPositionMs;
            public long endPositionMs;
            public bool relativeToLiveWindow;
            public bool relativeToDefaultPosition;
            public bool startsAtKeyFrame;
        }

        [Serializable]
        public struct PlaybackProperties {
            public Uri adTagUri;
            public String customCacheKey;
            public String mimeType;
            public Uri uri;
            // MediaItem.DrmConfiguration drmConfiguration;
            // List<StreamKey> streamKeys;
            // List<MediaItem.Subtitle> subtitles;
            // Object tag;
        }

        public ClippingProperties clippingProperties;
        public PlaybackProperties playbackProperties;
        public String mediaId;
        public MediaMetadata mediaMetadata;

        private static Builder sBuilder = null;

        internal static Builder getBuilder()
        {
            if (sBuilder == null) sBuilder = new Builder();
            return sBuilder;
        }

        public static MediaItem fromUri(Uri mediaUri)
        {
            return fromUri(mediaUri.ToString());
        }

        public static MediaItem fromUri(string mediaUri)
        {
            MediaItem mediaItem = new MediaItem();
#if UNITY_EDITOR
            mediaItem.mediaId = mediaUri;
#elif UNITY_ANDROID
            mediaItem.exoObject = getBuilder().MediaItem_fromUri(mediaUri);
            mediaItem.loadObject(null);
#endif
            return mediaItem;
        }

        public MediaItem()
        {
            exoObject = null;
            mediaId = null;
        }

        internal void loadObject(AndroidJavaObject javaObject)
        {
            if (javaObject != null) exoObject = javaObject;
            string json;
            json = getBuilder().MediaItem_getClippingPropertiesAsJson(exoObject);
            clippingProperties = JsonUtility.FromJson<ExoPlayerTypes.MediaItem.ClippingProperties>(json);
            json = getBuilder().MediaItem_getPlaybackPropertiesAsJson(exoObject);
            playbackProperties = JsonUtility.FromJson<ExoPlayerTypes.MediaItem.PlaybackProperties>(json);
            json = getBuilder().MediaItem_getMediaMetadataAsJson(exoObject);
            mediaMetadata = JsonUtility.FromJson<ExoPlayerTypes.MediaMetadata>(json);
            mediaId = getBuilder().MediaItem_getMediaId(exoObject);
        }
    }

    [Serializable]
    public struct MediaMetadata
    {
        public String title;
    }

    [Serializable]
    public struct ExoPlaybackException
    {
        public enum TimeoutOperation : Int32
        {
            UNDEFINED = 0,
            RELEASE = 1,
            SET_FOREGROUND_MODE = 2,
        };

        public enum Type : Int32
        {
            SOURCE = 0,
            RENDERER = 1,
            UNEXPECTED = 2,
            REMOTE = 3,
            OUT_OF_MEMORY = 4,
            TIMEOUT = 5,
        };

        public int type;
        public int rendererFormatSupport;
        public int rendererIndex;
        public string rendererName;
        public int timeoutOperation;
        public long timestampMs;
        //public Format rendererFormat;
        //public MediaSource.MediaPeriodId mediaPeriodId;

        public static ExoPlaybackException fromJson(string json)
        {
            return JsonUtility.FromJson<ExoPlaybackException>(json);
        }
    };

    public class Builder
    {
        private AndroidJavaClass classExoTypeBuilder = null;

        public Builder()
        {
            classExoTypeBuilder = new AndroidJavaClass("com.palantiri.android.plugin.videoplayer.ExoTypeBuilder");
        }

        public AndroidJavaObject MediaItem_fromUri(string uri)
        {
            return classExoTypeBuilder.CallStatic<AndroidJavaObject>("MediaItem_fromUri", uri);
        }

        public AndroidJavaObject MediaItem_fromUri(Uri uri)
        {
            return classExoTypeBuilder.CallStatic<AndroidJavaObject>("MediaItem_fromUri", uri.ToString());
        }

        public string MediaItem_getMediaId(AndroidJavaObject javaMediaItem)
        {
            return classExoTypeBuilder.CallStatic<string>("MediaItem_getMediaId", javaMediaItem);
        }

        public string MediaItem_getClippingPropertiesAsJson(AndroidJavaObject javaMediaItem)
        {
            return classExoTypeBuilder.CallStatic<string>("MediaItem_getClippingPropertiesAsJson", javaMediaItem);
        }

        public string MediaItem_getPlaybackPropertiesAsJson(AndroidJavaObject javaMediaItem)
        {
            return classExoTypeBuilder.CallStatic<string>("MediaItem_getPlaybackPropertiesAsJson", javaMediaItem);
        }

        public string MediaItem_getMediaMetadataAsJson(AndroidJavaObject javaMediaItem)
        {
            return classExoTypeBuilder.CallStatic<string>("MediaItem_getMediaMetadataAsJson", javaMediaItem);
        }
    }

    private static AndroidJavaObject sExoTypeBuilder = null;
    private static Builder sBuilder = null;

    public static ExoPlayerTypes.Builder getBuilder()
    {
        if (sBuilder != null) sBuilder = new Builder();
        return sBuilder;
    }
}
