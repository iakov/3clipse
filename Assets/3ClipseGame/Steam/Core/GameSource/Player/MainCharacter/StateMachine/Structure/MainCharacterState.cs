using _3ClipseGame.Steam.Core.GameSource.Player.Scripts;

namespace _3ClipseGame.Steam.Core.GameSource.Player.MainCharacter.StateMachine.Structure
{
    public abstract class MainCharacterState : State<MainCharacterStateFactory, MainCharacterState>
    {
        protected MainCharacterState(MainCharacterStateFactory factory) : base(factory){}
    }
}