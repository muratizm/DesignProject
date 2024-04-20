using UnityEngine;


public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    private AudioSource musicSource, sfxSource, uiSource;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();
        uiSource = gameObject.AddComponent<AudioSource>();
    }


    public void PlayMusic(string path, float volume = 1f)
    {
        AudioClip clip = AssetLibrary.GetAudioClip(path);

        if (clip == null) { Debug.LogWarning("Music: " + path + " not found!"); return; }
        if (musicSource.clip == clip) return;

        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(string path, float volume = 0.5f)
    {

        AudioClip clip = AssetLibrary.GetAudioClip(path);
        if (clip == null) { Debug.LogWarning("Music: " + path + " not found!"); return; }
        sfxSource.volume = volume;
        sfxSource.PlayOneShot(clip);
    }

    public void PlayUI(string path, float volume = 0.5f)
    {
        AudioClip clip = AssetLibrary.GetAudioClip(path);
        if (clip == null) { Debug.LogWarning("Music: " + path + " not found!"); return; }
        uiSource.volume = volume;
        uiSource.PlayOneShot(clip);
    }
}