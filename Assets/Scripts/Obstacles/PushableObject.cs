using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PushableObject : MonoBehaviour
    {
        [SerializeField] protected PushableType _pushableType;
        [SerializeField] protected Rigidbody _rb;
        [SerializeField] protected Transform[] _groundCheckAnchors;
        [SerializeField] protected Transform _model;
        [SerializeField] protected LayerMask _groundLayers;
        public PushableType PushableType => _pushableType;
      
        protected Vector3 _fallVector = new Vector3(0f, -4, 0f);

        protected float _detectionRadius = 0.5f;
        [SerializeField]  protected bool _isActive;

        public event Action OnFall;

        public virtual void EnableInteraction(bool enable)
        {
            _isActive = enable;
            if (enable)
            {
                StartCoroutine(GroundChecker());
            }
            else
            {
                _rb.isKinematic = false;
            }

            
        }


        public virtual void GetMovementDir(float dirX)
        {
            
        }

        protected IEnumerator GroundChecker()
        {  
            var contactPoints = _groundCheckAnchors.Length;
            while (_isActive)
            {
                  contactPoints = _groundCheckAnchors.Length;
                for (int i = 0; i < _groundCheckAnchors.Length; i++)
                {
                    var anchor = _groundCheckAnchors[i];
                    var isGroundFound = Physics.Raycast(anchor.position, Vector3.down, _detectionRadius, _groundLayers);
                    if (!isGroundFound)
                    {
                        contactPoints--;
                    }
                }

                if (contactPoints==0)
                {
                    Debug.Log("contactPoints "+contactPoints);
                    OnFalling();
                }

                yield return null;
            }
        }

        protected void OnFalling()
        { 
            Debug.Log("OnFalling");
            _rb.isKinematic = false;
            _isActive = false;
            OnFall?.Invoke();
            _rb.velocity = Vector3.zero;
            _rb.AddForce(_fallVector, ForceMode.Impulse);
          
            
        }


        [ContextMenu("RaycastDebug")]
        protected void RaycastDebug()
        {
            var contactPoints = _groundCheckAnchors.Length;
            for (int i = 0; i < _groundCheckAnchors.Length; i++)
            {
                var anchor = _groundCheckAnchors[i];
                var isGroundFound = Physics.Raycast(anchor.position, Vector3.down, _detectionRadius, _groundLayers);
                if (!isGroundFound)
                {
                    contactPoints--;
                }
            }
            
            Debug.Log("contactPoints="+contactPoints);
        }

    }


    public enum PushableType
    {
        Pushable,
        Rollable
    }
}
