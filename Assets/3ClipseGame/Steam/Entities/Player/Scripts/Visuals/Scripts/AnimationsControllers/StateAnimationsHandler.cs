using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.Visuals.Scripts.AnimationsControllers
{
    public abstract class StateAnimationsHandler
    {
        #region Initialization

        protected StateAnimationsHandler(Animator animator) => Animator = animator;
        protected Animator Animator;

        #endregion

        #region AbstractMethods

        public abstract void OnStateEnter();
        public abstract void ApplyLeavingSubState(SubState leavingSubState);
        public abstract void ApplyEnteringSubState(SubState enteringSubState);
        public abstract void OnStateExit();

        #endregion
    }
}