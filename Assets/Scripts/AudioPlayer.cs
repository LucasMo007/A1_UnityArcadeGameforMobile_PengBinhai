using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField][Range(0f, 1f)] float shootingVolume = 1f;

    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField][Range(0f, 1f)] float damageVolume = 1f;

    [Header("UI")]
    [SerializeField] AudioClip menuToggleClip;
    [SerializeField][Range(0f, 1f)] float uiVolume = 1f;

    [SerializeField] AudioSource musicSource;
    [SerializeField] float sfxVolume = 1f;


    static AudioPlayer instance;
   
     void Awake()
    {

        //ManageSingleton();
        Debug.Log("AudioPlayer initialized via MainController");
    }
    /*void ManageSingleton ()
    {
     //int instanceCount = FindObjectsOfType(GetType()).Length;
       // if (instanceCount > 1)
       if (instance != null) 
        {
             gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
             instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }*/

    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }
    public void PlayDamageClip()
    {
       PlayClip(damageClip,damageVolume);
    }
    /*void PlayClip(AudioClip clip,float volume)
    {
        if (clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos,volume);
        }
    }*/
    void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume * sfxVolume);
        }
    }
    public void PlayClipAtCamera(AudioClip clip, float volume = 1f)
    {
        if (clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
    public void PlayMenuToggleClip()
    {
        PlayClip(menuToggleClip, uiVolume);
    }
    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
        Debug.Log("SFX Volume set to: " + value);
    }

    public void SetMusicVolume(float value)
    {
        if (musicSource != null)
        {
            musicSource.volume = value;
            Debug.Log("Music Volume set to: " + value);
        }
    }
}
