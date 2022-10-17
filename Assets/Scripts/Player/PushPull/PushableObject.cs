using System;
using UnityEngine;

namespace Player
{


    public class PushableObject : MonoBehaviour
    {
        [SerializeField] private Transform [] _rayAnchors ;
        [SerializeField] private Rigidbody _rb;
        public event Action OnFall;
        
        private int _finalLayer;
        private Vector3 _fallVector = new Vector3(0f, -4, 0f);

        private float _detectionRadius = 0.2f;
        private void Start()
        {
            int _groundLayer = LayerMask.NameToLayer("ground");
            int _platformLayer = LayerMask.NameToLayer("platform");
            
            int _ground = 1<< _groundLayer;
            int _platform = 1<< _platformLayer;
            _finalLayer  = _ground | _platform;
        }

        private void Update()
        {
            CheckGrounder();
        }


        private void CheckGrounder()
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
                OnFalling();
            }
        }

        private void OnFalling()
        { 
            Debug.Log("OnFalling");
            OnFall?.Invoke();
            _rb.velocity = Vector3.zero;
            _rb.AddForce(_fallVector, ForceMode.Impulse);
        }

    }
}
