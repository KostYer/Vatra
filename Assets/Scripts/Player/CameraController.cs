using System;
using GameCore;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]private float smoothSpeed = 0.25f;
    [SerializeField] private Vector3 _initialOffset;
    private Transform _target;
    private Vector3 _offset;
    private bool _isActive;
     
    public void Init(Transform player)
    {
        _isActive = false;
        _target = player;
      //  _offset = transform.position - _target.position;
        ResetCamera();

    }

    public void ResetCamera()
    {
        transform.position = _target.position + _initialOffset;
        _isActive = true;
    }

    void LateUpdate()
    {
        if (!_isActive)
        {
            return;
        }
        _offset = transform.position - _target.position;
        var desiredPosition = _target.position + _offset;
        transform.position = _target.position + _initialOffset;
       transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }

     

    
}
