using _3ClipseGame.Steam.GameCore.Origin.Parts.Player.MainAnimal.StateMachine.Structure.Play.SubStates;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Player.MainAnimal.StateMachine.Structure.Play
{
    public class AnimalPlaySubStateFactory : AnimalSubStateFactory
    {
        public AnimalPlaySubStateFactory(AnimalPlayDto dto)
        {
            _dto = dto;
        }

        private readonly AnimalPlayDto _dto;

        public AnimalPlaySubState Idle() => new AnimalPlayIdleSubState(_dto, this);

        public AnimalPlaySubState Walk() => new AnimalPlayWalkSubState(_dto, this);

        public AnimalPlaySubState Run() => new AnimalPlayRunSubState(_dto, this);

        public AnimalPlaySubState Jump() => new AnimalPlayJumpSubState(_dto, this);
        
        public AnimalPlaySubState Fall() => new AnimalPlayFallSubState(_dto, this);
        
        public AnimalPlaySubState Stop() => new AnimalPlayStopSubState(_dto, this);
        
        public AnimalPlaySubState Crouch() => new AnimalPlayCrouchSubState(_dto, this);
    }
}