using UnityEngine;

[CreateAssetMenu(menuName = "SFX/SfxClip")]
public class SFXClip : ScriptableObject
{
    public string id;
    public AudioClip clip;
    public float volume = 1f;
    public float pitch = 1f;
    public SfxChannel channel = SfxChannel.SFX;
    public bool loop = false;
}

public enum SfxChannel { SFX, Music }