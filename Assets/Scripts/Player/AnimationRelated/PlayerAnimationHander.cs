using System;
using UnityEngine;

namespace Player.AnimationRelated
{
    public class PlayerAnimationHander : MonoBehaviour
    {
        [SerializeField] private Animator _animator;


        // animation IDs
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;
        private int _climbIDtrigger;
        private int _pushID;
        private int _pullID;

        private void Awake()
        {
            AssignAnimationIDs();
        }
        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
            _climbIDtrigger = Animator.StringToHash("ClimbTrig");
            _pushID = Animator.StringToHash("Pushing");
            _pullID = Animator.StringToHash("Pulling");
        }

        public void SetGrounded(bool grounded)
        {
            _animator.SetBool(_animIDGrounded, grounded);
        }


        public void SetBlendSpeed(float blend)
        {
            _animator.SetFloat(_animIDSpeed, blend);
        }

        public void SetMotionSpeed(float speed)
        {
            _animator.SetFloat(_animIDMotionSpeed, speed);
        }

        public void SetJump(bool jump)
        {
            _animator.SetBool(_animIDJump, jump);
        }

        public void SetFreeFall(bool fall)
        {
            _animator.SetBool(_animIDFreeFall, fall);
        }


    


        public void TriggerClimbing()
        {
            _animator.SetTrigger(_climbIDtrigger);
        }

        public void PlayPushing(bool value)
        {
            _animator.SetBool(_pushID,value);
        }
        public void PlayPulling(bool value)
        {
            _animator.SetBool(_pullID,value);
        }


        [ContextMenu("pushing anim test")]
        private void PushAnimPlayTest()
        {
            _animator.SetBool(_pushID,true);
        }
    }
}