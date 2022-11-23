using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
public class FloodGlowUp : MonoBehaviour
{
    float current = 0.0f;
    bool tdedans;
    public Volume  m_Volume;
    Vignette m_Vignette;

    private void Start()
    {
      
        Debug.Log(m_Volume);
    }
    private void Update()
    {
        m_Volume.profile.TryGet<Vignette>(out Vignette v);
        if (v.intensity.value == 1f)
            return;

        if(tdedans)
        {
           
            float target = 0.4f;

            float delta = target - v.intensity.value;
            delta *= Time.deltaTime;

            v.intensity.value += delta;
        }else
        {
           
            float target = 1f;

            float delta = target - v.intensity.value;
            delta *= Time.deltaTime;

            v.intensity.value += delta;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Player"))
        {
            tdedans = true;

            float speed = 0.3f;

            m_Volume.profile.TryGet<Vignette>(out Vignette v);
            //v.intensity.value = Mathf.Lerp(v.intensity.value, 0.40f,speed);

            float target = 0.4f;

            float delta = target - v.intensity.value;
            delta *= Time.deltaTime;

            v.intensity.value += delta;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        float speed = 0.3f;
        if (collision.CompareTag("Player"))
        {
            tdedans = false;

            m_Volume.profile.TryGet<Vignette>(out Vignette v);
            //v.intensity.value = 1f;

            //v.intensity.value = Mathf.Lerp(v.intensity.value, 1f, speed);


            float target = 1.0f;

            float delta = target - v.intensity.value;
            delta *= Time.deltaTime;

            v.intensity.value += delta;
        }
    }

}
