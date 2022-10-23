using System.Collections;
using UnityEngine;

namespace Player
{
    public class PushableObjectRollable : PushableObject
    {
        [SerializeField] protected float _rotSpeed = 160f;
        private float _rotation;
        public override void EnableInteraction(bool enable)
        {
            base.EnableInteraction(enable);
            StartCoroutine(RollingCor());

        }

        public override void GetMovementDir(float _dirX)
        {
            base.GetMovementDir(_dirX);
            var mul = _dirX;
            if (_dirX > 0)
            {
                mul = 1f;
            }
            else if (_dirX < 0)
            {
                mul = -1;
            }

            _rotation = _rotSpeed * mul;
        }

        private IEnumerator RollingCor()
        {
            Debug.Log("rolling start");
            while (_isActive)
            {   Debug.Log("rolling _isActive");
                _model.Rotate(0f, 0f, _rotation * Time.deltaTime);
                yield return null;
            }
        }
    }
}