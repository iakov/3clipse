using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.Play.SubStates
{
    public class AnimalPlayRunSubState : AnimalPlaySubState
    {
        public AnimalPlayRunSubState(AnimalPlayDto dto, AnimalPlaySubStateFactory factory) : base(factory, dto){}

        public override void OnStateEnter()
        {
            SwitchStaminaRecovery(false);
        }
        
        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            
            ChangeMove();
            WasteRunStamina();
        }

        private void ChangeMove()
        {
            var currentInput = Dto.MovementInputProcessor.GetCurrentInput();
            var rawMoveVector = new Vector3(currentInput.x, 0f, currentInput.y);

            var speedModifier = Dto.RunSpeedCurve.Evaluate(StateTimer) * Dto.WalkSpeed;
            var moveVector = rawMoveVector * speedModifier;
            Dto.AnimalMover.ChangeMove(MoveType.StateMove, moveVector, RotationType.RotateOnBeginning);
        }

        private void WasteRunStamina()
        {
            Dto.Stamina.AddValue(Dto.RunStaminaReduce * Time.deltaTime);
        }

        public override void OnStateExit()
        {
            SwitchStaminaRecovery(true);
        }

        private void SwitchStaminaRecovery(bool isRecovering)
        {
            var stamina = Dto.Stamina;
            stamina.IsRecovering = isRecovering;
        }

        protected override bool TrySwitch(out AnimalPlaySubState newAnimalState)
        {
            newAnimalState = null;
            
            if (IsJumping()) newAnimalState = Factory.Jump();
            else if (IsOutOfStamina()) newAnimalState = Factory.Walk();
            else if (!IsSprinting()) newAnimalState = Factory.Walk();
            else if (IsFalling()) newAnimalState = Factory.Fall();
            else if (IsStill()) newAnimalState = Factory.Stop();
            else if (IsCrouching()) newAnimalState = Factory.Crouch();

            return newAnimalState != null;
        }

        private bool IsJumping()
        {
            return Dto.MovementInputProcessor.GetIsJumpPressed();
        }

        private bool IsOutOfStamina()
        {
            return Dto.Stamina.StaminaPercentage <= 0f;
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
            return currentInput == Vector2.zero;
        }

        private bool IsCrouching()
        {
            return Dto.MovementInputProcessor.GetIsCrouchPressed();
        }
    }
}