using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Shake : MonoBehaviour
{
    private CinemachineImpulseSource _impulse;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        _impulse = GetComponent<CinemachineImpulseSource>();
    }

    [ContextMenu("ShakeMenu")]
    private void ShakeMenu()
    {
        _impulse = GetComponent<CinemachineImpulseSource>();
        ShakeScreen(force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShakeScreen(float power)
    {
        _impulse.GenerateImpulse(power);
        Debug.Log("Shake");
    }
}
