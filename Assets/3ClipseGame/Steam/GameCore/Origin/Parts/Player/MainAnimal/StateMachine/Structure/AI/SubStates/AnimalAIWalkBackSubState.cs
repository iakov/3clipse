using UnityEngine;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Player.MainAnimal.StateMachine.Structure.AI.SubStates
{
    public class AnimalAIWalkBackSubState : AnimalAISubState
    {
        public AnimalAIWalkBackSubState(AnimalAIDto dto, AnimalAISubStateFactory factory) : base(dto, factory){}

        public override void OnStateEnter()
        {
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            
            UpdateSpeed();
            UpdateMoveDirection();
        }

        private void UpdateSpeed()
        {
            var distance = Dto.GetDistance();
            var currentSpeed = Dto.WalkBackSpeedCurve.Evaluate(distance);
            Dto.AnimalAgent.speed = currentSpeed;
        }

        private void UpdateMoveDirection()
        {
            var animalAgent = Dto.AnimalAgent;
            var animalPosition = Dto.AnimalTransform.position;
            var nextDestination = GetMoveDirection() + animalPosition;
            
            if (animalAgent.isOnNavMesh) 
                animalAgent.SetDestination(nextDestination);
        }

        private Vector3 GetMoveDirection()
        {
            var mainCharacterForward = Dto.MainCharacterTransform.forward;
            var mainCharacterRight = Dto.MainCharacterTransform.right + mainCharacterForward;
            var mainCharacterLeft = mainCharacterRight * -1 + mainCharacterForward;
            var animalPosition = Dto.AnimalTransform.position;
            
            var distanceToLeft = Vector3.Distance(animalPosition, mainCharacterLeft);
            var distanceToRight = Vector3.Distance(animalPosition, mainCharacterRight);
            
            if (distanceToRight < distanceToLeft) return mainCharacterRight;
            return mainCharacterLeft;
        }

        public override void OnStateExit()
        {
        }

        protected override bool TrySwitch(out AnimalAISubState newAnimalState)
        {
            newAnimalState = null;

            if (IsFarEnough()) newAnimalState = Factory.Idle();

            return newAnimalState != null;
        }

        private bool IsFarEnough()
        {
            var distance = Dto.GetDistance();
            return distance > Dto.EndWalkBackDistance;
        }
    }
}