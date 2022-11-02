using System.Collections;
using System.Linq;
using UnityEngine;

namespace Com.Donut.BattleSystem
{
    public class AbilityEvent : MonoBehaviour
    {
        private BattleSystem _battleSystem;

        private bool _isAnEnemy = false;

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

        public void LaunchHitEffect()
        {
            _battleSystem.HitEffect();
        }

        public void Move(string animationName)
        {
            CheckIfEnemy();
            _cooldownMoveDuration = FindAnim(animationName);
            
            _startInit = transform.position;
            _startMoving = true;
        }
        
        public void MoveBack(string animationName)
        {
            _cooldownMoveDurationBackward = FindAnim(animationName);
            _startInitBackward = transform.position;
            _startMovingBackward = true;
        }

        private float FindAnim(string animationName)
        {
            var animator = gameObject.GetComponent<Animator>();
            foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == animationName)
                {
                    Debug.Log("name: " + clip.name + "length: " + clip.length);
                    return clip.length;
                }
            }

            Debug.LogError("Anim clip not found --- Script ABILITY EVENT");
            return 0f;
        }

        private void CheckIfEnemy()
        {
            if (_battleSystem.ListEnemiesData.Any(p => p.FighterGo == gameObject))
            {
                _isAnEnemy = true;
            }
        }



        private void Update()
        {
            if (_startMoving)
            {
                if (!_isAnEnemy)
                {
                    if (_currentCooldown <= _cooldownMoveDuration)
                    {
                        _currentCooldown += Time.deltaTime;
                        var position = _battleSystem.playerTargetTransform.position;
                        MoveForward(_startInit, new Vector3(position.x - _offsetX, position.y, 0));

                    }
                    else
                        EndOfForwardMove(new Vector3(_battleSystem.playerTargetTransform.position.x - _offsetX, _battleSystem.playerTargetTransform.position.y, 0));
                }
                else
                {
                    if (_currentCooldown <= _cooldownMoveDuration)
                    {
                        _currentCooldown += Time.deltaTime;
                        var position = _battleSystem.enemyTargetTransform.position;
                        MoveForward(_startInit, new Vector3(position.x + _offsetX, position.y, 0));

                    }
                    else
                        EndOfForwardMove(new Vector3(_battleSystem.playerTargetTransform.position.x - _offsetX, _battleSystem.playerTargetTransform.position.y, 0));
                }

            }

            else if (_startMovingBackward)
            {
                if (!_isAnEnemy)
                {
                    if (_currentCooldownBackward <= _cooldownMoveDurationBackward)
                    {
                        _currentCooldownBackward += Time.deltaTime;
                        MoveBackward(_startInitBackward, _startInit);
                    }
                    else
                        EndOfBackwardMove();
                }
                else
                {
                    if (_currentCooldownBackward <= _cooldownMoveDurationBackward)
                    {
                        _currentCooldownBackward += Time.deltaTime;
                        MoveBackward(_startInitBackward, _startInit);
                    }
                    else
                        EndOfBackwardMove();
                }
            }
        }

        private void MoveForward(Vector3 initPos, Vector3 finalPos)
        {
            transform.position = Vector3.Lerp(initPos, finalPos,_currentCooldown / _cooldownMoveDuration);
        }

        private void MoveBackward(Vector3 initPos, Vector3 finalPos)
        {
            transform.position = Vector3.Lerp(initPos, finalPos, _currentCooldownBackward / _cooldownMoveDurationBackward);
        }

        private void EndOfForwardMove(Vector3 finalPos)
        {
            transform.position = finalPos;
            _startMoving = false;
            _currentCooldown = 0;
        }
        private void EndOfBackwardMove()
        {
            transform.position = new Vector3(_startInit.x, _startInit.y, 0);
            _startMovingBackward = false;
            _currentCooldownBackward = 0;
        }
        
    }
}

