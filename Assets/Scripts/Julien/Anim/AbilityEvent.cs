using System.Collections;
using System.Linq;
using UnityEngine;
using DG.Tweening;

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
        
        //Jump
        private Vector3 _startInitJump;
        private float _cooldownJumpDuration;
        
        //JumpBackward
        private Vector3 _startInitJumpBackward;
        private float _cooldownJumpBackwardDuration;

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

        public void Jump(string animationName)
        {
            _cooldownJumpDuration = FindAnim(animationName);
            
            if (CheckIfEnemy())
            {
                _startInitJumpBackward = transform.position;
                var position = _battleSystem.enemyTargetTransform.position;
                StartCoroutine(JumpAnim(position, _cooldownJumpDuration));

            }
            else
            {
                _startInitJump = transform.position;
                var position = _battleSystem.playerTargetTransform.position;
                StartCoroutine(JumpAnim(position, _cooldownJumpDuration));
            }
        }

        public void JumpBack(string animationName)
        {
            _cooldownJumpBackwardDuration = FindAnim(animationName);

            StartCoroutine(CheckIfEnemy()
                ? JumpAnim(_startInitJumpBackward, _cooldownJumpBackwardDuration)
                : JumpAnim(_startInitJump, _cooldownJumpBackwardDuration));
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

        private IEnumerator JumpAnim(Vector3 position, float duration)
        {
            transform.DOMoveX(position.x + _offsetX, duration);
            transform.DOMoveY(position.y + 100, duration/2);
            yield return new WaitForSeconds(duration/2);
            transform.DOMoveY(position.y, duration/2);
        }
    }
}

