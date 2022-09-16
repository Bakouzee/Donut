using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour  {

    private Vector2 movement;
    private Rigidbody2D rb;
    public float speed;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        transform.Translate(movement * speed * Time.deltaTime);
        //Debug.Log("vel: " + rb.velocity);
    }


    public void OnMove(InputAction.CallbackContext e) {
        if (e.performed)
            movement = e.ReadValue<Vector2>();
        if(e.canceled)
            movement = Vector2.zero;
    }
   
}
