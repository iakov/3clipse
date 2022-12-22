using _3ClipseGame.Steam.Core.GameSource.Player.Scripts;

namespace _3ClipseGame.Steam.Core.GameSource.Player.MainAnimal.StateMachine.Structure
{
    public abstract class AnimalState : State<AnimalStateFactory, AnimalState>
    {
        protected AnimalState(AnimalStateFactory factory) : base(factory){}
    }
}
