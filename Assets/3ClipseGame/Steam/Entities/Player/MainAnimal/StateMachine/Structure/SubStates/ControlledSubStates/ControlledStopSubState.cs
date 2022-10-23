using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.ControlledSubStates
{
    public class ControlledStopSubState : ControlledSubState
    {
        public ControlledStopSubState(MainAnimalStateMachine context, ControlledSubStatesFactory factory) : base(context, factory){}

        private Vector3 _lastMoveVector;

        public override void OnStateEnter()
        {
            _lastMoveVector = Context.AnimalMover.GetLastMove(MoveType.StateMove, true);
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();

            var t = StateTimer <= 1 ? StateTimer : 1f;
            var interpolatedMoveVector = Vector3.Lerp(_lastMoveVector, Vector3.zero, t * Context.SpeedInterpolation);
            Context.AnimalMover.ChangeMove(MoveType.StateMove, interpolatedMoveVector, RotationType.NoRotation);
        }

        public override void OnStateExit()
        {
            
        }

        public override bool TrySwitchState(out AnimalSubState<ControlledSubStatesFactory> newAnimalState)
        {
            newAnimalState = null;

            if (Context.InputProcessor.GetIsJumpPressed()) newAnimalState = Factory.Jump();
            else if (!Context.AnimalController.IsGrounded && !Physics.Raycast(Context.AnimalTransform.position, Vector3.down,
                         Context.AnimalController.Radius)) newAnimalState = Factory.Fall();
            else if (Context.AnimalMover.GetLastMove(MoveType.StateMove, false) == Vector3.zero) newAnimalState = Factory.Idle();
            else if (Context.InputProcessor.GetCurrentInput() != Vector2.zero) newAnimalState = Factory.Walk();

            return newAnimalState != null;
        }
    }
}