using _3ClipseGame.Steam.Core.GameStates.Scripts;
using _3ClipseGame.Steam.Global.Scripts.GameScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.Visuals.Animations.Scripts
{
    public class FreezeInput : StateMachineBehaviour
    {
        #region StateMachineBehaviourMethods

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Game.Instance.CinematicMode.StartEnable();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) =>
            Game.Instance.PlayMode.StartEnable();

        #endregion
    }
}
