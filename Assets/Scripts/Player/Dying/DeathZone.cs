using System;
using UnityEngine;

namespace Player.Dying
{
    [RequireComponent(typeof(BoxCollider))]
    public class DeathZone : MonoBehaviour
    {
        [SerializeField] private BoxCollider _collider;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private DeathType _deathType;
        private void Awake()
        {
            _renderer.enabled = false;
        }


        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<PlayerManager>().OnPlayerDeath(_deathType);
            Debug.Log("player fall death zone");
        
        }
    }
}