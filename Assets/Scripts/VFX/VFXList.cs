using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VFXList", menuName = "VFX/NewVFXList")]
public class VFXList : ScriptableObject
{
    public List<VFX> vfxList;
}
