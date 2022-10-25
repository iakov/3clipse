namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.ControlAnimal
{
    public class MainCharacterControlAnimalState : MainCharacterState
    {
        public MainCharacterControlAnimalState(ControlAnimalDto dto, MainCharacterStateFactory factory) : base(factory)
            => _dto = dto;

        private ControlAnimalDto _dto;
        
        private int _framesFromSwitch;

        public override void OnStateEnter()
        {
            
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            _framesFromSwitch++;
        }

        public override void OnStateExit()
        {
            
        }

        public override bool TrySwitchState(out MainCharacterState newMainCharacterState)
        {
            newMainCharacterState = null;

            if (_dto.InputProcessor.GetIsSwitched() && _framesFromSwitch >= 2) newMainCharacterState = Factory.Explore();

            return newMainCharacterState != null;
        }
    }
}