using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;


    private readonly string Volume = "Volume_SFX";
    // Start is called before the first frame update
    void Start()
    {
        _audioMixer.SetFloat(Volume, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
