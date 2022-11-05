using System;
using Player;
using UnityEngine;

namespace LevelManagement
{
    [RequireComponent(typeof(Collider))]
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField]  private Vector3 _position;

        private void Awake()
        {
            _position = transform.position;
            _renderer.enabled = false;
        }


        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<IRespawnable>().SetSpawnPosition(_position);
            Debug.Log("CheckPointSet");
        }
    }
}