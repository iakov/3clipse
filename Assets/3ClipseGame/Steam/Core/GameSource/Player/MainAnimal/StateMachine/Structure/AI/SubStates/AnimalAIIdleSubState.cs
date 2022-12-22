using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Core.GameSource.Player.MainAnimal.StateMachine.Structure.AI.SubStates
{
    public class AnimalAIIdleSubState : AnimalAISubState
    {
        public AnimalAIIdleSubState(AnimalAIDto dto, AnimalAISubStateFactory factory) : base(dto, factory) {}

        public override void OnStateEnter()
        {
            ResetMove();
            UpdateAgent();
        }

        private void ResetMove()
        {
            var mover = Dto.AnimalMover;
            mover.ChangeMove(MoveType.StateMove, Vector3.zero, RotationType.RotateOnBeginning);
        }

        private void UpdateAgent()
        {
            Dto.AnimalAgent.enabled = false;
            Dto.AnimalAgent.enabled = true;
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            
            LookAtCharacter();
        }

        private void LookAtCharacter()
        {
            var character = Dto.MainCharacterTransform;
            var animal = Dto.AnimalTransform;
            animal.LookAt(character);
        }

        public override void OnStateExit()
        {
            Dto.UpdateCurrentTarget();
        }

        protected override bool TrySwitch(out AnimalAISubState newAnimalState)
        {
            newAnimalState = null;

            if (IsTooFar()) newAnimalState = Factory.Walk();
            else if (IsTooClose()) newAnimalState = Factory.WalkBack();
            else if (IsEntertainTime()) newAnimalState = Factory.Entertain();

            return newAnimalState != null;
        }

        private bool IsTooFar()
        {
            var distance = Dto.GetDistance();
            return distance > Dto.StartFollowWalkDistance;
        }

        private bool IsTooClose()
        {
            var distance = Dto.GetDistance();
            return distance < Dto.StartWalkBackDistance;
        }

        private bool IsEntertainTime()
        {
            return StateTimer >= Dto.WaitTimeBeforeEntertain;
        }
    }
}