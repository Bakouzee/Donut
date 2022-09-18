using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character  {

    public Vector2 movement;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public float speed;
    
    // Animations 
    private static string[] IDLES = new string[] {"M_I_Front", "M_I_Right" };
    private static string[] WALKS = new string[] { "M_W_Front","M_W_Back","M_W_Side" }; 
    
    

    public override void Start() {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    protected override void Move() {
        if (movement == Vector2.zero)
            SwitchAnimState(IDLES[0]);
        else {
            string anim_id = movement.x != 0 && movement.y == 0 ? WALKS[2] : movement.y > 0 && movement.x == 0 ? WALKS[1] : WALKS[0];
            SwitchAnimState(anim_id);
        }

        rb.velocity = movement * speed;
        
        spriteRenderer.flipX = movement.x < 0 && movement.y == 0;
    }

    

    public void OnMove(InputAction.CallbackContext e) {
        if (e.performed)
            movement = e.ReadValue<Vector2>();
        if(e.canceled)
            movement = Vector2.zero;
    }
   
}
