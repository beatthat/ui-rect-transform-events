# ui-rect-transform-events

Use when you have a Unity3D UI component that has `RectTransform` children (or siblings) and needs to get an event callback when their dimensions change.

## Usage

```c#
using BeatThat.UIRectTransformEvents;

public class MyManagerComponent
{
    public RectTransform m_managedChild;

    void Start()
    {
        m_managedChild.AddComponent<RectTransformEvents>.onScreenRectChanged.AddListener(this.OnManagedChildScreenRectChanged);
    }

    private void OnManagedChildScreenRectChanged()
    {
        // do something
    }
}
```

## Install

From your unity project folder:

    npm init
    npm install --save beatthat/ui-rect-transform-events

The package and all its dependencies will be installed under Assets/Plugins/packages.
