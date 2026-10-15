using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager:MonoBehaviour 
{ 
    public enum SoundType
    {
        SOUND_SFX,
        SOUND_MUSIC
    }
    public static SoundManager Instance { get; private set; }
    // Create Dictionary for sfx.
    private  Dictionary<string, AudioClip> sfxDictionary = new Dictionary<string, AudioClip>();
    // Create Dictionary for music.
    private   Dictionary<string,AudioClip>musicDictionary = new Dictionary<string, AudioClip>();
    // Create AudioSource for sfx.
    private AudioSource sfxSource;
    // Create AudioSource for music.
    private AudioSource musicSource;
    

    
    private void Awake()//singleton pattern
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Initialize the SoundManager. I just put this functionality here instead of in the static constructor.
    private  void Initialize()
    {
        // Create a new GameObject to hold the AudioSource
       
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.volume = 1.0f;

        // Fill in for lab.
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.volume = 1.0f;
        musicSource.loop = true;
    }

    // Add a sound to the dictionary.
    public  void AddSound(string soundKey, AudioClip audioClip, SoundType soundType)
    {
        // Fill in for lab.
        Dictionary<string, AudioClip> TargetDictionary = GetDictionaryByType(soundType);
        if (!TargetDictionary.ContainsKey(soundKey))
        {
            TargetDictionary.Add(soundKey, audioClip);
        }
        else
        {
            Debug.LogWarning("Sound Key" + soundKey + "already exists in the" + soundType + "dictinonary");
        }

    }

    // Play a sound by key interface.
    public  void PlaySound(string soundKey)
    {
        // Fill in for lab.
        Play(soundKey, SoundType.SOUND_SFX);
    }

    // Play music by key interface.
    public  void PlayMusic(string soundKey)
    {
        // Fill in for lab.
        musicSource.Stop();
        Play(soundKey, SoundType.SOUND_MUSIC);
    }

    // Play utility.
    private  void Play(string soundKey, SoundType soundType)
    {
        // Fill in for lab.
        Dictionary<string, AudioClip> targetDictionary;
        AudioSource targetSource;

        SetTargetsByType(soundType, out targetDictionary, out targetSource);
        if (targetDictionary.ContainsKey(soundKey))
        {
            targetSource.PlayOneShot(targetDictionary[soundKey]);

        }
        else
        {
            Debug.LogWarning("Sound Key" + soundKey + "not found in the" + soundType + "dictinonary");
        }

    }

    private  void SetTargetsByType(SoundType soundType, out Dictionary<string, AudioClip> targetDictionary, out AudioSource targetSource)
    {
        // Fill in for lab.
        switch (soundType)
        {

            case SoundType.SOUND_SFX:
                targetDictionary = sfxDictionary;
                targetSource = sfxSource;
                break;
            case SoundType.SOUND_MUSIC:
                targetDictionary = musicDictionary;
                targetSource = musicSource;
                break;
            default:
                Debug.LogError("Unkonw sound type:" + soundType);
                targetDictionary = null;
                targetSource = null;
                break;

        }

    }
    private  Dictionary<string, AudioClip> GetDictionaryByType(SoundType soundType)
    {
        // Fill in for lab.
        switch (soundType)
        {
            case SoundType.SOUND_SFX:
                return sfxDictionary;
            case SoundType.SOUND_MUSIC:
                return musicDictionary;
            default:
                Debug.LogError("Unknow sound Type:" + soundType);
                return null;
        }
    }
    public  void SetSFXVolume(float volume)
    {
        if (sfxSource != null)
            sfxSource.volume = volume;
    }

    public  void SetMusicVolume(float volume)
    {
        if (musicSource != null)
            musicSource.volume = volume;
    }
    public  void SetMasterVolume(float volume)
    {
        if (sfxSource != null)
            sfxSource.volume = volume;

        if (musicSource != null)
            musicSource.volume = volume;
    }

    public  void SetStereoPan(float pan)
    {
        pan = Mathf.Clamp(pan, -1f, 1f); // Ensure pan stays in range
        if (sfxSource != null)
            sfxSource.panStereo = pan;

        if (musicSource != null)
            musicSource.panStereo = pan;
    }
}