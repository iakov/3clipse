namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Player.MainCharacter.StateMachine.Structure.ControlAnimal
{
    public class MainCharacterControlAnimalState : MainCharacterState
    {
        public MainCharacterControlAnimalState(ControlAnimalDto dto, MainCharacterStateFactory factory) : base(factory)
        {
            _dto = dto;
        }

        private readonly ControlAnimalDto _dto;

        public override void OnStateEnter()
        {
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
        }

        public override void OnStateExit()
        {
        }

        protected override bool TrySwitch(out MainCharacterState newMainCharacterState)
        {
            newMainCharacterState = null;

            if (IsSwitching()) newMainCharacterState = Factory.Explore();

            return newMainCharacterState != null;
        }

        private bool IsSwitching()
        {
            return _dto.InputProcessor.GetIsSwitched();
        }
    }
}