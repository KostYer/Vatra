using UnityEngine;

namespace Player.AnimationRelated
{
    public class LedgePullAnimState : StateMachineBehaviour
    {
    [SerializeField]  private AnimationEventsSO _animationEvents;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var clipDuration = stateInfo.length;
            _animationEvents.NotifyLedgePullStarted(clipDuration);
       
        }
    }
}