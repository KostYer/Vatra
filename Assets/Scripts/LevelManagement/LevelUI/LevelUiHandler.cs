using System;
using GameCore;
using UnityEngine;
using UnityEngine.UI;

namespace LevelUI
{
    public class LevelUiHandler : MonoBehaviour, IPlayerDied
    {
        [SerializeField] private RectTransform _restartPanel;
        [SerializeField] private Button _restartButton;
        public event Action OnRestartRequested;
        
        private ISignalBus _signalBus;

        private void Start()
        {
            _signalBus = GameContext.GetInstance<ISignalBus>();
            _signalBus.Subscribe<IPlayerDied>(this);
            _restartButton.onClick.AddListener(OnRestartClicked);
            _restartPanel.gameObject.SetActive(false);
        }

        private void OnRestartClicked()
        {
            OnRestartRequested?.Invoke();
            _restartPanel.gameObject.SetActive(false);
        }


        private void OnDestroy()
        {
            _restartButton.onClick.RemoveAllListeners();
        }

        public void OnPlayerDied()
        {
            _restartPanel.gameObject.SetActive(true);
        }
    }
}