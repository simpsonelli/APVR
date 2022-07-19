# Welcome to **ExoPlayer for Unity**

This is **ExoPlayer for Unity** asset for playing video files on **Android platform** using Android **ExoPlayer** package.

------------


# Requirements
- Supported Unity versions: Unity 2019, Unity 2020
- Supported Platform: Android (API level 29+)
- Supported Settings
  - Vulkan is **not** supported
  - Multi-threaded Rendering is **not** supported
  - Playing Unity Streaming Assets is **not** supported

    | Multi-threaded Rendering | Off | On |
    |--:|:--:|:--:|
    | OpenGL ES 2.0 | **O** | X |
    | OpenGL ES 3.0 | **O** | X |
    | Vulkan | X | X |


------------


# Importing Instructions


## Unity 2019.x

1. Create or Open target Unity project

2. Import *ExoPlayer for Unity* asset package

3. Switch to Android platform
  - File --> Build Settings --> Platform --> Select Android and Switch Platform

4. Enable Custom Main Gradle Template
  - Edit --> Project Settings --> Publishing Settings --> check Custom Main Gradle Template
  - Open `Assets/Plugins/Android/mainTemplate.gradle` with any text editor
  - Add below line inside `dependencies {  }`
```javascript
implementation 'com.google.android.exoplayer:exoplayer:2.12.2'
```

5. Set Graphics API
  - Edit --> Project Settings --> Other Settings --> Graphics API
  - Remove Vulkan
  - Add OpenGL ES 2.0 or OpenGL ES 3.0

6. Disable Multithreaded Rendering option
  - Edit --> Project Settings --> Other Settings --> uncheck Multithreaded Rendering

7. Set Minimum API Level to API Level 29
  - Edit --> Project Settings --> Other Settings --> Minimum API Level
  - set to API Level 29

8. Enable Internet access to test sample scenes
  - Edit --> Project Settings --> Other Settings --> Internet Access
  - set to Require


## Unity 2020.x

1. Follow import instruction for Unity 2019.x
2. Enable Custom Base Gradle Template
  - Edit --> Project Settings --> Publishing Settings --> check Custom Base Gradle Template
  - Open `Assets/Plugins/Android/baseProjectTemplate.gradle` with any text editor, and change version of gradle plugin to 3.4.0
```javascript
classpath 'com.android.tools.build:gradle:3.4.0'
```



------------


# Demo Scenes

## SampleVideoPlayer
- Shows simple video player feature
- Run, click "Initialize", and click "Load Video"

## SampleVideoCubes
- Shows how to play a video on a texture object
- Just Run

## Sample360Video
- Shows how to play a 360 video
- Just Run, and drag left/right to rotate horizontally


------------


# Contact

- [Homepage](https://jounggj.github.io/product/exoplayer/overview/)
- [jounggj@gmail.com](mailto:jounggj@gmail.com)

