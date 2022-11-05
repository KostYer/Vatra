using System;
using GameCore;
using UnityEngine;

namespace Player.Dying
{
    [RequireComponent(typeof(BoxCollider))]
    public class DeathZone : MonoBehaviour
    {
        [SerializeField] private BoxCollider _collider;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private DeathType _deathType;
        private ISignalBus _signalBus;
        private void Awake()
        {
            _renderer.enabled = false;
            _signalBus = GameContext.GetInstance<ISignalBus>();
        }


        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<PlayerManager>().OnPlayerDeath(_deathType);
            _signalBus.Emit<IPlayerDied>(s=>s.OnPlayerDied());
            Debug.Log("player fall death zone");
        
        }
    }
}