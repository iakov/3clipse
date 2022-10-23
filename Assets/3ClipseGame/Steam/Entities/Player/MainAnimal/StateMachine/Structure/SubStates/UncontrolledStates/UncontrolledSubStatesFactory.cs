namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.UncontrolledStates
{
    public class UncontrolledSubStatesFactory : AnimalSubStateFactory
    {
        public UncontrolledSubStatesFactory(MainAnimalStateMachine context) : base(context){}

        public AnimalSubState<UncontrolledSubStatesFactory> Idle() => new UncontrolledIdleSubState(Context, this);
        public AnimalSubState<UncontrolledSubStatesFactory> Walk() => new UncontrolledFollowWalkSubState(Context, this);
        public AnimalSubState<UncontrolledSubStatesFactory> Run() => new UncontrolledFollowRunSubState(Context, this);
        public AnimalSubState<UncontrolledSubStatesFactory> Entertain() => new UncontrolledEntertainSubState(Context, this);
        public AnimalSubState<UncontrolledSubStatesFactory> WalkBack() => new UncontrolledWalkBackSubState(Context, this);
    }
}