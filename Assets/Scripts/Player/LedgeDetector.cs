using System.Collections;
using Player.AnimationRelated;
using UnityEngine;

namespace Player
{
    public class LedgeDetector : MonoBehaviour
    {
        [Header("Raycasts")] [SerializeField] private LayerMask _layer;
        [SerializeField] private Transform _raycastAnchor;
        [SerializeField] private float _rayLen;
        [Tooltip("offset for edge detection")] private Vector2 _detectionOffset = new Vector2(-1.5f, -0.2f);
        

        private bool _movedX = false;
        private Vector3 _platformEdge;


        [SerializeField] private GroundChecker _groundChecker;

        [SerializeField] private PlayerController _playerController;

        [SerializeField] private AnimationEventsSO _animationEvents;
        [SerializeField] private PlayerAnimationHander _animationHander;

        [Tooltip("offset that occures snaps player to edge raycast hit point before any movement")]
        private Vector2 _edgeSnatOffset = new Vector2(-0.12f, -1.65f);

        [Tooltip("offset that occures during movement along the edge")]
        private Vector2 _edgeMovement = new Vector2(0.55f, 1.6f);

        private bool _isEdgeResult = false;
        private Vector3 _resultEdge = Vector3.zero;

        private void Start()
        {
            _animationEvents.OnLedgePullStarted += OnLedgePullStarted;
        }

        private void OnLedgePullStarted(float clipLen)
        {
            var yRise = _edgeMovement.y;

            StartCoroutine(MoveUp(yRise, clipLen));
        }


        private IEnumerator MoveUp(float yOffset, float duration)
        {
            var startPos = transform.position;
            var desiredPos = startPos + new Vector3(0f, yOffset, 0f);
            var time = 0f;

            var horOffset = _edgeMovement.x;
            var currentlyFacing = _groundChecker.GetCurrentlyFacing();
            while (time <= duration)
            {
                yield return null;
                time += Time.deltaTime;
                float value = time / duration;
                transform.position = Vector3.Lerp(startPos, desiredPos, value);
            }

            transform.position += new Vector3(horOffset * currentlyFacing, 0f, 0f);
            _playerController.Enable(true);
        }


        private void Update()
        {
            bool isGrounded = _groundChecker.IsGraunded;
            if (isGrounded)
            {
                _movedX = false;
                return;
            }

            TryDetectLedge();
        }


        private void TryDetectLedge()
        {
            if (Physics.Raycast(_raycastAnchor.position,
                    Vector3.down, out var hit, _rayLen, _layer))
            {
                _resultEdge = GetEdge(hit.point);
                _playerController.Enable(false);

                if (!_movedX)
                {
                    var faceDir = _groundChecker.GetCurrentlyFacing();
                    var offset = new Vector3(_edgeSnatOffset.x * faceDir, _edgeSnatOffset.y, 0f);
                    transform.position = _resultEdge + offset;
                    _animationHander.TriggerClimbing();
                    _movedX = true;
                }
            }
        }


        private Vector3 GetEdge(Vector3 verticalHit)
        {
            var result = Vector3.zero;
            var faceDir = _groundChecker.GetCurrentlyFacing();
            var rayOffsetx = _detectionOffset.x * faceDir;
            var rayOffsety = _detectionOffset.y;
            var origin = new Vector3(verticalHit.x + rayOffsetx, verticalHit.y + rayOffsety, verticalHit.z);
            if (Physics.Raycast(origin, Vector3.right * faceDir, out var hit, 5, _layer))
            {
                result = hit.point;
                _isEdgeResult = true;
            }

            return result;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(_raycastAnchor.position, 0.2f);
            Gizmos.DrawRay(_raycastAnchor.position, Vector3.down * _rayLen);

            if (_isEdgeResult)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(_resultEdge, 0.2f);
            }
        }
    }
}