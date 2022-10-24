using System;
using UnityEngine;

namespace ParalaxBackground
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ParalaxEffect : MonoBehaviour
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private float _paralaxPower;
        private float len, startPos;
        
        private void Start()
        {
            len = GetComponent<SpriteRenderer>().bounds.size.x;
            startPos = transform.position.x;
        }

        private void FixedUpdate()
        {
            var temp = _camera.position.x * (1-_paralaxPower);
            var dist = _camera.position.x * _paralaxPower;
            var pos = transform.position;
            pos.x = startPos + dist;
            transform.position = pos;
            // repetition of background
           
            if (temp > startPos + len)
            {
                startPos += len;
            }
            else if (temp<startPos-len)
            {
                startPos -= len;
            }
           

        }
    }
}