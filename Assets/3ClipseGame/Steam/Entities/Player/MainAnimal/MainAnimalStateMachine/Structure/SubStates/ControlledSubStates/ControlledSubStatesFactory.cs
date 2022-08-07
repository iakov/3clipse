namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates.ControlledSubStates
{
    public class ControlledSubStatesFactory : AnimalSubStateFactory
    {
        public ControlledSubStatesFactory(MainAnimalStateMachine context) : base(context){}

        public AnimalSubState Idle() => new ControlledIdleSubState(Context, this);
        public AnimalSubState Walk() => new ControlledWalkSubState(Context, this);
        public AnimalSubState Run() => new ControlledRunSubState(Context, this);
    }
}