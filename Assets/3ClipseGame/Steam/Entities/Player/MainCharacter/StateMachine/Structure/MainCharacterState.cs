using _3ClipseGame.Steam.Entities.Player.Scripts;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.StateMachine.Structure
{
    public abstract class MainCharacterState : State<MainCharacterStateFactory, MainCharacterState>
    {
        protected MainCharacterState(MainCharacterStateFactory factory) : base(factory){}
    }
}