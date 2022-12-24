using _3ClipseGame.Steam.GameCore.Origin.Parts.Player.MainAnimal.StateMachine.Structure.AI.SubStates;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Player.MainAnimal.StateMachine.Structure.AI
{
    public class AnimalAISubStateFactory : AnimalSubStateFactory
    {
        public AnimalAISubStateFactory(AnimalAIDto dto)
        {
            _dto = dto;
        }

        private readonly AnimalAIDto _dto;

        public AnimalAISubState Idle() => new AnimalAIIdleSubState(_dto, this);
        public AnimalAISubState Walk() => new AnimalAIFollowWalkSubState(_dto, this);
        public AnimalAISubState Run() => new AnimalAIFollowRunSubState(_dto, this);
        public AnimalAISubState Entertain() => new AnimalAIEntertainSubState(_dto, this);
        public AnimalAISubState WalkBack() => new AnimalAIWalkBackSubState(_dto, this);
    }
}