using _3ClipseGame.Steam.Entities.Player.Scripts;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.States
{
    public class AnimalStateFactory : StateFactory<MainAnimalStateMachine>
    {
        public AnimalStateFactory(MainAnimalStateMachine context) : base(context){}

        public AnimalState ControlledState() => new ControlledState(Context, this);
        public AnimalState UncontrolledState() => new UncontrolledState(Context, this);
    }
}
