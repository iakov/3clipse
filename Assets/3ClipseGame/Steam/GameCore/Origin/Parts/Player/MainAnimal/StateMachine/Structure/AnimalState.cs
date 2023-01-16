using _3ClipseGame.Steam.GameCore.Origin.Parts.Player.Scripts;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Player.MainAnimal.StateMachine.Structure
{
    public abstract class AnimalState : State<AnimalStateFactory, AnimalState>
    {
        protected AnimalState(AnimalStateFactory factory) : base(factory){}
    }
}
