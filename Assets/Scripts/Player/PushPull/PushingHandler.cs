using System;
using Player.AnimationRelated;
using UnityEngine;

namespace Player
{
    public class PushingHandler : MonoBehaviour
    {
        [SerializeField] private PushablesDetector _pushDetector;
        [SerializeField] private PlayerAnimationHander _animationHander;

        private Rigidbody _pushedObjectRb;

        private void Start()
        {
            _pushDetector.OnPushableDetected += OnPushDetected;
            _pushDetector.OnDetectionExit += OnDetectionExit;
        }

        private void OnPushDetected(Rigidbody rb)
        {
            Debug.Log("obj pushing");
            _animationHander.PlayPushing(true);

        }

        private void OnDetectionExit()
        {
            _animationHander.PlayPushing(true);
        }
    }
}