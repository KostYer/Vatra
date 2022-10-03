using System.Collections;
using Player.AnimationRelated;
using UnityEngine;

namespace Player
{
    public class LedgeDetector : MonoBehaviour
    {
         [Header("Raycasts")]
        [SerializeField] private LayerMask _layer;
        [SerializeField] private Transform _raycastAnchor;
        [SerializeField] private Vector2 _rayVerticalOffset;
        [SerializeField] private float _rayLen;
        [Space] [SerializeField] private CharacterController _characterController;

        [SerializeField] private Animator _animator;
       // [SerializeField] private AnimationHandler _animationHandler;
     //   [SerializeField] private GroundChecker _groundChecker;

     

        private Vector3 _platformEdge;

  //      [SerializeField] private AnimationEvents _animationEvents;
        [SerializeField] private Transform _hips;
        [SerializeField] private Vector2 _endGrabOffset;
        [SerializeField] private GroundChecker _groundChecker;

        [SerializeField] private PlayerController _playerController;

        [SerializeField] private AnimationEventsSO _animationEvents;
        private void Start()
        {
            _animationEvents.OnLedgePullStarted += OnLedgePullStarted;
        }

        private void OnLedgePullStarted(float clipLen)
        {
            var yRise = 2.65f;

            StartCoroutine(MoveUp(yRise, clipLen));


        }


        private IEnumerator MoveUp(float yOffset, float duration)
        {
            var startPos = transform.position;
            var desiredPos = startPos + new Vector3(0f, yOffset, 0f);
            float time = 0;
            while (time < duration)
            {
                float value = time / duration;
                time += Time.deltaTime;
                transform.position = Vector3.Lerp(startPos, desiredPos, value);
                yield return null;
            }

            var horOffset = 0.63f;

            var currentlyFacing = _groundChecker.GetCurrentlyFacing();
            transform.position += new Vector3(horOffset * currentlyFacing, 0f, 0f);
             _playerController.Enable(true);
        }
       

        private void Update()
        {

            bool isGrounded = _groundChecker.IsGraunded;
            if (isGrounded)
            {
                return;
            }

            TryDetectLedge();
        }


        private void TryDetectLedge()
        {
          
          
            if (Physics.Raycast(_raycastAnchor.position  ,
                    Vector3.down, out var hit, _rayLen, _layer))
            {
                /*_rb.useGravity = false;
                _rb.velocity = Vector3.zero;*/
       //         _animationHandler.HungOnEdge();
       _playerController.Enable(false);
        _animator.SetTrigger("ClimbTrig");
        
      
            }
            else
            {
                _animator.ResetTrigger("ClimbTrig");

            }


        }

        

        private void OnDrawGizmos()
        {
            
           Gizmos.color = Color.blue;
           Gizmos.DrawSphere(_raycastAnchor.position  , 0.2f);
           Gizmos.DrawRay(_raycastAnchor.position  , Vector3.down * _rayLen);
        }
    }
}