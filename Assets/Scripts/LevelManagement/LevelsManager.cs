using GameCore;
using Level;
using LevelUI;
using Player;
using Prefabs;
using UnityEngine;

namespace LevelManagement
{
    public class LevelsManager: MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;      
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private LevelUiHandler _levelUi;
        [SerializeField] private LevelView _levelView;
        private IPrefabsProvider _prefabsProvider;
        private IPrefabsSpawner _prefabsSpawner;
        
     

        private void Start()
        {
            _prefabsProvider = GameContext.GetInstance<IPrefabsProvider>();
            _prefabsSpawner = GameContext.GetInstance<IPrefabsSpawner>();
            _levelUi.OnRestartRequested += OnRestartRequested;
            SpawnPlayer();


        }

        private void OnRestartRequested()
        {
            SpawnPlayer();
            Time.timeScale = 1f;
            _cameraController.ResetCamera();
        }


        public void SpawnPlayer()
        {
            var spawnPoint = _spawnPoint.position;
            if (_levelView.Player != null)
            {
                spawnPoint = _levelView.Player.SpawnPos;
                Destroy(_levelView.Player.gameObject);
            }

            var playerObj = _prefabsSpawner.SpawnAtPoint(_prefabsProvider.GetGamePrefab(PrefabType.Player),
                spawnPoint, this.transform);
            var player =  playerObj.GetComponent<PlayerManager>();
            _levelView.Player = player;
            _cameraController.Init(player.transform);
        }
    }
}