using System;
using System.Collections;
using Player.AnimationRelated;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PushingHandler : MonoBehaviour
    {
        [SerializeField] private PushablesDetector _pushDetector;
        [SerializeField] private PlayerAnimationHander _animationHander;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Button _bushButton;

       
        
        private Rigidbody _pushedObjectRb;
        private Vector3 _pushingDirection = Vector3.zero;
        private bool _isPushing = false;
        private bool _pushingCoroutine = false;
        [SerializeField]  private float _pushForce = 1;
        private void Start()
        {
            _pushDetector.OnPushableDetected += OnPushDetected;
            _pushDetector.OnDetectionExit += OnDetectionExit;
            ShowUiButton(false);
            _bushButton.onClick.AddListener(OnPushStart);
        }


        private void ShowUiButton(bool show)
        {
            _bushButton.gameObject.SetActive(show);
        }

        private Rigidbody _rb;
        private void OnPushDetected(Rigidbody rb)
        {
            _rb = rb;
            ShowUiButton(true);

        }

        private void OnPushStart()
        {
            Debug.Log("obj pushing");
            _rb.GetComponent<PushableObject>().OnFall += OnDetectionExit;
            _animationHander.PlayPushing(true);
            _isPushing = true;
            
            
            var dir = (_rb.position - transform.position).normalized;
            _pushedObjectRb = _rb;
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


        private void FixedUpdate()
        {
            
        }
    }
}