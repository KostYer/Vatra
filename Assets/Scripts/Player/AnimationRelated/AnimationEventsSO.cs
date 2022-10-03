using System;
using UnityEngine;

namespace Player.AnimationRelated
{
    [CreateAssetMenu(fileName = "AnimationEvents", menuName = "SO/AnimationEvents")]
    public class AnimationEventsSO : ScriptableObject
    {
        public event Action<float> OnLedgePullStarted;



        public void NotifyLedgePullStarted(float duration)
        {
            OnLedgePullStarted?.Invoke(duration);
        }
    }
}

 