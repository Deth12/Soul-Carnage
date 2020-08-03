using System;
using UnityEngine;
using UnityEditor;

public class ScreenshotTaker : Editor
{
    [MenuItem("Tools/Take screenshot")]
    static void Screenshot()
    {
        DateTime d = DateTime.Now;
        string name = "screenshot_"
                      + d.Day + "_" + d.Month + "_" + d.Year + "_"
                      + d.Hour.ToString() + d.Minute.ToString() + d.Second.ToString()
                      + ".png";
        ScreenCapture.CaptureScreenshot(name);
        Debug.Log("Screenshot captured: " + name);
    }
}
