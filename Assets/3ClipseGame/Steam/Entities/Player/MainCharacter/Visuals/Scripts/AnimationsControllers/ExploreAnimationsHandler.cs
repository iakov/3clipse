using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.SubStates;
using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.SubStates.ExploreSubStates;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.Visuals.Scripts.AnimationsControllers
{
    public class ExploreAnimationsHandler : StateAnimationsHandler
    {
        #region Initialization

        public ExploreAnimationsHandler(Animator animator) : base(animator) {}
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int IsCrouching = Animator.StringToHash("IsCrouching");
        private static readonly int IsFalling = Animator.StringToHash("IsFalling");
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");

        #endregion

        #region StateAnimationsHandlerMethods 

        public override void OnStateEnter()
        {
            
        }

        public override void OnStateExit()
        {
            
        }
        
        public override void ApplyLeavingSubState(MainCharacterSubState currentMainCharacterSubState)
        {
            if(currentMainCharacterSubState.GetType() == typeof(ExploreRunSubState)) Animator.SetBool(IsRunning, false);
            if(currentMainCharacterSubState.GetType() == typeof(ExploreJumpSubState)) Animator.SetBool(IsJumping, false);
        }

        public override void ApplyEnteringSubState(MainCharacterSubState newMainCharacterSubState)
        {
            if (newMainCharacterSubState.GetType() == typeof(ExploreIdleSubState))
            {
                Animator.SetBool(IsWalking, false);
                Animator.SetBool(IsRunning, false);
                Animator.SetBool(IsCrouching, false);
                Animator.SetBool(IsFalling, false);
            }
            else if(newMainCharacterSubState.GetType() == typeof(ExploreWalkSubState)) Animator.SetBool(IsWalking, true);
            else if (newMainCharacterSubState.GetType() == typeof(ExploreRunSubState)) Animator.SetBool(IsRunning, true);
            else if(newMainCharacterSubState.GetType() == typeof(ExploreCrouchSubState)) Animator.SetBool(IsCrouching, true);
            else if(newMainCharacterSubState.GetType() == typeof(ExploreFallSubState)) Animator.SetBool(IsFalling, true);
            else if (newMainCharacterSubState.GetType() == typeof(ExploreJumpSubState)) Animator.SetTrigger(IsJumping);
            
        }

        #endregion
    }
}