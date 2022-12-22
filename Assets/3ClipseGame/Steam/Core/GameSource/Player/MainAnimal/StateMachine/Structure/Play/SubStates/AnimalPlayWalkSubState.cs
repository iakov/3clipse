using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Core.GameSource.Player.MainAnimal.StateMachine.Structure.Play.SubStates
{
    public class AnimalPlayWalkSubState : AnimalPlaySubState
    {
        public AnimalPlayWalkSubState(AnimalPlayDto dto, AnimalPlaySubStateFactory factory) : base(factory, dto){}

        public override void OnStateEnter()
        {
        }
        
        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            
            ChangeMove();
            Rotate();
        }

        private void ChangeMove()
        {
            var currentInput = Dto.MovementInputProcessor.GetCurrentInput();
            var rawMoveVector = new Vector3(currentInput.x, 0f, currentInput.y);
            var moveVector = rawMoveVector * Dto.WalkSpeed;
            Dto.AnimalMover.ChangeMove(MoveType.StateMove, moveVector, RotationType.RotateOnBeginning);
        }
        
        private void Rotate()
        {
            var rotatedMove = Dto.AnimalMover.GetLastMove(MoveType.StateMove, true);
            if (rotatedMove == Vector3.zero) return;
            
            Dto.AnimalController.Rotate(Quaternion.LookRotation(rotatedMove));
        }

        public override void OnStateExit()
        {
        }

        protected override bool TrySwitch(out AnimalPlaySubState newAnimalState)
        {
            newAnimalState = null;

            if (IsJumping()) newAnimalState = Factory.Jump();
            else if (IsSprinting()) newAnimalState = Factory.Run();
            else if (IsFalling()) newAnimalState = Factory.Fall();
            else if (IsStill()) newAnimalState = Factory.Stop();
            else if (IsCrouching()) newAnimalState = Factory.Crouch();
            
            return newAnimalState != null;
        }

        private bool IsJumping()
        {
            return Dto.MovementInputProcessor.GetIsJumpPressed();
        }

        private bool IsSprinting()
        {
            return Dto.MovementInputProcessor.GetIsSprintPressed();
        }

        private bool IsFalling()
        {
            return !Dto.AnimalController.IsGrounded;
        }

        private bool IsStill()
        {
            var currentInput = Dto.MovementInputProcessor.GetCurrentInput();
            return  currentInput == Vector2.zero;
        }

        private bool IsCrouching()
        {
            return Dto.MovementInputProcessor.GetIsCrouchPressed();
        }
    }
}
