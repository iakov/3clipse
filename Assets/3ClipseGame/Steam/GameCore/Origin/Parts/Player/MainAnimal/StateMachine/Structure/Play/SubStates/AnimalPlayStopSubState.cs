using _3ClipseGame.Steam.GameCore.GlobalScripts.EntityScripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Player.MainAnimal.StateMachine.Structure.Play.SubStates
{
    public class AnimalPlayStopSubState : AnimalPlaySubState
    {
        public AnimalPlayStopSubState(AnimalPlayDto dto, AnimalPlaySubStateFactory factory) : base(factory, dto){}

        private Vector3 _lastMoveVector;

        public override void OnStateEnter()
        {
            SaveLastMove();
        }

        private void SaveLastMove()
        {
            _lastMoveVector = Dto.AnimalMover.GetLastMove(MoveType.StateMove, true);
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            
            ChangeMove();
        }

        private void ChangeMove()
        {
            var t = StateTimer <= 1 ? StateTimer : 1f;
            var interpolatedMoveVector = Vector3.Lerp(_lastMoveVector, Vector3.zero, t * Dto.SpeedInterpolation);
            Dto.AnimalMover.ChangeMove(MoveType.StateMove, interpolatedMoveVector, RotationType.NoRotation);
        }

        public override void OnStateExit()
        {
        }

        protected override bool TrySwitch(out AnimalPlaySubState newAnimalState)
        {
            newAnimalState = null;

            if (IsJumping()) newAnimalState = Factory.Jump();
            else if (IsFalling()) newAnimalState = Factory.Fall();
            else if (IsStill()) newAnimalState = Factory.Idle();
            else if (IsWalking()) newAnimalState = Factory.Walk();

            return newAnimalState != null;
        }

        private bool IsJumping()
        {
            return Dto.MovementInputProcessor.GetIsJumpPressed();
        }

        private bool IsFalling()
        {
            return Dto.AnimalController.IsGrounded;
        }

        private bool IsStill()
        {
            var lastMove = Dto.AnimalMover.GetLastMove(MoveType.StateMove, false);
            return lastMove == Vector3.zero;
        }

        private bool IsWalking()
        {
            var currentInput = Dto.MovementInputProcessor.GetCurrentInput();
            return currentInput == Vector2.zero;
        }
    }
}