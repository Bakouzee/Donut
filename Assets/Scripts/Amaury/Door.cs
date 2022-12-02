using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {
    private bool isCollide;
    private bool isOpened;
    private GameObject collideObj;

    public Dialog notOpened;
    [SerializeField] private GameObject endMenu;

    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
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
        if (e.started && isCollide) {
            
            if(!collideObj.GetComponent<Player>().hasKey)
                DialogController.Instance.StartDialog(notOpened);
            else
            {
                //collideObj.GetComponent<Player>().playerInput.DeactivateInput();
                //endMenu.SetActive(true);
                transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                transform.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                SceneManager.LoadScene("CFINISCENE");
            }
        }
    }
}
