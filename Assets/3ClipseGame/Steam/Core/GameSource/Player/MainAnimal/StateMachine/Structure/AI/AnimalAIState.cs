namespace _3ClipseGame.Steam.Core.GameSource.Player.MainAnimal.StateMachine.Structure.AI
{
    public class AnimalAIState : AnimalState
    {
        public AnimalAIState(AnimalAIDto dto, AnimalStateFactory factory) : base(factory)
        {
            _dto = dto;
        }
        
        private AnimalAISubStateFactory _subStateFactory;
        private AnimalAISubState _currentSubState;
        private readonly AnimalAIDto _dto;

        public override void OnStateEnter()
        {
            EnterSubState();
            SwitchNavMeshAgent(false);
        }

        private void EnterSubState()
        {
            _subStateFactory = new AnimalAISubStateFactory(_dto);
            _currentSubState = _subStateFactory.Idle();
            _currentSubState.OnStateEnter();
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();

            if (_currentSubState.TrySwitchState(out var newSubState)) SwitchSubState(newSubState);
            _currentSubState.OnStateUpdate();
        }
        
        private void SwitchSubState(AnimalAISubState newSubState)
        {
            _currentSubState.OnStateExit();
            newSubState.OnStateEnter();
            _currentSubState = newSubState;
        }

        public override void OnStateExit()
        {
            _currentSubState.OnStateExit();
           SwitchNavMeshAgent(false);
        }

        private void SwitchNavMeshAgent(bool isActive)
        {
            _dto.AnimalAgent.enabled = isActive;
        }

        protected override bool TrySwitch(out AnimalState newAnimalState)
        {
            newAnimalState = null;

            if (IsSwitching()) newAnimalState = Factory.AnimalPlayState();

            return newAnimalState != null;
        }

        private bool IsSwitching()
        {
            return _dto.MovementInputProcessor.GetIsSwitched();
        }
    }
}
