using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.AI.SubStates
{
    public class AnimalAIEntertainSubState : AnimalAISubState
    {
        public AnimalAIEntertainSubState(AnimalAIDto context, AnimalAISubStateFactory factory) : base(context, factory){}

        public override void OnStateEnter()
        {
            Dto.AnimalMover.ChangeMove(MoveType.StateMove, Vector3.zero, RotationType.RotateOnBeginning);
        }

        public override void OnStateExit()
        {
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
        }

        protected override bool TrySwitch(out AnimalAISubState newAnimalState)
        {
            newAnimalState = null;

            if (IsStill() == false) newAnimalState = Factory.Idle();

            return newAnimalState != null;
        }

        private bool IsStill()
        {
            var velocity = Dto.MainCharacterController.Velocity;
            return velocity == Vector3.zero;
        }
    }
}