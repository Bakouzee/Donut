using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : SingletonBase<AudioManager>
{
    [Header("UI")]
    public GameObject audioMenu;

    [Header("Source")]
    [SerializeField] private AudioSource mainAudioSource;
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    public AudioSource MainAudioSource => mainAudioSource;
    public AudioSource MusicAudioSource => musicAudioSource;
    public AudioSource SfxAudioSource => sfxAudioSource;

    [Header("Clips")]
    [SerializeField] private List<AudioClip> musicAudioClips = new List<AudioClip>();
    [SerializeField] private List<AudioClip> sfxAudioClips = new List<AudioClip>();

    private void Start()
    {
        //DontDestroyOnLoad(audioMenu);
        audioMenu.SetActive(false);
    }

    public void ShellHitRock()
    {
        sfxAudioSource.PlayOneShot(sfxAudioClips[0]);
    }

    public void ShellSpin()
    {
        sfxAudioSource.loop = true;
        sfxAudioSource.clip = sfxAudioClips[2];
        sfxAudioSource.Play();
    }

    public void StopShellSpin()
    {
        sfxAudioSource.loop = false;
        sfxAudioSource.Stop();
        sfxAudioSource.clip = null;
    }
    /* public IEnumerator Footstep()
     {
         sfxAudioSource.PlayOneShot(sfxAudioClips[1]);
         yield return new WaitForSeconds(sfxAudioClips[1].length);
     }*/

    #region Change Volume via Slider
    public void OnVolumeMasterChanged(float newVolume)
    {
        mainAudioSource.outputAudioMixerGroup.audioMixer.SetFloat("MasterVolume", newVolume);
    }
    public void OnVolumeMusicChanged(float newVolume)
    {
        mainAudioSource.outputAudioMixerGroup.audioMixer.SetFloat("MusicVolume", newVolume);
    }
    public void OnVolumeSFXChanged(float newVolume)
    {
        mainAudioSource.outputAudioMixerGroup.audioMixer.SetFloat("SFXVolume", newVolume);
    }
    #endregion
}
