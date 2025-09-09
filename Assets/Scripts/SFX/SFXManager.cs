using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    private AudioSource musicSource;  
    private AudioSource sfxSource;       
    private Dictionary<string, AudioSource> activeLoopSFX = new();

    private SFXList sfxList;

    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;

        sfxSource = gameObject.AddComponent<AudioSource>();

        sfxList = Addressables.LoadAssetAsync<SFXList>("SFXList").WaitForCompletion();
    }

    public void PlaySFX(string id)
    {
        SFXClip clip = sfxList.SFXClips.Find(sfx => sfx.id == id && sfx.channel == SfxChannel.SFX);
        if (clip == null)
        {
            Debug.LogWarning($"SFX with ID {id} not found.");
            return;
        }

        if (clip.loop)
        {
            if (!activeLoopSFX.ContainsKey(id))
            {
                var loopSource = gameObject.AddComponent<AudioSource>();
                loopSource.clip = clip.clip;
                loopSource.volume = clip.volume;
                loopSource.pitch = clip.pitch;
                loopSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
                loopSource.loop = true;
                loopSource.Play();
                activeLoopSFX[id] = loopSource;
            }
        }
        else
        {
            sfxSource.volume = clip.volume;
            sfxSource.pitch = clip.pitch;
            sfxSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
            sfxSource.PlayOneShot(clip.clip);
        }
    }

    public void StopSFX(string id)
    {
        if (activeLoopSFX.TryGetValue(id, out var source))
        {
            source.Stop();
            Destroy(source);
            activeLoopSFX.Remove(id);
        }
    }

    public void PlayMusic(string id)
    {
        SFXClip clip = sfxList.SFXClips.Find(sfx => sfx.id == id && sfx.channel == SfxChannel.Music);
        if (clip == null)
        {
            Debug.LogWarning($"Music with ID {id} not found.");
            return;
        }

        musicSource.clip = clip.clip;
        musicSource.volume = clip.volume;
        musicSource.pitch = clip.pitch;
        musicSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Music")[0];
        musicSource.loop = clip.loop;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}