using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.Visuals.Scripts
{
    public class ActivateFallOnEntrance : StateMachineBehaviour
    {
        #region Initialization

        private static readonly int IsFalling = Animator.StringToHash("IsFalling");

        #endregion

        #region StateMachineBehaviourMethods

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => animator.SetBool(IsFalling, true);        

        #endregion
        
    }
}
