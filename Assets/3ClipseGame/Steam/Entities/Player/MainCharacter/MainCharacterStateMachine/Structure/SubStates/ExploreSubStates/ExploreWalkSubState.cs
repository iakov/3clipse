using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.Scripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.SubStates.ExploreSubStates
{
    public class ExploreWalkSubState : MainCharacterSubState
    {
        #region Initialization

        public ExploreWalkSubState(MainCharacterStateMachine context, MainCharacterSubStateFactory factory) : base(context, factory) =>
            _factory = (ExploreSubStatesFactory) factory;

        private ExploreSubStatesFactory _factory;
        private Vector3 _lastMoveVector;


        #endregion

        #region MonoBehaviourMethods

        public override void OnStateEnter()
        {
            _lastMoveVector = Context.PlayerMover.GetLastMove(MoveType.StateMove);
        }

        public override void OnStateUpdate()
        {
            AddTime(Time.deltaTime);
            var rawMoveVector = new Vector3(Context.InputHandler.CurrentInput.x, 0f, Context.InputHandler.CurrentInput.y);
            var moveVector = rawMoveVector * Context.WalkSpeed;
            var t = StateTimer <= 1 ? StateTimer : 1f;
            var interpolatedMoveVector = Vector3.Lerp(_lastMoveVector, moveVector, t * Context.SpeedInterpolation);
            Context.PlayerMover.ChangeMove(MoveType.StateMove, interpolatedMoveVector, RotationType.RotateWithCamera);
        }
        
        public override void OnStateExit(){}

        public override bool TrySwitchState(out MainCharacterState newMainCharacterState)
        {
            newMainCharacterState = null;

            if (Context.InputHandler.IsJumpPressed) newMainCharacterState = _factory.Jump();
            else if (!Context.PlayerController.isGrounded && !Physics.Raycast(Context.Transform.position, Vector3.down,
                    Context.PlayerController.radius)) newMainCharacterState = _factory.Fall();
            else if (Context.InputHandler.CurrentInput == Vector2.zero) newMainCharacterState = _factory.Stop();
            else if (Context.InputHandler.IsRunPressed) newMainCharacterState = _factory.Run();
            else if (Context.InputHandler.IsCrouchPressed) newMainCharacterState = _factory.Crouch();
            return newMainCharacterState != null;
        }

        #endregion
    }
}