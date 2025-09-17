using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

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
        }
        else
        {
            Destroy(gameObject);
        }

        var handle = Addressables.LoadAssetAsync<VFXList>("VFXList");
        handle.Completed += op =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                VFXList = op.Result;
            }
        };
    }

    public void Initialize(int id, Vector2 position)
    {
        Process(id, position, Quaternion.identity, null);
    }

    public void Initialize(int id, Vector2 position, Quaternion rot)
    {
        Process(id, position, rot, null);
    }

    public void Initialize(int id, Vector2 position, Quaternion rot, Transform parent)
    {
        Process(id, position, rot, parent);
    }

    private void Process(int id, Vector2 position, Quaternion rot, Transform parent)
    {
        VFX vfx = VFXList.vfxList.Find(vfx => vfx.id == id);
        GameObject vfxPrefab = vfx.prefab;
        if (vfxPrefab != null)
        {
            GameObject vfxInstance = Instantiate(vfxPrefab, position, rot, parent);
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
