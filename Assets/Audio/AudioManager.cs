using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : SingletonBase<AudioManager>
{
    private AudioSource audioSource;
    public AudioSource AudioSource => audioSource;
    public GameObject audioMenu;

    private void Start()
    {
        audioMenu.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    #region Change Volume via Slider
    public void OnVolumeMasterChanged(float newVolume)
    {
        audioSource.outputAudioMixerGroup.audioMixer.SetFloat("MasterVolume", newVolume);
    }
    public void OnVolumeMusicChanged(float newVolume)
    {
        audioSource.outputAudioMixerGroup.audioMixer.SetFloat("MusicVolume", newVolume);
    }
    public void OnVolumeSFXChanged(float newVolume)
    {
        audioSource.outputAudioMixerGroup.audioMixer.SetFloat("SFXVolume", newVolume);
    }
    #endregion
}
