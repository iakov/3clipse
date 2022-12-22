using _3ClipseGame.Steam.Core.GameSource.Player.Scripts;

namespace _3ClipseGame.Steam.Core.GameSource.Player.MainCharacter.StateMachine.Structure
{
    public abstract class MainCharacterSubState<TFactory, TReturn> : SubState<TFactory, TReturn>
        where TFactory : SubStateFactory
    {
        protected MainCharacterSubState(TFactory factory) : base(factory){}
    }
}