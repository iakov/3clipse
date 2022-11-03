using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.Visuals.Animations.Scripts
{
    public class ActivateFallOnEntrance : StateMachineBehaviour
    {
        private static readonly int IsFalling = Animator.StringToHash("IsFalling");

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => animator.SetBool(IsFalling, true);
    }
}
