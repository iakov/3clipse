using _3ClipseGame.Steam.Entities.Player.Scripts.GlobalScripts;
using Assets._3ClipseGame.Steam.Entities.Player.Scripts.GlobalScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.Visuals.Scripts
{
    public class FreezeInput : StateMachineBehaviour
    {
        #region StateMachineBehaviourMethods

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            PlayerMover.IsFreezed = true;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) =>
            PlayerMover.IsFreezed = false;

        #endregion
    }
}
