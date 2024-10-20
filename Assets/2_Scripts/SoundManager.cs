using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource sfxPistolFire;
    [SerializeField] AudioSource sfxPistolClick;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }   
    }

    public void SetBgm(float volume, float pitch)
    {
        bgm.volume = volume;
        bgm.pitch = pitch;
    }
    public void playBGM(AudioClip clip)
    { 
        bgm.clip = clip;
        bgm.Play();
    }
    public void StopBGM(AudioClip clip)
    {
        if (bgm.isPlaying == false)
            return;
        bgm.Stop();
    }
    public void PauseBGM()
    {
        if (bgm.isPlaying == false)
            return;
        bgm.Pause();
    }
    public void PlaySFXPistolFire(AudioClip clip)
    {
        sfxPistolFire.PlayOneShot(clip);
    }
    public void PlaySFXPistolClick(AudioClip clip)
    {
        sfxPistolClick.PlayOneShot(clip);
    }

}
