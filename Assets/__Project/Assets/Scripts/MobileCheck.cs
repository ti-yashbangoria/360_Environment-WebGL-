using UnityEngine;
using System.Runtime.InteropServices;

public class MobileCheck : MonoBehaviour
{
    #region Variables

    public RunningOS runningOS;

    #endregion


    #region Main Methods

/*    private void Start()
    {
        Debug.Log("OS:" + runningOS);
    }*/

    #endregion


    #region Helper Methods


    [DllImport("__Internal")]
    private static extern string GetOS();

    public static string GetDeviceOS()
    {
        return Application.platform == RuntimePlatform.WebGLPlayer ? GetOS() : SystemInfo.operatingSystem;
    }

    //public void ChangeRunningOSState(RunningOS _runningOS)
    //{
    //    runningOS = _runningOS;
    //    Debug.Log("OS:" + runningOS);
    //    Debug.Log("_OS:" + _runningOS);
    //    switch (_runningOS)
    //    {
    //        case RunningOS.MobileDevice:
    //            break;

    //        case RunningOS.NotMobileDevice:
    //            break;

    //            default:
    //            RunningOS.Nothing:                
    //            break;
    //    }
    //} 

    #endregion
}

public enum RunningOS
{
    MobileDevice,
    NotMobileDevice,
    Nothing
}
