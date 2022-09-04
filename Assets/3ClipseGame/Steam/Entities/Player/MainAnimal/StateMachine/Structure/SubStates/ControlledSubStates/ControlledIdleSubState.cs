using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.ControlledSubStates
{
    public class ControlledIdleSubState : ControlledSubState
    {
        #region Initialization

        public ControlledIdleSubState(MainAnimalStateMachine context, ControlledSubStatesFactory factory) : base(context, factory){}

        #endregion

        #region SubStateMethods

        public override void OnStateEnter()
        {
            Context.AnimalMover.ChangeMove(MoveType.StateMove, Vector3.zero, RotationType.NoRotation);
        }

        public override void OnStateUpdate()
        {
            StateTimer += Time.deltaTime;
        }

        public override void OnStateExit(){}

        public override bool TrySwitchState(out AnimalSubState newAnimalState)
        {
            newAnimalState = null;

            if (Context.InputHandler.IsJumpPressed) newAnimalState = Factory.Jump();
            else if (Context.InputHandler.CurrentInput != Vector2.zero) newAnimalState = Factory.Walk();
            else if (!Context.AnimalController.IsGrounded) newAnimalState = Factory.Fall();
            else if (Context.InputHandler.IsCrouchPressed) newAnimalState = Factory.Crouch();

            return newAnimalState != null;
        }
        
        #endregion
    }
}
