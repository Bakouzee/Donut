using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogSpawner : MonoBehaviour {

    public Dialog dialogToStart;

    private bool isCollide;

    private void Update()
    {
        Debug.Log("collide " + isCollide);
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Player"))
            isCollide = true;
    }

    private void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.CompareTag("Player"))
            isCollide = false;
    }

    public void OnInteract(InputAction.CallbackContext e) {
        if (e.started && isCollide && !DialogController.Instance.isInDialog)
        {
            Debug.Log("my start");
            DialogController.Instance.StartDialog(dialogToStart);
        }
    }
}
