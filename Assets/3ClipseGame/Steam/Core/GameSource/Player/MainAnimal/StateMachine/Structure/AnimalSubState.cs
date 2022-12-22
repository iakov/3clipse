using _3ClipseGame.Steam.Core.GameSource.Player.Scripts;

namespace _3ClipseGame.Steam.Core.GameSource.Player.MainAnimal.StateMachine.Structure
{
    public abstract class AnimalSubState<TFactory, TReturn> : SubState<TFactory, TReturn>
        where TFactory : AnimalSubStateFactory
    {
        protected AnimalSubState(TFactory factory) : base(factory){}
    }
}
