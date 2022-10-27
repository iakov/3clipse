namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.StateMachine.Structure.ControlAnimal
{
    public abstract class MainCharacterControlAnimalSubState : MainCharacterSubState<ControlAnimalSubStateFactory, MainCharacterControlAnimalSubState>
    {
        protected MainCharacterControlAnimalSubState(ControlAnimalDto controlAnimalDto, ControlAnimalSubStateFactory factory) : base(factory)
            => _controlAnimalDto = controlAnimalDto;

        private ControlAnimalDto _controlAnimalDto;
    }
}