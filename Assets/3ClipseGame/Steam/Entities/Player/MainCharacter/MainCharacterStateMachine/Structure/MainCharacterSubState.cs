using _3ClipseGame.Steam.Entities.Player.Scripts;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure
{
    public abstract class MainCharacterSubState<TFactory, TReturn> : SubState<TFactory, TReturn>
        where TFactory : SubStateFactory
    {
        protected MainCharacterSubState(TFactory factory) : base(factory){}
    }
}