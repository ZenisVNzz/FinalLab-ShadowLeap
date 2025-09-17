using UnityEngine;

public class PlatformCameraController : MonoBehaviour
{
    [SerializeField] private GameObject staticCam;
    [SerializeField] private GameObject followCam;

    private void Start()
    {
#if UNITY_ANDROID
        staticCam.SetActive(false);
        followCam.SetActive(true);
#elif UNITY_WEBGL
        Debug.Log(SystemInfo.operatingSystem);

        if (SystemInfo.operatingSystem.Contains("Android"))
        {
            staticCam.SetActive(false);
            followCam.SetActive(true);
        }
        else if (SystemInfo.operatingSystem.Contains("iOS"))
        {
            staticCam.SetActive(false);
            followCam.SetActive(true);
        }
#endif


    }
}
