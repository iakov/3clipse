using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.ControlledSubStates
{
    public class ControlledWalkSubState : ControlledSubState
    {
        #region Initialization

        public ControlledWalkSubState(MainAnimalStateMachine context, ControlledSubStatesFactory factory) : base(context, factory){}

        #endregion

        #region SubStateMethods

        public override void OnStateEnter()
        {
            
        }
        
        public override void OnStateUpdate()
        {
            StateTimer += Time.deltaTime;
            
            var rawMoveVector = new Vector3(Context.InputHandler.GetCurrentInput().x, 0f, Context.InputHandler.GetCurrentInput().y);
            var moveVector = rawMoveVector * Context.WalkSpeed;
            Context.AnimalMover.ChangeMove(MoveType.StateMove, moveVector, RotationType.RotateOnBeginning);
        }

        public override void OnStateExit()
        {
            
        }

        public override bool TrySwitchState(out AnimalSubState newAnimalState)
        {
            newAnimalState = null;

            if (Context.InputHandler.GetIsJumpPressed()) newAnimalState = Factory.Jump();
            else if (Context.InputHandler.GetIsSprintPressed()) newAnimalState = Factory.Run();
            else if (!Context.AnimalController.IsGrounded && !Physics.Raycast(Context.AnimalTransform.position, Vector3.down, 0.1f))
                newAnimalState = Factory.Fall();
            else if (Context.InputHandler.GetCurrentInput() == Vector2.zero) newAnimalState = Factory.Stop();
            else if (Context.InputHandler.GetIsCrouchPressed()) newAnimalState = Factory.Crouch();
            
            return newAnimalState != null;
        }
        
        #endregion
    }
}
