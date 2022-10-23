using System;
using UnityEngine;

namespace Player
{
    public class PushablesDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask _pushableLayer;
        public Action<Rigidbody> OnPushableDetected;
        public Action OnDetectionExit;

        private void OnTriggerEnter(Collider other)
        {
            var rb = other.GetComponent<Rigidbody>();
            OnPushableDetected?.Invoke(rb);
            
        }
        
        private void OnTriggerExit(Collider other)
        {
            OnDetectionExit?.Invoke();
            
        }
    }
}