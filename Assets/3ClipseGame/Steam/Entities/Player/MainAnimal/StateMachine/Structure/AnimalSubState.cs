using _3ClipseGame.Steam.Entities.Player.Scripts;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure
{
    public abstract class AnimalSubState<TFactory, TReturn> : SubState<TFactory, TReturn>
        where TFactory : AnimalSubStateFactory
    {
        protected AnimalSubState(TFactory factory) : base(factory){}
    }
}
