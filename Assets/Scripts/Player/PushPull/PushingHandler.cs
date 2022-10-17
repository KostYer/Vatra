using System;
using System.Collections;
using Player.AnimationRelated;
using UnityEngine;

namespace Player
{
    public class PushingHandler : MonoBehaviour
    {
        [SerializeField] private PushablesDetector _pushDetector;
        [SerializeField] private PlayerAnimationHander _animationHander;
        [SerializeField] private CharacterController _characterController;
        private Rigidbody _pushedObjectRb;
        private Vector3 _pushingDirection = Vector3.zero;
        private bool _isPushing = false;
        private bool _pushingCoroutine = false;
        [SerializeField]  private float _pushForce = 1;
        private void Start()
        {
            _pushDetector.OnPushableDetected += OnPushDetected;
            _pushDetector.OnDetectionExit += OnDetectionExit;
        }

        private void OnPushDetected(Rigidbody rb)
        {
            Debug.Log("obj pushing");
            rb.GetComponent<PushableObject>().OnFall += OnDetectionExit;
            
            _animationHander.PlayPushing(true);
            _isPushing = true;
            
            
            var dir = (rb.position - transform.position).normalized;
            _pushedObjectRb = rb;
            _pushingDirection = dir;
            Pushing();


        }

        private void OnDetectionExit()
        {
            // _pushedObjectRb.GetComponent<PushableObject>().OnFall -= OnDetectionExit;
            OnPushingEnd();
        }

        private void OnPushingEnd()
        {
            _animationHander.PlayPushing(false);
            _isPushing = false;
            _pushingCoroutine = false;
            _pushedObjectRb = null;
           
        }

        private void Pushing()
        {
            if (_pushingCoroutine)
            {
                return;
            }

          
            StartCoroutine(PushingCor());
           _pushingCoroutine = true;
        }

        private IEnumerator PushingCor()
        {   
            while (_isPushing)
            {
                Debug.Log("push coroutine");
              //  var velocity = _characterController.velocity;
              _pushedObjectRb.velocity = new Vector3(_characterController.velocity.x+0.1f, 0f, 0f);
              /*if (_pushedObjectRb.velocity.y < 0f)
              {
                  _isPushing = false;
              }*/

              //   _pushedObjectRb.AddRelativeForce (_pushingDirection * _pushForce - _pushedObjectRb.velocity, ForceMode.Force);
               // _pushedObjectRb.AddForce(_pushingDirection * _pushForce, ForceMode.Acceleration);
               yield return new WaitForFixedUpdate();
            }
        }
    }
}