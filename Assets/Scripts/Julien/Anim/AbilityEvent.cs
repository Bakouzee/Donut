using System.Collections;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

namespace Com.Donut.BattleSystem
{
    public class AbilityEvent : MonoBehaviour
    {
        private BattleSystem _battleSystem;

        //Move forward
        private float _offsetX = 50f;
        private Vector3 _startInit;
        private float _cooldownMoveDuration;

        //Move Backward
        private Vector3 _startInitBackward;
        private float _cooldownMoveDurationBackward;

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
            _cooldownMoveDuration = FindAnim(animationName);

            if (CheckIfEnemy())
            {
                _startInitBackward = transform.position;
                var position = _battleSystem.enemyTargetTransform.position;
                transform.DOMove(new Vector3(position.x + _offsetX, position.y, 0), _cooldownMoveDuration);
            }
            else
            {
                _startInit = transform.position;
                var position = _battleSystem.playerTargetTransform.position;
                transform.DOMove(new Vector3(position.x - _offsetX, position.y, 0), _cooldownMoveDuration);
            }
        }

        public void MoveBack(string animationName)
        {
            _cooldownMoveDurationBackward = FindAnim(animationName);

            transform.DOMove(CheckIfEnemy() ? _startInitBackward : _startInit, _cooldownMoveDurationBackward);
        }

        private float FindAnim(string animationName)
        {
            var animator = gameObject.GetComponent<Animator>();
            foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name != animationName) continue;
                
                return clip.length;
            }

            Debug.LogError("Anim clip not found --- Script ABILITY EVENT");
            return 0f;
        }

        private bool CheckIfEnemy()
        {
            return _battleSystem.ListEnemiesData.Any(p => p.FighterGo == gameObject);
        }
    }
}

