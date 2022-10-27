
using Player.AnimationRelated;
using Player.Dying;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{

    public interface IRespawnable
    {
        void SetSpawnPosition(Vector3 pos);
    }

    public class PlayerManager : MonoBehaviour, IRespawnable
    {

        [SerializeField] private PlayerController _playerController;
        [SerializeField] private PlayerAnimationHander _animationHander;
        [SerializeField] private CharacterController _characterController;
        public Vector3 SpawnPos;
        
        
        public void OnPlayerDeath(DeathType deathType)
        {
           gameObject.SetActive(false);
           transform.position = SpawnPos;
           gameObject.SetActive(true);
          
        

        }

        public void SetSpawnPosition(Vector3 pos)
        {
            SpawnPos = pos;
        }
    }
}

