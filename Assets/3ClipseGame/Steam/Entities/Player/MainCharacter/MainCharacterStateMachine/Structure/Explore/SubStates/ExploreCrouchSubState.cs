using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.Explore.SubStates
{
    public class ExploreCrouchSubState : MainCharacterSubState
    {
        #region Initialization

        public ExploreCrouchSubState(MainCharacterStateMachine context, MainCharacterSubStateFactory factory) : base(context, factory) =>
            _factory = (ExploreSubStatesFactory) factory;
        
        private ExploreSubStatesFactory _factory;

        #endregion

        #region SubStateMethods

        public override void OnStateEnter()
        {
            
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            
            var rawInput = new Vector3(Context.InputProcessor.GetCurrentInput().x, 0f, Context.InputProcessor.GetCurrentInput().y);
            var moveVector =  Context.WalkSpeed * Context.CrouchSpeedModifier * rawInput;
            Context.PlayerMover.ChangeMove(MoveType.StateMove, moveVector, RotationType.RotateWithCamera);
        }

        public override void OnStateExit()
        {
            
        }

        public override bool TrySwitchState(out MainCharacterSubState newMainCharacterState)
        {
            newMainCharacterState = null;

            if (!Context.PlayerController.IsGrounded && !Physics.Raycast(Context.Transform.position, Vector3.down,
                    Context.PlayerController.Radius)) newMainCharacterState = _factory.Fall();
            else if (!Context.InputProcessor.GetIsCrouchPressed()) newMainCharacterState = _factory.Idle();
            
            return newMainCharacterState != null;
        }

        #endregion
    }
}