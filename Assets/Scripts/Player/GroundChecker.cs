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
        
        [SerializeField]  private LayerMask _walkableMask;
        private float _detectionRadius = 0.15f;
        
      


        private void Update()
        {
            var isGrounded = Physics.CheckSphere(groundCheck.position, _detectionRadius, _walkableMask);
            var yVelocity = _characterController.velocity.y;
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