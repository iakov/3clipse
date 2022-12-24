using _3ClipseGame.Steam.GameCore.Origin.Parts.Player.Scripts;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Player.MainCharacter.StateMachine.Structure
{
    public abstract class MainCharacterState : State<MainCharacterStateFactory, MainCharacterState>
    {
        protected MainCharacterState(MainCharacterStateFactory factory) : base(factory){}
    }
}