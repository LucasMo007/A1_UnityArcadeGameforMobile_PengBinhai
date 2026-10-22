using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasScript : MonoBehaviour
{
    void Start()
    {
        SetMasterVolume(0.0f);
        // Fill in for lab. Add all sounds to SoundManager.
        SoundManager.Instance.AddSound("Boom", Resources.Load<AudioClip>("boom"), SoundManager.SoundType.SOUND_SFX);
        SoundManager.Instance.AddSound("Death", Resources.Load<AudioClip>("death"), SoundManager.SoundType.SOUND_SFX);
        SoundManager.Instance.AddSound("Jump", Resources.Load<AudioClip>("jump"), SoundManager.SoundType.SOUND_SFX);
        SoundManager.Instance.AddSound("Laser", Resources.Load<AudioClip>("laser"), SoundManager.SoundType.SOUND_SFX);
        SoundManager.Instance.AddSound("MASK", Resources.Load<AudioClip>("MASK"), SoundManager.SoundType.SOUND_MUSIC);
        SoundManager.Instance.AddSound("TCats", Resources.Load<AudioClip>("Thundercats"), SoundManager.SoundType.SOUND_MUSIC);
        SoundManager.Instance.AddSound("Turtles", Resources.Load<AudioClip>("Turtles"), SoundManager.SoundType.SOUND_MUSIC);
    }

    public void PlaySFX(string soundKey)
    {
        //SoundManager.PlaySound(soundKey);
        SoundManager.Instance.PlaySound(soundKey);
    }

    public void PlayMusic(string soundKey)
    {
        //SoundManager.PlayMusic(soundKey);
        SoundManager.Instance.PlayMusic(soundKey);

    }
    public void OutputSFXVolume(float slideValue)
    {
        SoundManager.Instance.SetSFXVolume(slideValue);
    }

    public void OutputMusicVolume(float slideValue)
    {
        SoundManager.Instance.SetMusicVolume(slideValue);
    }
    public void SetMasterVolume(float sliderValue)
    {
        SoundManager.Instance.SetMasterVolume(sliderValue);
    }

    public void SetStereoPan(float sliderValue)
    {
        SoundManager.Instance.SetStereoPan(sliderValue);
    }
}
