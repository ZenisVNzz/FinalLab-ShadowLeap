using UnityEngine;

public class MobileUIController : MonoBehaviour
{
    [SerializeField] private GameObject MobileUI;

    void Start()
    {
#if UNITY_ANDROID
        MobileUI.SetActive(true);
#elif UNITY_WEBGL
        Debug.Log(SystemInfo.operatingSystem);

        if (SystemInfo.operatingSystem.Contains("Android"))
        {
            MobileUI.SetActive(true);
        }
        else if (SystemInfo.operatingSystem.Contains("iOS"))
        {
            MobileUI.SetActive(true);
        }
#endif
    }
}
