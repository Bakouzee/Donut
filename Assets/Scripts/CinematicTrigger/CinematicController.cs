using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class CinematicController : MonoBehaviour
{
    private PlayableDirector playableDirector;

    private void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(playableDirector != null && collision.gameObject.CompareTag("Player"))
        {
            playableDirector.Play();
            playableDirector = null;
        }
    }
}
