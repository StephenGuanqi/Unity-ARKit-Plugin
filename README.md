# Unity-ARKit-Plugin

[![License: GPL v3](https://img.shields.io/badge/License-GPL%20v3-blue.svg)](#license)

Modified plugin source and add custom ARKit projects implemented by Unity

## Introduction
The unity-arkit-plugin enables using all the functionality of the ARKit SDK simply within your Unity projects for iOS. The plugin exposes ARKit SDK's world tracking capabilities, rendering the camera video input, plane detection and update, point cloud extraction, light estimation, and hit testing API to Unity developers for their AR projects. For more information, you can go to the official [repo](https://bitbucket.org/Unity-Technologies/unity-arkit-plugin) for downloading.

This repository has two extensions for the official plugin. First it modifies the existing `UnityARKitScene` project to change it to a zombie walk AR application. The app support hitTest function, changing the zombie's euler angle and pos, as well as scaling the zombie up and down with walking animation. Second the repository has make changes to the plugin's source code in the `NativeInterface` folder, in order to get the captured image `CVPixelBuffer` from the `ARFrame` object, as well as the camera pos, timestamp, lightestimation, etc. and saved those info into the native document directory of the app container asyncronously. And the `UnityARRecorder` app has realize this function.

> Environment Requirements:
> - Xcode 9 that contains ARKit framework
> - iOS 11
> - iOS devices that support A9 or better processor for ARWorldTrackingSessionConfiguration (iPhone6s or higher)
> - Unity v5.6.1p1+ with iOS plugin

## Usage

1. ` $ git clone https://github.com/StephenGuanqi/Unity-ARKit-Plugin.git `
2. Create a new empty project in your Unity, and drag the `unity-arkit-plugin.unitypackage` into your project.
3. You could see multiple unity projects whose names start with `UnithyAR`
4. Try any projects and made modifications you like, and build with the iOS plugin.
5. Open the XCode projects, add the `CoreImage` framework and finish building.


## Custom Projects

### ZombieWalk

![gif](https://raw.githubusercontent.com/StephenGuanqi/AVResources/unity-arkit-plugin/zombie.gif)

The project enables cloudpoint and plane detection, and perform hitTest against existing plane to change the zombie's transform. It also changes the zombie's euler angle to face towards the position of the camera in world coordinates.

### UnityARRecorder

![gif](https://raw.githubusercontent.com/StephenGuanqi/AVResources/unity-arkit-plugin/unityrecording.gif)

The `ARSessionNative.mm` in the plugin source code export various functions to C# layer to let users configure the `ARSession` using C#, and in turn the objective-C++ class take the passed in paramters and conduct processing like parsing the UnityARKitWorldTrackingConfiguration and set it to the ARSession, or transforming left-hand coordinates(Unity) to right-hand coordinaets(ARKit).

The core in `ARSessionNative.mm` is that it provides callback method for `ARFrame` update and `ARAnchor` update. This callback is intended to be implemented by user in C#, and when the `ARFrame` and `ARAnchor` id updated, the corresponding delegate method in objective-c like `(void)session:(ARSession *)session didUpdateFrame:(ARFrame *)frame` and `(void)session:(ARSession *)session didAddAnchors:(NSArray<ARAnchor*>*)anchors` will call the callback function and passed in the `ARFrame` and `ARAnchor` parameter.

However, the official plugin provides many interfaces for the native ARKit API, except for method to get `CVPixelBuffer` in `ARFrame`. The `ARSessionNative.mm` only gives user the opportunity to **manipulate the `ARCamera`, not the `ARFrame`**, therefore it's hard for us to get the captured image and conduct our own research.

This repository has modified the `ARSessionNative.mm` and `UnityARSessionNativeInterface`, meanwhile added `ARFrameHandler` class to get and convert the `CVPixelBuffer` to UIImage, save it to jpg file, and save the camera pos and other info into json file every time the frame is updated.

The sample json file is as follows:

```json
{
  "27255.603090583.jpg" : {
    "lightEstimate" : 944.16241455078125,
    "imageResolution" : {
      "width" : 1280,
      "height" : 720
    },
    "timeStamp" : "27255.603090583",
    "cameraPos" : {
      "x" : -0.0040707914158701897,
      "y" : 0.0039433315396308899,
      "z" : 0.0042630583047866821
    },
    "cameraTransform" : [
      [
        0.83647698163986206,
        -0.21551112830638885,
        0.50384646654129028,
        -0.0040707914158701897
      ],
      [
        0.084991201758384705,
        0.95931828022003174,
        0.26923003792762756,
        0.0039433315396308899
      ],
      [
        -0.54137128591537476,
        -0.18238219618797302,
        0.82076418399810791,
        0.0042630583047866821
      ],
      [
        0,
        0,
        0,
        1
      ]
    ],
    "cameraIntrinsics" : [
      [
        1088.72119140625,
        0,
        639.5
      ],
      [
        0,
        1088.72119140625,
        359.5
      ],
      [
        0,
        0,
        1
      ]
    ],
    "imageName" : "27255.603090583.jpg",
    "cameraEulerAngle" : {
      "x" : -0.27259346842765808,
      "y" : 0.55055916309356689,
      "z" : 0.088364705443382263
    }
  }
}

```
You can find more sample json and image data in the sampleAppContainer.xcappdata.

## Original Official Projects

### ParticlePainter
![gif](https://raw.githubusercontent.com/StephenGuanqi/AVResources/unity-arkit-plugin/particlepainter.gif)
The app paint the gameobject only when the distance between the current camera transform and the previous camera transform is larger than the threshold.

### ARBallz
![gif](https://raw.githubusercontent.com/StephenGuanqi/AVResources/unity-arkit-plugin/ballz.gif)
The app put balls on the coordinates derived by the hitTest result, when a plane has been detected.

## Communication
- If you **found a bug**, open an issue.
- If you **have a feature request**, open an issue.
- If you **want to contribute**, submit a pull request.

## Thanks
[Augmented Reality Tutorial: APPLE ARkit is AMAZING!!!](https://www.youtube.com/watch?v=S7kKQZuOdlk)

## License

This custom Unity-ARKit-Plugin is under GNU GPL v3.0 License. [SEE LICENSE](https://github.com/StephenGuanqi/Unity-ARKit-Plugin/blob/master/License.md)for details.
