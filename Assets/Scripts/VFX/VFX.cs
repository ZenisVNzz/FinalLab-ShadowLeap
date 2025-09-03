using UnityEngine;

[CreateAssetMenu(fileName = "VFX", menuName = "VFX/NewVFX")]
public class VFX : ScriptableObject
{
    public GameObject prefab;
    public int id;
    public bool autoDestroy;
}
