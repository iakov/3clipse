using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.States;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates
{
    public abstract class AnimalSubState : AnimalState
    {
        protected AnimalSubState(MainAnimalStateMachine context, AnimalStateFactory factory) : base(context, factory){}
    }
}
