using UnityEngine;

namespace Player
{
    public class PushableGroundChecker : MonoBehaviour
    {
        [SerializeField] private Transform [] _rayAnchors;
        [SerializeField] private LayerMask _standOnMasks;
        public bool IsGrounded { get; set; } = false;



        /*private void CheckGrounded()
        {
            int cnt = 0;
            for (int i = 0; i < _rayAnchors.Length; i++)
            {
                bool grounded = Physics.CheckSphere(_rayAnchors[i].position, _detectionRadius, _finalLayer);
                if (grounded)
                {
                    cnt++;
                }
            }

            if (cnt==0)
            { 
                IsGrounded = false;
            }
        }*/
    }
}