using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class GroundChecker : MonoBehaviour
    {
        [FormerlySerializedAs("IsGrainded")] public bool IsGraunded = true;
        public Transform groundCheck;

        [SerializeField] private CharacterController _characterController;
        
        private int _finalLayer;
        private float _detectionRadius = 0.15f;
        
        private void Start()
        {
            int _groundLayer = LayerMask.NameToLayer("ground");
            int _platformLayer = LayerMask.NameToLayer("platform");
            int _pushable = LayerMask.NameToLayer("pusshable");
            
            int _ground = 1<< _groundLayer;
            int _platform = 1<< _platformLayer;
            int pushable = 1 << _pushable;
            _finalLayer  = _ground | _platform | pushable;
        }


        private void Update()
        {
            var isGrounded = Physics.CheckSphere(groundCheck.position, _detectionRadius, _finalLayer);
            var yVelocity = _characterController.velocity.y;
//            Debug.Log("yVelocity" + yVelocity);
            IsGraunded = isGrounded;// && yVelocity > 0.04f;
        }


        [SerializeField] private Transform _model;
        
    
        public float GetCurrentlyFacing()
        {
            var dot = Vector3.Dot(_model.forward, Vector3.right);
            if (dot >= 0)
            {
                dot = 1;
            }
            else if (dot<0)
            {
                dot = -1;
            }
 
            return dot;
        }
    }
}