using _3ClipseGame.Steam.Entities.Player.Scripts;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure
{
    public abstract class AnimalState : State<AnimalStateFactory, AnimalState>
    {
        protected AnimalState(AnimalStateFactory factory) : base(factory){}
    }
}
