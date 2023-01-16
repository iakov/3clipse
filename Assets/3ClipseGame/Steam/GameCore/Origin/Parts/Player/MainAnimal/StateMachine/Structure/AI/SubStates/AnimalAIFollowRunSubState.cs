namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Player.MainAnimal.StateMachine.Structure.AI.SubStates
{
    public class AnimalAIFollowRunSubState : AnimalAISubState
    {
        public AnimalAIFollowRunSubState(AnimalAIDto dto, AnimalAISubStateFactory factory) : base(dto, factory){}

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
            Dto.AnimalAgent.speed = Dto.FollowRunSpeedCurve.Evaluate(distance);
        }

        public override void OnStateExit()
        {
        }

        protected override bool TrySwitch(out AnimalAISubState newAnimalState)
        {
            newAnimalState = null;

            if (IsTooClose()) newAnimalState = Factory.Walk();
            
            return newAnimalState != null;
        }

        private bool IsTooClose()
        {
            var distance = Dto.GetDistance();
            return distance < Dto.MinFollowRunDistance;
        }
    }
}