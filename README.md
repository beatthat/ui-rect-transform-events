# ui-rect-transform-events

Use when you have a Unity3D UI component that has `RectTransform` children (or siblings) and needs to get an event callback when their dimensions change.

## Usage

```c#
using BeatThat.FrameTime;

var frame = FrameTimeUtils.TimeToFrame(1.5, 30); // 45
var time = FrameTimeUtils.FrameToTime(frame, 30); // 1.5

// can also use them as ext functions on float, e.g.
var t = frame.FrameToTime(30); // 1.5
```

## Install

From your unity project folder:

    npm init
    npm install --save beatthat/frame-time

The package and all its dependencies will be installed under Assets/Plugins/packages.
