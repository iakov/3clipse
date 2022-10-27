namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.Play
{
    public class AnimalPlayState : AnimalState
    {
        public AnimalPlayState(AnimalPlayDto dto, AnimalStateFactory factory) : base(factory)
        {
            _dto = dto;
        }

        private AnimalPlaySubStateFactory _subStateFactory;
        private AnimalPlaySubState _currentSubState;
        private readonly AnimalPlayDto _dto;

        public override void OnStateEnter()
        {
            EnableController();
            EnterSubState();
        }

        private void EnableController()
        {
            _dto.AnimalController.enabled = true;
        }

        private void EnterSubState()
        {
            _subStateFactory = new AnimalPlaySubStateFactory(_dto);
            _currentSubState = _subStateFactory.Idle();

            _currentSubState.OnStateEnter();
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();

            if (_currentSubState.TrySwitchState(out var newSubState)) SwitchSubState(newSubState);
            _currentSubState.OnStateUpdate();
        }
        
        private void SwitchSubState(AnimalPlaySubState newState)
        {
            _currentSubState.OnStateExit();
            newState.OnStateEnter();
            _currentSubState = newState;
        }
        
        public override void OnStateExit()
        {
            _currentSubState.OnStateExit();
            _dto.AnimalController.enabled = false;
        }

        protected override bool TrySwitch(out AnimalState newAnimalState)
        {
            newAnimalState = null;

            if (IsSwitching()) newAnimalState = Factory.AnimalAIState();

            return newAnimalState != null;
        }

        private bool IsSwitching()
        {
            return _dto.MovementInputProcessor.GetIsSwitched();
        }
    }
}
