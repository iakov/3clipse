using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.ControlledSubStates
{
    public class ControlledCrouchSubState : ControlledSubState
    {
        #region Initialization
        
        public ControlledCrouchSubState(MainAnimalStateMachine context, ControlledSubStatesFactory factory) : base(context, factory){}

        #endregion

        #region SubStateMethods
        
        public override void OnStateEnter(){}

        public override void OnStateUpdate()
        {
            StateTimer += Time.deltaTime;
            
            var rawInput = new Vector3(Context.InputHandler.CurrentInput.x, 0f, Context.InputHandler.CurrentInput.y);
            var moveVector = rawInput * Context.CrouchSpeedModifier;
            Context.AnimalMover.ChangeMove(MoveType.StateMove, moveVector, RotationType.RotateWithCamera);
        }

        public override void OnStateExit(){}

        public override bool TrySwitchState(out AnimalSubState newAnimalState)
        {
            newAnimalState = null;
            
            if (!Context.AnimalController.IsGrounded && !Physics.Raycast(Context.AnimalTransform.position, Vector3.down,
                    Context.AnimalController.Radius)) newAnimalState = Factory.Fall();
            else if (!Context.InputHandler.IsCrouchPressed) newAnimalState = Factory.Idle();

            return newAnimalState != null;
        }
        
        #endregion
    }
}