using UnityEngine;

public class MobileUIController : MonoBehaviour
{
    [SerializeField] private GameObject MobileUI;

    void Start()
    {
#if UNITY_ANDROID
        MobileUI.SetActive(true);
#endif
    }
}
