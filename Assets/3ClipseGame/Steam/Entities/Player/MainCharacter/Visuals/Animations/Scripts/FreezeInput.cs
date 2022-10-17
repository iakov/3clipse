using _3ClipseGame.Steam.Core.GameSource;
using _3ClipseGame.Steam.Core.GameSource.Parts.States;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.Visuals.Animations.Scripts
{
    public class FreezeInput : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
            => GameSource.Instance.GetStatesManager().Enable(GameStateTypes.Cinematic);

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
            => GameSource.Instance.GetStatesManager().Enable(GameStateTypes.PlayMode);
    }
}
