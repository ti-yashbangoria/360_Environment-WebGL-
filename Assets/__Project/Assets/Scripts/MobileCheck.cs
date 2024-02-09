using UnityEngine;
using System.Runtime.InteropServices;

public class MobileCheck : MonoBehaviour
{
    public RunningOS runningOS;

    [DllImport("__Internal")]
    private static extern string GetOS();

    public static string GetDeviceOS()
    {
        return Application.platform == RuntimePlatform.WebGLPlayer ? GetOS() : SystemInfo.operatingSystem;
    }

    public void ChangeRunningOSState(RunningOS _runningOS)
    {
        runningOS = _runningOS;
        switch (_runningOS)
        {
            case RunningOS.MobileDevice:
                break;

            case RunningOS.NotMobileDevice:
                break;
        }
    }
}

public enum RunningOS
{
    MobileDevice,
    NotMobileDevice
}
