using _3ClipseGame.Steam.Entities.Player.Scripts.GlobalScripts;
using UnityEngine;

namespace Assets._3ClipseGame.Steam.Entities.Player.Scripts.Visuals.Scripts
{
    public class FreezeInput : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            PlayerMover.IsFreezed = true;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) =>
            PlayerMover.IsFreezed = false;
    }
}
