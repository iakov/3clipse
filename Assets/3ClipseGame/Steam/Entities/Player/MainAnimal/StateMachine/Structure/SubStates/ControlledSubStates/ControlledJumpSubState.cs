using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.ControlledSubStates
{
    public class ControlledJumpSubState : ControlledSubState
    {
        public ControlledJumpSubState(MainAnimalStateMachine context, ControlledSubStatesFactory factory) : base(context, factory){}

        private Vector3 _lastMoveVector;
        private bool _isJumped;

        public override void OnStateEnter()
        {
            _lastMoveVector = Context.AnimalMover.GetLastMove(MoveType.StateMove, true);
            _lastMoveVector.y = 0f;
            var jumpMoveVector = _lastMoveVector + Vector3.up * Context.JumpStrength;
            Context.AnimalMover.ChangeMove(MoveType.StateMove, jumpMoveVector, RotationType.NoRotation);

            Context.Stamina.IsRecovering = false;
            Context.Stamina.AddValue(Context.JumpStaminaReduce);
        }

        public override void OnStateExit()
        {
            Context.Stamina.IsRecovering = true;
        }

        public override bool TrySwitchState(out AnimalSubState<ControlledSubStatesFactory> newAnimalState)
        {
            newAnimalState = null;

            if (Context.AnimalController.IsGrounded && StateTimer > 0.1f) newAnimalState = Factory.Idle();
            else if (Context.AnimalMover.GetLastMove(MoveType.StateMove, false).y + Context.AnimalMover.GetLastMove(MoveType.GravityMove, false).y < 0)
                newAnimalState = Factory.Fall();
            
            return newAnimalState != null;
        }
    }
}