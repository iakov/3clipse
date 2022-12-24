using _3ClipseGame.Steam.GameCore.GlobalScripts.EntityScripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Player.MainAnimal.StateMachine.Structure.Play.SubStates
{
    public class AnimalPlayCrouchSubState : AnimalPlaySubState
    {
        public AnimalPlayCrouchSubState(AnimalPlayDto dto, AnimalPlaySubStateFactory factory) : base(factory, dto){}

        public override void OnStateEnter()
        {
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            
            ChangeMove();
        }

        private void ChangeMove()
        {
            var rawInput = new Vector3(Dto.MovementInputProcessor.GetCurrentInput().x, 0f, Dto.MovementInputProcessor.GetCurrentInput().y);
            var moveVector = rawInput * Dto.CrouchSpeedModifier;
            Dto.AnimalMover.ChangeMove(MoveType.StateMove, moveVector, RotationType.RotateWithCamera);
        }

        public override void OnStateExit()
        {
        }

        protected override bool TrySwitch(out AnimalPlaySubState newAnimalState)
        {
            newAnimalState = null;
            
            if (IsFalling()) newAnimalState = Factory.Fall();
            else if (!IsCrouching()) newAnimalState = Factory.Idle();

            return newAnimalState != null;
        }

        private bool IsFalling()
        {
            return Dto.AnimalController.IsGrounded == false;
        }

        private bool IsCrouching()
        {
            return Dto.MovementInputProcessor.GetIsCrouchPressed();
        }
    }
}