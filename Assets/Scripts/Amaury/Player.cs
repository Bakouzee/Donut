using Com.Donut.BattleSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class Player : Character  {

    [SerializeField] private BattleSystem battleSystem;
    public Vector2 movement;
    private Rigidbody2D rb;
    public float speed;
    private float initialSpeed;
    public List<IFollowable> followers;

    private int lastFollowersSize = -1;

    public bool hasCarapace;
    public bool isTransformed;

    public GameObject arrow;
    [SerializeField] public PlayerInput playerInput;

    private Vector3 direction;
    private Vector3 lastVelocity;

    [SerializeField] private GameObject abilityImg;
    [SerializeField] private GameObject playerImg;

    public override void Awake() {
        base.Awake();
        
        rb = GetComponent<Rigidbody2D>();
        followers = new List<IFollowable>();

        initialSpeed = speed;
    }

    public override void Update() {
        base.Update();

        //  if(lastFollowersSize != followers.Count)
        //    ManageFollowers(followers.Count > lastFollowersSize); // To Modify : probably doesn't work with follower remove

        lastFollowersSize = followers.Count;
    }
    
    protected override void Move() {
        if (!hasCarapace)
        {
            if (movement == Vector2.zero)
                SwitchAnimState(IDLES[0]);
            else {
                string anim_id = movement.x != 0 && movement.y == 0 ? WALKS[2] : movement.y > 0 && movement.x == 0 ? WALKS[1] : WALKS[0];
                SwitchAnimState(anim_id);
            }
        }
        else if(!isTransformed)
        {
            if (movement == Vector2.zero)
                SwitchAnimState(IDLES[0]);
            else {
                string anim_id = movement.x != 0 && movement.y == 0 ? WALKS_CARAPACE[2] : movement.y > 0 && movement.x == 0 ? WALKS_CARAPACE[1] : WALKS_CARAPACE[0];
                SwitchAnimState(anim_id);
            }
        }

        rb.velocity = movement * speed;
        
        if (isTransformed)
            rb.velocity = direction * speed;
        
        
        spriteRenderer.flipX = movement.x < 0 && movement.y == 0;
        lastVelocity = rb.velocity;
    }

    private void ManageFollowers(bool add) {
        int followerIndex = add ? followers.Count - 1 : 0; // To Modify with remove
        
        followers[followerIndex].target = this;
    }

    public void OnMove(InputAction.CallbackContext e) {
        if (e.performed)
            movement = e.ReadValue<Vector2>();
        if(e.canceled)
            movement = Vector2.zero;
    }

    public void OnTransformation(InputAction.CallbackContext e) {
        if (e.performed) {
            SwitchAnimState("WC_Run");
            isTransformed = !isTransformed;
            if(isTransformed == true)
            {
                direction = Vector3.zero;
                abilityImg.SetActive(true);
                abilityImg.transform.DOMoveY(abilityImg.transform.position.y - 15f, 0.5f).SetEase(Ease.InElastic).SetEase(HideImg);
                playerImg.transform.DOMoveY(abilityImg.transform.position.y + 15f, 0.5f).SetEase(Ease.InElastic);
            }
            else
            {
                playerImg.SetActive(true);
                playerImg.transform.DOMoveY(abilityImg.transform.position.y - 15f, 0.5f).SetEase(Ease.InElastic).SetEase(HideImg);
                abilityImg.transform.DOMoveY(abilityImg.transform.position.y + 15f, 0.5f).SetEase(Ease.InElastic);
            }
        }
    }

    private float HideImg(float time, float duration, float overshootOrAmplitude, float period)
    {
        if (time <= 0) {
            if (isTransformed)
            {
                abilityImg.SetActive(false);
            }
            else
            {
                playerImg.SetActive(false);
            }
        }
        return 0; 
    }

    public void OnThrow(InputAction.CallbackContext e) {
        if (e.performed) {
            direction = (arrow.transform.position - transform.position).normalized;
            
        }
    }

    public void OnSetBattlePhaseDebug(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            battleSystem.SetState(new Init(battleSystem));
        }
    }

    public void OnSetExplorationPhaseDebug(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            battleSystem.SetState(new Exploration(battleSystem));
        }
    }

    public void OnChangedMap(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log("The Default map has been changed !!");
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Destructible") && isTransformed)
        {
            Destroy(col.gameObject);
        }
        Vector3 reflectVec = Vector3.Reflect(lastVelocity.normalized,col.contacts[0].normal);
        direction = reflectVec;
    }
}
