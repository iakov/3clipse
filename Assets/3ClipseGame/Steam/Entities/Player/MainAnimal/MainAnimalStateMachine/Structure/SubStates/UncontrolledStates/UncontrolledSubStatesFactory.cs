namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates.UncontrolledStates
{
    public class UncontrolledSubStatesFactory : AnimalSubStateFactory
    {
        public UncontrolledSubStatesFactory(MainAnimalStateMachine context) : base(context){}

        public AnimalSubState Idle() => new UncontrolledIdleSubState(Context, this);
        public AnimalSubState Walk() => new UncontrolledWalkSubState(Context, this);
    }
}