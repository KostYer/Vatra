
using Player.AnimationRelated;
using Player.Dying;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {

        [SerializeField] private PlayerController _playerController;
        [SerializeField] private PlayerAnimationHander _animationHander;

        public void OnPlayerDeath(DeathType deathType)
        {
      //      _playerController.Enable(false);
         //   _animationHander.PlayDeath(deathType);
        }

    }
}

