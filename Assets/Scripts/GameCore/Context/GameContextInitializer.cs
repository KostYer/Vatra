using System;
using UnityEngine;

namespace GameCore
{
    public class GameContextInitializer : MonoBehaviour
    {
        
        
        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            SignalBus eventBus = new SignalBus();
            GameContext.Register<ISignalBus>(eventBus);
        }
    }
}