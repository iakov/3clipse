namespace _3ClipseGame.Steam.Core.GameSource.Player.MainAnimal.StateMachine.Structure.AI
{
    public abstract class AnimalAISubState : AnimalSubState<AnimalAISubStateFactory, AnimalAISubState>
    {
        protected AnimalAISubState(AnimalAIDto dto, AnimalAISubStateFactory factory) : base(factory)
        {
            Dto = dto;
        }

        protected readonly AnimalAIDto Dto;
    }
}