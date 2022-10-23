using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.ControlledSubStates
{
    public class ControlledWalkSubState : ControlledSubState
    {
        #region Initialization

        public ControlledWalkSubState(MainAnimalStateMachine context, ControlledSubStatesFactory factory) : base(context, factory){}

        #endregion

        public override void OnStateEnter()
        {
            
        }
        
        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            
            var rawMoveVector = new Vector3(Context.InputProcessor.GetCurrentInput().x, 0f, Context.InputProcessor.GetCurrentInput().y);
            var moveVector = rawMoveVector * Context.WalkSpeed;
            Context.AnimalMover.ChangeMove(MoveType.StateMove, moveVector, RotationType.RotateOnBeginning);
            Rotate();
        }

        public override void OnStateExit()
        {
            
        }

        public override bool TrySwitchState(out AnimalSubState<ControlledSubStatesFactory> newAnimalState)
        {
            newAnimalState = null;

            if (Context.InputProcessor.GetIsJumpPressed()) newAnimalState = Factory.Jump();
            else if (Context.InputProcessor.GetIsSprintPressed()) newAnimalState = Factory.Run();
            else if (!Context.AnimalController.IsGrounded && !Physics.Raycast(Context.AnimalTransform.position, Vector3.down, 0.1f))
                newAnimalState = Factory.Fall();
            else if (Context.InputProcessor.GetCurrentInput() == Vector2.zero) newAnimalState = Factory.Stop();
            else if (Context.InputProcessor.GetIsCrouchPressed()) newAnimalState = Factory.Crouch();
            
            return newAnimalState != null;
        }
        
        private void Rotate()
        {
            var rotatedMove = Context.AnimalMover.GetLastMove(MoveType.StateMove, true);
            if (rotatedMove == Vector3.zero) return;
            
            Context.AnimalController.Rotate(Quaternion.LookRotation(rotatedMove));
        }
    }
}
