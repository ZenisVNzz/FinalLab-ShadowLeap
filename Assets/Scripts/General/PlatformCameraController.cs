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
#endif
    }
}
