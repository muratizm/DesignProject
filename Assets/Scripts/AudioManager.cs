using System.Threading.Tasks;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    private AudioSource musicSource, sfxSource, uiSource, voiceSource;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();
        uiSource = gameObject.AddComponent<AudioSource>();
        voiceSource = gameObject.AddComponent<AudioSource>();
    }


    public async void PlayMusic(string path, float volume = 1f)
    {
        AudioClip clip = AssetLibrary.GetAudioClip(path);

        // Fade out current music if it exists
        if (clip != null) 
        { 
            while (musicSource.volume > 0f)
            {
                musicSource.volume -= Time.deltaTime * 2; // adjust the 2 to change the fade speed
                await Task.Delay(10);
            }

            musicSource.Stop();          
        }

        // Play new music
        musicSource.clip = clip;
        musicSource.Play();

        // Fade in new music
        while (musicSource.volume < volume)
        {
            musicSource.volume += Time.deltaTime * 2; // adjust the 2 to change the fade speed
            await Task.Delay(10);
        }
    }

    public void PlayVoice(AudioClip clip, float volume = 0.5f)
    {
        if (clip == null) { Debug.LogWarning("null clip"); return; }
        voiceSource.Stop();
        voiceSource.volume = volume;
        voiceSource.PlayOneShot(clip);
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