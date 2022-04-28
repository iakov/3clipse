using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.SubStates;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.Visuals.Scripts.AnimationsControllers
{
    public abstract class StateAnimationsHandler
    {
        #region Initialization

        protected StateAnimationsHandler(Animator animator) => Animator = animator;
        protected Animator Animator;

        #endregion

        #region AbstractMethods

        public abstract void OnStateEnter();
        public abstract void ApplyLeavingSubState(MainCharacterSubState leavingMainCharacterSubState);
        public abstract void ApplyEnteringSubState(MainCharacterSubState enteringMainCharacterSubState);
        public abstract void OnStateExit();

        #endregion
    }
}