using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.ControlledSubStates
{
    public class ControlledStopSubState : ControlledSubState
    {
        #region Initialization

        public ControlledStopSubState(MainAnimalStateMachine context, ControlledSubStatesFactory factory) : base(context, factory){}

        private Vector3 _lastMoveVector;

        #endregion

        #region SubStateMethods

        public override void OnStateEnter()
        {
            _lastMoveVector = Context.AnimalMover.GetLastMove(MoveType.StateMove, true);
        }

        public override void OnStateUpdate()
        {
            StateTimer += Time.deltaTime;
            
            var t = StateTimer <= 1 ? StateTimer : 1f;
            var interpolatedMoveVector = Vector3.Lerp(_lastMoveVector, Vector3.zero, t * Context.SpeedInterpolation);
            Context.AnimalMover.ChangeMove(MoveType.StateMove, interpolatedMoveVector, RotationType.NoRotation);
        }

        public override void OnStateExit()
        {
            
        }

        public override bool TrySwitchState(out AnimalSubState newAnimalState)
        {
            newAnimalState = null;

            if (Context.InputHandler.GetIsJumpPressedRecently()) newAnimalState = Factory.Jump();
            else if (!Context.AnimalController.IsGrounded && !Physics.Raycast(Context.AnimalTransform.position, Vector3.down,
                         Context.AnimalController.Radius)) newAnimalState = Factory.Fall();
            else if (Context.AnimalMover.GetLastMove(MoveType.StateMove, false) == Vector3.zero) newAnimalState = Factory.Idle();
            else if (Context.InputHandler.GetCurrentInput() != Vector2.zero) newAnimalState = Factory.Walk();

            return newAnimalState != null;
        }
        
        #endregion
    }
}