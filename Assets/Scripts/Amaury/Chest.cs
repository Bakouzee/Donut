using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : MonoBehaviour {
    
    public bool isCollide;
    public bool isOpened;
    public GameObject openedChest;
    
    private SpriteRenderer spriteRenderer;

    public Dialog dialogOpened;
    private GameObject collideObj;

    public AudioClip open;
    
    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        DontDestroyOnLoad(openedChest);
    }
    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Player")) {
            isCollide = true;
            collideObj = col.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            isCollide = false;
            collideObj = other.gameObject;
        }
    }

    public void OnInteract(InputAction.CallbackContext e) {
        if (e.started && isCollide && !isOpened) {
            isOpened = true;
            openedChest.SetActive(true);
            transform.gameObject.SetActive(false);

            collideObj.GetComponent<Player>().hasKey = true;
            
            DialogController.Instance.StartDialog(dialogOpened);

            AudioManager.Instance.SfxAudioSource.clip = open;
            AudioManager.Instance.SfxAudioSource.Play();
        }
    }
}
