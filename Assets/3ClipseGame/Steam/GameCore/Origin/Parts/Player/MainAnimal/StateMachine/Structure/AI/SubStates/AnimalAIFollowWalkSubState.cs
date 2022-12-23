namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Player.MainAnimal.StateMachine.Structure.AI.SubStates
{
    public class AnimalAIFollowWalkSubState : AnimalAISubState
    {
        public AnimalAIFollowWalkSubState(AnimalAIDto dto, AnimalAISubStateFactory factory) : base(dto, factory){}

        public override void OnStateEnter()
        {
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();

            UpdateDestination();
            UpdateSpeed();
        }
        
        private void UpdateDestination()
        {
            if (Dto.AnimalAgent.isOnNavMesh) 
                Dto.AnimalAgent.SetDestination(Dto.CurrentTarget.position);
        }
        
        private void UpdateSpeed()
        {
            var distance = Dto.GetDistance();
            Dto.AnimalAgent.speed = Dto.FollowWalkSpeedCurve.Evaluate(distance);
        }

        public override void OnStateExit()
        {
        }
        
        protected override bool TrySwitch(out AnimalAISubState newAnimalState)
        {
            newAnimalState = null;

            if (IsTooFar()) newAnimalState = Factory.Run();
            else if (IsCloseEnough()) newAnimalState = Factory.Idle();

            return newAnimalState != null;
        }

        private bool IsTooFar()
        {
            var distance = Dto.GetDistance();
            return distance >= Dto.MinFollowRunDistance;
        }

        private bool IsCloseEnough()
        {
            var distance = Dto.GetDistance();
            var isOffMeshLink = !Dto.AnimalAgent.isOnOffMeshLink;
            return distance < Dto.StopFollowWalkDistance && isOffMeshLink;
        }
    }
}