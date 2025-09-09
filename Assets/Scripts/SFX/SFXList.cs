using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SFXList", menuName = "SFX/SFXList", order = 1)]
public class SFXList : ScriptableObject
{
    public List<SFXClip> SFXClips = new List<SFXClip>();
}
