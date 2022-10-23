using _3ClipseGame.Steam.Entities.Player.Scripts;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure
{
    public abstract class MainCharacterState : State<MainCharacterState, MainCharacterStateFactory, MainCharacterStateMachine>
    {
        protected MainCharacterState(MainCharacterStateMachine context, MainCharacterStateFactory factory) : base(context, factory) {}

        public abstract override void OnStateEnter();
        public abstract override void OnStateExit();

        public abstract override bool TrySwitchState(out MainCharacterState newMainCharacterState);
    }
}