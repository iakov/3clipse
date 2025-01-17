namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Player.MainAnimal.StateMachine.Structure.Play
{
    public abstract class AnimalPlaySubState : AnimalSubState<AnimalPlaySubStateFactory, AnimalPlaySubState>
    {
        protected AnimalPlaySubState(AnimalPlaySubStateFactory factory, AnimalPlayDto dto) : base(factory)
            => Dto = dto;

        protected readonly AnimalPlayDto Dto;
    }
}