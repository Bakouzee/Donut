using System;
using System.Collections;
using System.Collections.Generic;
using Com.Donut.BattleSystem;
using Unity.VisualScripting;
using UnityEngine;

public class AbilityEvent : MonoBehaviour
{
    private BattleSystem _battleSystem;
    
    //Move forward
    private bool _startMoving = false;
    private float _offsetX = 50f;

    private Vector3 _startInit;
    private float _cooldownMoveDuration;
    private float _currentCooldown;
    
    //Move Backward
    private bool _startMovingBackward = false;
    private Vector3 _startInitBackward;
    private float _cooldownMoveDurationBackward;
    private float _currentCooldownBackward;
    private void Awake()
    {
        _battleSystem = FindObjectOfType<BattleSystem>();
    }
    
    public void EndOfAnim()
    {
        _battleSystem.Animation_End();
    }

    public void Move() 
    {
        _startInit = transform.position;
        var animator = gameObject.GetComponent<Animator>();
        var currentClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        var currentClipLength = currentClipInfo[0].clip.length;   //Access the current length of the clip
        _cooldownMoveDuration = currentClipLength;  
        _startMoving = true;
    }

    public void MoveBack()
    {
        _startInitBackward = transform.position;
        var animator = gameObject.GetComponent<Animator>();
        var currentClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        var currentClipLength = currentClipInfo[0].clip.length;   //Access the current length of the clip
        _cooldownMoveDurationBackward = currentClipLength;  
        _startMovingBackward = true;
    }

    private void Update()
    {
        if (_startMoving)
        {
            if (_currentCooldown <= _cooldownMoveDuration)
            {
                _currentCooldown += Time.deltaTime;
                transform.position = Vector3.Lerp(_startInit, new Vector3(_battleSystem.PlayerTargetTransform.position.x - _offsetX, _battleSystem.PlayerTargetTransform.position.y, 0), _currentCooldown / _cooldownMoveDuration);

            }
            else 
            {
                transform.position = new Vector3(_battleSystem.PlayerTargetTransform.position.x - _offsetX, _battleSystem.PlayerTargetTransform.position.y, 0);
                _startMoving = false;
            }
        }
        
        else if (_startMovingBackward)
        {
            if (_currentCooldownBackward <= _cooldownMoveDurationBackward)
            {
                _currentCooldownBackward += Time.deltaTime;
                transform.position = Vector3.Lerp(_startInitBackward, _startInit, _currentCooldownBackward / _cooldownMoveDurationBackward);
            }
            else
            {
                transform.position = new Vector3(_startInit.x, _startInit.y, 0);
                _startMovingBackward = false;
            }
        }
    }
}

