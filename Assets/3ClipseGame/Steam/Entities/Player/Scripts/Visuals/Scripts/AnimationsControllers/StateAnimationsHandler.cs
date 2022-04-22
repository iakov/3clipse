using System.Runtime.CompilerServices;
using Assets._3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates;
using UnityEngine;

namespace Assets._3ClipseGame.Steam.Entities.Player.Scripts.Visuals.Scripts.AnimationsControllers
{
    public abstract class StateAnimationsHandler
    {
        protected StateAnimationsHandler(Animator animator) => Animator = animator;
        protected Animator Animator;
        public abstract void OnStateEnter();
        public abstract void ApplyLeavingSubState(SubState leavingSubState);
        public abstract void ApplyEnteringSubState(SubState enteringSubState);
        public abstract void OnStateExit();
    }
}