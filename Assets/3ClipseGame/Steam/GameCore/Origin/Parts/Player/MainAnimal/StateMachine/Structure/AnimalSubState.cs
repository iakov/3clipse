using _3ClipseGame.Steam.GameCore.Origin.Parts.Player.Scripts;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Player.MainAnimal.StateMachine.Structure
{
    public abstract class AnimalSubState<TFactory, TReturn> : SubState<TFactory, TReturn>
        where TFactory : AnimalSubStateFactory
    {
        protected AnimalSubState(TFactory factory) : base(factory){}
    }
}
