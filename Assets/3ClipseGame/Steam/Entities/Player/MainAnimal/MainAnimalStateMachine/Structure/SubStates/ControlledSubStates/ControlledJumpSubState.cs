using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates.ControlledSubStates
{
    public class ControlledJumpSubState : AnimalSubState
    {
        public ControlledJumpSubState(MainAnimalStateMachine context, AnimalStateFactory factory) : base(context, factory)
            => _factory = (ControlledSubStatesFactory)factory;

        private ControlledSubStatesFactory _factory;
        private Vector3 _lastMoveVector;

        public override void OnStateEnter()
        {
            _lastMoveVector = Context.AnimalMover.GetLastMove(MoveType.StateMove, true);
            _lastMoveVector.y = 0f;
            var jumpMoveVector = _lastMoveVector + Vector3.up * Context.JumpStrength;
            Context.AnimalMover.ChangeMove(MoveType.StateMove, jumpMoveVector, RotationType.NoRotation);

            Context.Stamina.IsRecovering = false;
            Context.Stamina.AddValue(Context.JumpStaminaReduce);
        }

        public override void OnStateUpdate()
        {
            AddTime(Time.deltaTime);
        }

        public override void OnStateExit()
        {
            Context.InputHandler.IsJumpPressed = false;
            Context.Stamina.IsRecovering = true;
        }

        public override bool TrySwitchState(out AnimalState newAnimalState)
        {
            newAnimalState = null;

            if (Context.AnimalController.IsGrounded && StateTimer > 0.1f) newAnimalState = _factory.Idle();
            
            return newAnimalState != null;
        }
    }
}