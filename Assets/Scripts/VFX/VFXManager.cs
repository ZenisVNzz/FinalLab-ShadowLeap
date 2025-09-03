using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance;
    private VFXList VFXList;
    private Dictionary<int, GameObject> activeVFX = new Dictionary<int, GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(int id, Vector2 position)
    {
        VFX vfx = VFXList.vfxList.Find(vfx => vfx.id == id);
        GameObject vfxPrefab = vfx.prefab;
        if (vfxPrefab != null)
        {
            GameObject vfxInstance = Instantiate(vfxPrefab, position, Quaternion.identity);
            if (vfx.autoDestroy)
            {
                Destroy(vfxInstance, vfxInstance.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            }    
            else
            {
                int instanceId = vfxInstance.GetInstanceID();
                activeVFX.Add(instanceId, vfxInstance);
            }    
        }    
        else
        {
            Debug.LogWarning($"VFX with ID {id} not found.");
        }
    }
}
