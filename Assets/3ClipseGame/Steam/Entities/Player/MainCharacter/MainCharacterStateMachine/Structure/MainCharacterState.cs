using _3ClipseGame.Steam.Entities.Player.Scripts;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure
{
    public abstract class MainCharacterState : State<MainCharacterStateFactory, MainCharacterState>
    {
        protected MainCharacterState(MainCharacterStateFactory factory) : base(factory){}

        public abstract override void OnStateEnter();
        
        public abstract override void OnStateExit();

        public abstract override bool TrySwitchState(out MainCharacterState newState);
    }
}