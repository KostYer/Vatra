
using System;
using Prefabs;
using UnityEngine;

namespace GameCore
{
    public class GameContextInitializer : MonoBehaviour
    {
        [SerializeField] private PrefabsProviderSO _prefabsProvider;
        private SignalBus _signalBus;
        private PrefabsSpawner _prefabsSpawner;
        private void Awake()
        {
            Initialize();
            
        }

        private void Start()
        {
            _signalBus.Emit<ILevelStart>(s=>s.OnLevelStart());
        }

        private void Initialize()
        {
            _signalBus  = new SignalBus();
            _prefabsSpawner = new PrefabsSpawner();
            
            
            GameContext.Register<ISignalBus>(_signalBus);
            GameContext.Register<IPrefabsProvider>(_prefabsProvider);
            GameContext.Register<IPrefabsSpawner>(_prefabsSpawner);
        }
    }
}