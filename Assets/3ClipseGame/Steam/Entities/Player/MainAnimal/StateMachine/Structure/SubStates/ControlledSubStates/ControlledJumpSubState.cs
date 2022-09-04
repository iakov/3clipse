using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.ControlledSubStates
{
    public class ControlledJumpSubState : ControlledSubState
    {
        #region Initialization

        public ControlledJumpSubState(MainAnimalStateMachine context, ControlledSubStatesFactory factory) : base(context, factory){}

        private Vector3 _lastMoveVector;

        #endregion

        #region SubStateMethods

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
            StateTimer += Time.deltaTime;
        }

        public override void OnStateExit()
        {
            Context.InputHandler.IsJumpPressed = false;
            Context.Stamina.IsRecovering = true;
        }

        public override bool TrySwitchState(out AnimalSubState newAnimalState)
        {
            newAnimalState = null;

            if (Context.AnimalController.IsGrounded && StateTimer > 0.1f) newAnimalState = Factory.Idle();
            else if (Context.AnimalMover.GetLastMove(MoveType.StateMove, false).y + Context.AnimalMover.GetLastMove(MoveType.GravityMove, false).y < 0)
                newAnimalState = Factory.Fall();
            
            return newAnimalState != null;
        }
        
        #endregion
    }
}