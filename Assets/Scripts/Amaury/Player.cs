using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character  {

    public Vector2 movement;
    private Rigidbody2D rb;
    public float speed;
    public List<IFollowable> followers;

    private int lastFollowersSize = -1;
    
    
    

    public override void Awake() {
        base.Awake();
        
        rb = GetComponent<Rigidbody2D>();
        followers = new List<IFollowable>();

    }

    public override void Update() {
        base.Update();
        
        if(lastFollowersSize != followers.Count)
            ManageFollowers(followers.Count > lastFollowersSize); // To Modify : probably doesn't work with follower remove

        lastFollowersSize = followers.Count;
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

    private void ManageFollowers(bool add) {
        int followerIndex = add ? followers.Count - 1 : 0; // To Modify with remove
        
        Debug.Log("in list " + followers[followerIndex]);
        
        Debug.Log("value before " + followers[followerIndex].target);
        
        followers[followerIndex].target = this;
        
        Debug.Log("value after " + followers[followerIndex].target);
        
        
    }

    public void OnMove(InputAction.CallbackContext e) {
        if (e.performed)
            movement = e.ReadValue<Vector2>();
        if(e.canceled)
            movement = Vector2.zero;
    }
   
}
