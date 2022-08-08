using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates.ControlledSubStates
{
    public class ControlledStopSubState : AnimalSubState

    {
        public ControlledStopSubState(MainAnimalStateMachine context, AnimalStateFactory factory) : base(context, factory) 
            => _factory = (ControlledSubStatesFactory) factory;

        private ControlledSubStatesFactory _factory;
        private Vector3 _lastMoveVector;

        public override void OnStateEnter()
        {
            _lastMoveVector = Context.AnimalMover.GetLastMove(MoveType.StateMove, true);
        }

        public override void OnStateUpdate()
        {
            AddTime(Time.deltaTime);
            var t = StateTimer <= 1 ? StateTimer : 1f;
            var interpolatedMoveVector = Vector3.Lerp(_lastMoveVector, Vector3.zero, t * Context.SpeedInterpolation);
            Context.AnimalMover.ChangeMove(MoveType.StateMove, interpolatedMoveVector, RotationType.NoRotation);
        }

        public override void OnStateExit(){}

        public override bool TrySwitchState(out AnimalState newAnimalState)
        {
            newAnimalState = null;

            if (Context.InputHandler.IsJumpPressed) newAnimalState = _factory.Jump();
            else if (!Context.AnimalController.IsGrounded && !Physics.Raycast(Context.AnimalTransform.position, Vector3.down,
                         Context.AnimalController.Radius)) newAnimalState = _factory.Fall();
            else if (Context.AnimalMover.GetLastMove(MoveType.StateMove, false) == Vector3.zero) newAnimalState = _factory.Idle();
            else if (Context.InputHandler.CurrentInput != Vector2.zero) newAnimalState = _factory.Walk();

            return newAnimalState != null;
        }
    }
}