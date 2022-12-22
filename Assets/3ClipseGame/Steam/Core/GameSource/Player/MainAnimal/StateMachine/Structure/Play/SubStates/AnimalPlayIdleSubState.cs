using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Core.GameSource.Player.MainAnimal.StateMachine.Structure.Play.SubStates
{
    public class AnimalPlayIdleSubState : AnimalPlaySubState
    {
        public AnimalPlayIdleSubState(AnimalPlayDto dto, AnimalPlaySubStateFactory factory) : base(factory, dto){}

        public override void OnStateEnter()
        {
            ResetMove();
        }

        private void ResetMove()
        {
            var animalMover = Dto.AnimalMover;
            animalMover.ChangeMove(MoveType.StateMove, Vector3.zero, RotationType.NoRotation);
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
        }

        public override void OnStateExit()
        {
        }

        protected override bool TrySwitch(out AnimalPlaySubState newAnimalState)
        {
            newAnimalState = null;

            if (IsJumping()) newAnimalState = Factory.Jump();
            else if (!IsStill()) newAnimalState = Factory.Walk();
            else if (IsFalling()) newAnimalState = Factory.Fall();
            else if (IsCrouching()) newAnimalState = Factory.Crouch();

            return newAnimalState != null;
        }

        private bool IsJumping()
        {
            return Dto.MovementInputProcessor.GetIsJumpPressed();
        }

        private bool IsStill()
        {
            var currentInput = Dto.MovementInputProcessor.GetCurrentInput();
            return currentInput == Vector2.zero;
        }

        private bool IsFalling()
        {
            return !Dto.AnimalController.IsGrounded;
        }

        private bool IsCrouching()
        {
            return Dto.MovementInputProcessor.GetIsCrouchPressed();
        }
    }
}
