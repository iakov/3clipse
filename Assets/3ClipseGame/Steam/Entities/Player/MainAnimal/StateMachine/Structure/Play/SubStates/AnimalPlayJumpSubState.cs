using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.Play.SubStates
{
    public class AnimalPlayJumpSubState : AnimalPlaySubState
    {
        public AnimalPlayJumpSubState(AnimalPlayDto dto, AnimalPlaySubStateFactory factory) : base(factory, dto){}

        private Vector3 _lastMoveVector;

        public override void OnStateEnter()
        {
            SaveLastVector();
            SetJumpVector();
            SwitchStaminaRecovery(false);
            WasteJumpStamina();
        }

        private void SaveLastVector()
        {
            var lastMoveVector = Dto.AnimalMover.GetLastMove(MoveType.StateMove, true);
            lastMoveVector.y = 0f;
            _lastMoveVector = lastMoveVector;
        }

        private void SetJumpVector()
        {
            var jumpMoveVector = _lastMoveVector + Vector3.up * Dto.JumpStrength;
            Dto.AnimalMover.ChangeMove(MoveType.StateMove, jumpMoveVector, RotationType.NoRotation);
        }

        private void WasteJumpStamina()
        {
            var stamina = Dto.Stamina;
            stamina.AddValue(Dto.JumpStaminaReduce);
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
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

            if (IsIdling()) newAnimalState = Factory.Idle();
            else if (IsFalling()) newAnimalState = Factory.Fall();
            
            return newAnimalState != null;
        }

        private bool IsIdling()
        {
            var isGrounded = Dto.AnimalController.IsGrounded;
            return isGrounded && StateTimer > 0.1f;
        }

        private bool IsFalling()
        {
            var stateYMove = Dto.AnimalMover.GetLastMove(MoveType.StateMove, false).y;
            var gravityYMove = Dto.AnimalMover.GetLastMove(MoveType.GravityMove, false).y;

            return stateYMove + gravityYMove < 0f;
        }
    }
}