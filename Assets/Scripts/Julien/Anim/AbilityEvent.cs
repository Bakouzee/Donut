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

        public void Move()
        {
            CheckIfEnemy();
            _startInit = transform.position;
            var animator = gameObject.GetComponent<Animator>();
            var currentClipInfo = animator.GetCurrentAnimatorClipInfo(0);
            var currentClipLength = currentClipInfo[0].clip.length; //Access the current length of the clip
            _cooldownMoveDuration = currentClipLength;
            _startMoving = true;
        }

        private void CheckIfEnemy()
        {
            if (gameObject == _battleSystem.enemy1Go)
            {
                _isAnEnemy = true;
            }
        }

        public void MoveBack()
        {
            _startInitBackward = transform.position;
            var animator = gameObject.GetComponent<Animator>();
            var currentClipInfo = animator.GetCurrentAnimatorClipInfo(0);
            var currentClipLength = currentClipInfo[0].clip.length; //Access the current length of the clip
            _cooldownMoveDurationBackward = currentClipLength;
            _startMovingBackward = true;
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

