namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.States
{
    public class AnimalControlState : MainCharacterState
    {
        public AnimalControlState(MainCharacterStateMachine context, MainCharacterStateFactory factory) : base(context, factory){}

        private bool _isSwitching;

        public override void OnStateEnter()
        {
            Context.InputHandler.SwitchToAnimalControls();

            Context.InputHandler.ModeSwitchPressed += OnModeSwitch;
        }

        public override void OnStateUpdate(){}

        public override void OnStateExit()
        {
            Context.InputHandler.ModeSwitchPressed -= OnModeSwitch;
        }

        public override bool TrySwitchState(out MainCharacterState newMainCharacterState)
        {
            newMainCharacterState = null;

            if (_isSwitching) newMainCharacterState = Factory.ExploreState();

            return newMainCharacterState != null;
        }
        
        private void OnModeSwitch() => _isSwitching = true;
    }
}