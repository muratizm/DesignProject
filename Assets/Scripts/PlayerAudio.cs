using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PlayerAudio : MonoBehaviour
{
    private static PlayerAudio instance;
    public static PlayerAudio Instance { get { return instance; } }



    private List<AudioClip> footstepSounds;    // an array of footstep sounds that will be randomly selected from.
    private int previousFootstepIndex;

    private AudioClip jumpSound;
    private AudioClip landSound;

    private AudioSource m_AudioSource;



    void Start()
    {
        Debug.Log("PlayerAudio Start");
        m_AudioSource = gameObject.AddComponent<AudioSource>();

    }


    public void PlayFootStepAudio()
    {
        Debug.Log(footstepSounds);
        Debug.Log(footstepSounds.Count);
        int n = Random.Range(1, footstepSounds.Count);
        while (n == previousFootstepIndex) // Check if the new index is the same as the previous one
        {
            n = Random.Range(1, footstepSounds.Count); // Generate a new random index
        }

        previousFootstepIndex = n; // Store the current index as the previous one

        m_AudioSource.clip = footstepSounds[n];
        m_AudioSource.PlayOneShot(m_AudioSource.clip);
        // move picked sound to index 0 so it's not picked next time

    }


    public void PlayJumpSound()
    {
        Debug.Log("Jump sound");
        m_AudioSource.clip = jumpSound;
        m_AudioSource.Play();
    }

    public void PlayLandingSound()
    {
        m_AudioSource.clip = landSound;
        m_AudioSource.Play();
    }

    public void LoadSoundsForGround(string groundTag)
    {
        Debug.Log("Loading sounds for ground from addressables");
        //addressable code to load footstep sounds
        Addressables.LoadAssetAsync<AudioClip>(groundTag + "_jump").Completed += (audioClip) =>
        {
            jumpSound = audioClip.Result;
        };
        Addressables.LoadAssetAsync<AudioClip>(groundTag + "_land").Completed += (audioClip) =>
        {
            landSound = audioClip.Result;
        };
        Addressables.LoadAssetsAsync<AudioClip>(groundTag + "_walk", null).Completed += handle =>
        {
            footstepSounds = new List<AudioClip>();
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                foreach (var audioClip in handle.Result)
                {
                    footstepSounds.Add(audioClip);
                }
            }
            else
            {
                Debug.LogError("Failed to load audio clips: " + handle.OperationException);
            }
        };
    }




}
