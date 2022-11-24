using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXmanager : MonoBehaviour
{

    [SerializeField] private VisualEffect _healEffect;

    [ContextMenu("Heal")]
    public void Heal()
    {
        _healEffect.Play();
    }
}
