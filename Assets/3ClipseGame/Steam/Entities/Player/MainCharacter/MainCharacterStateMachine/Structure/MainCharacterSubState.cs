using _3ClipseGame.Steam.Entities.Player.Scripts;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure
{
    public abstract class MainCharacterSubState : State<MainCharacterSubState, MainCharacterSubStateFactory, MainCharacterStateMachine>
    {
        protected MainCharacterSubState(MainCharacterStateMachine context, MainCharacterSubStateFactory factory) : base(context, factory){}
        
        public abstract override void OnStateEnter();
        
        public abstract override void OnStateExit();

        public abstract override bool TrySwitchState(out MainCharacterSubState newState);
    }
}
