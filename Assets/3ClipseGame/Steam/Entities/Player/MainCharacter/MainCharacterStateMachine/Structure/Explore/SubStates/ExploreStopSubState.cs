using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.Explore.SubStates
{
    public class ExploreStopSubState : MainCharacterSubState
    {
        public ExploreStopSubState(MainCharacterStateMachine context, MainCharacterSubStateFactory factory) : base(context, factory) =>
            _factory = (ExploreSubStatesFactory) factory;

        private ExploreSubStatesFactory _factory;
        private Vector3 _lastMoveVector;

        public override void OnStateEnter()
        {
            _lastMoveVector = Context.PlayerMover.GetLastMove(MoveType.StateMove, true);
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            
            var t = StateTimer <= 1 ? StateTimer : 1f;
            var interpolatedMoveVector = Vector3.Lerp(_lastMoveVector, Vector3.zero, t * Context.SpeedInterpolation);
            Context.PlayerMover.ChangeMove(MoveType.StateMove, interpolatedMoveVector, RotationType.NoRotation);
        }

        public override void OnStateExit()
        {
        }

        public override bool TrySwitchState(out MainCharacterSubState newMainCharacterState)
        {
            newMainCharacterState = null;

            if (Context.InputProcessor.GetIsJumpPressed()) newMainCharacterState = _factory.Jump();
            else if (!Context.PlayerController.IsGrounded && !Physics.Raycast(Context.Transform.position, Vector3.down,
                    Context.PlayerController.Radius)) newMainCharacterState = _factory.Fall();
            else if (Context.PlayerMover.GetLastMove(MoveType.StateMove, false) == Vector3.zero) newMainCharacterState = _factory.Idle();
            else if (Context.InputProcessor.GetCurrentInput() != Vector2.zero) newMainCharacterState = _factory.Walk();

            return newMainCharacterState != null;
        }
    }
}