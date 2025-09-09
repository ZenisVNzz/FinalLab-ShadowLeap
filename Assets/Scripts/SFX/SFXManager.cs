using UnityEngine;
using UnityEngine.AddressableAssets;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    private AudioSource musicSource;
    private SFXList sfxList;

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

        sfxList = Addressables.LoadAssetAsync<SFXList>("SFXList").WaitForCompletion();
    }

    public void PlaySFX(string id)
    {
        AudioSource sfxSource = gameObject.AddComponent<AudioSource>();

        SFXClip clip = sfxList.SFXClips.Find(sfx => sfx.id == id && sfx.channel == SfxChannel.SFX);
        if (clip != null)
        {
            sfxSource.clip = clip.clip;
            sfxSource.volume = clip.volume;
            sfxSource.pitch = clip.pitch;
            sfxSource.loop = clip.loop;
            sfxSource.Play();

            if (!clip.loop)
            {
                Destroy(sfxSource, clip.clip.length);
            }
        }
        else
        {
            Debug.LogWarning($"SFX with ID {id} not found.");
        }
    }

    public void PlayMusic(string id)
    {
        SFXClip clip = sfxList.SFXClips.Find(sfx => sfx.id == id && sfx.channel == SfxChannel.Music);
        if (clip != null)
        {
            musicSource.clip = clip.clip;
            musicSource.volume = clip.volume;
            musicSource.pitch = clip.pitch;
            musicSource.loop = clip.loop;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning($"Music with ID {id} not found.");
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void StopSFX(string id)
    {
        AudioSource[] sources = GetComponents<AudioSource>();
        foreach (AudioSource source in sources)
        {
            if (source.clip != null && source.clip.name == id && source != musicSource)
            {
                source.Stop();
                Destroy(source);
                break;
            }
        }
    }
}
