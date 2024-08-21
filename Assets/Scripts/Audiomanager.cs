using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Audiomanager : MonoBehaviour
{
    public static Audiomanager instance;

    // Background Music
    public AudioSource bgmSource;

    // Sound Effects
    public AudioSource sfxSource;

    // Audio Clips for BGM and SFX
    public AudioClip[] bgmClips;
    public AudioClip[] sfxClips;

    public float BGMvolumn;
    public float SfxVolume;

    private void Start()
    {

    }

    // Play Background Music
    public void PlayBGM(int index)
    {
        if (index >= 0 && index < bgmClips.Length)
        {
            bgmSource.clip = bgmClips[index];
            bgmSource.Play();
        }
        else
        {
            Debug.LogError("BGM index out of range.");
        }
    }

    // Play Sound Effect
    public void PlaySFX(int index)
    {
        if (index >= 0 && index < sfxClips.Length)
        {
            sfxSource.PlayOneShot(sfxClips[index]);
        }
        else
        {
            Debug.LogError("SFX index out of range.");
        }
    }

    // Stop Background Music
    public void StopBGM()
    {
        bgmSource.Stop();
    }

    // Stop Sound Effect
    public void StopSFX()
    {
        sfxSource.Stop();
    }

    // Adjust Volume for BGM and SFX
    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
