
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
        [SerializeField] private ButtonPushable _UiPushableButton;

        [SerializeField] private PlayerController _playerController;
        [SerializeField] private float _velocityX;   
        
        private Rigidbody _pushedObjectRb;
    
        private bool _isPushing = false;
        private Transform _playerModel;
 
        [SerializeField]  private float _pushForce = 1;
        private PushableObject _pushable;
        private void Start()
        {
            _pushDetector.OnPushableDetected += OnPushDetected;
            _pushDetector.OnDetectionExit    += OnPushDetectedEnd;
            ShowUiButton(false);
            _UiPushableButton.OnButtonPressed += OnButtonDown;
            _UiPushableButton.OnButtonUnpressed += OnButtonUnpressed;
            _playerModel = _playerController.GetPlayerModel();
        }

        private void OnButtonDown()
        {
            
            OnPushStart();
        }

        private void OnButtonUnpressed()
        {
            
            OnPushingEnd();
        }

        private Rigidbody _rb;

        private void OnPushDetected(Rigidbody rb)
        {
            _rb = rb;
            ShowUiButton(true);

        }   
        private void OnPushDetectedEnd()
        {
            ShowUiButton(false);
         
        }
        
        private void ShowUiButton(bool show)
        {
           _UiPushableButton.Show(show);
        }
  
        private void OnPushStart()
        {
            Debug.Log("OnPushStart");
            
            
            
            _pushable = _rb.GetComponent<PushableObject>();

            var distToPushable = Vector3.Distance(_pushDetector.transform.position, _pushable.transform.position);
            Debug.Log("distToPushable " + distToPushable);
            
            _pushable.EnableInteraction(true);
            _pushable.OnFall += OnPushDetectedEnd;
            
            
            _isPushing = true;
            
            
            var dir = (_rb.position - transform.position).normalized;
            _pushedObjectRb = _rb;
          
            Pushing();
            StartCoroutine(HandleMovementDirectionChange());
            StartCoroutine(HandleFreezAnimation());

        }
 
 
        private void Pushing()
        {
          
 
          _pushedObjectRb.isKinematic = true;
          _pushedObjectRb.transform.SetParent(_playerModel);
          _playerController.PreventModelRot = true;
        }
       
        
        private void OnPushingEnd()
        {
            
            _isPushing = false;
            _pushable.EnableInteraction(false);
            _pushable.OnFall -= OnPushDetectedEnd;

           
         
            _pushedObjectRb.transform.SetParent(null);
            _pushedObjectRb = null;
            _playerController.ResetModelRotation();
              _playerController.PreventHorizontalMovement(false);
            _pushable = null;
            _animationHander.PlayPulling(false);
            _animationHander.PlayPushing(false);
            _animationHander.FreezePushPull(false);
        }


      

        private IEnumerator HandleMovementDirectionChange()
        {
            while (_isPushing)
            {
               
                var dot = GetDotToPushable();
                if (dot>0)
                {
                    _animationHander.PlayPulling(true);
                    _animationHander.PlayPushing(false);
                }
                else if (dot<0)
                {
                    _animationHander.PlayPulling(false);
                    _animationHander.PlayPushing(true);
                }

                if (_pushable.PushableType == PushableType.Rollable)
                {
                    _pushable.GetMovementDir(_playerController.VelocityX);
                    
                 
                    if ( dot>= 0)
                    {
                        _playerController.PreventHorizontalMovement(true);
                    }
                    else
                    {  _playerController.PreventHorizontalMovement(false);
                    }
                }

                yield return null;
            }
            
        }


        private IEnumerator HandleFreezAnimation()
        {
            while (_isPushing)
            {
                var horVelocity =   _characterController.velocity.x;
                bool freze = horVelocity == 0;

                _animationHander.FreezePushPull(freze);
                yield return null;
            }
        }


        private int GetDotToPushable()
        {
            var dir = Vector3.zero;
            if (_pushable == null)
            {
                dir = Vector3.zero;
            }

            else
            {
                dir = transform.position - _pushable.transform.position;
            }

            var dot = Vector3.Dot(transform.right, dir);
            dot = dot >= 0 ? 1 : -1;
            return (int)dot;
        }
    }
}