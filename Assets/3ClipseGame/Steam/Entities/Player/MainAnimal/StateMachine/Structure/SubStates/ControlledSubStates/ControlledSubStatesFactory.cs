namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.ControlledSubStates
{
    public class ControlledSubStatesFactory : AnimalSubStateFactory
    {
        public ControlledSubStatesFactory(MainAnimalStateMachine context) : base(context){}

        public AnimalSubState<ControlledSubStatesFactory> Idle() => new ControlledIdleSubState(Context, this);

        public AnimalSubState<ControlledSubStatesFactory> Walk() => new ControlledWalkSubState(Context, this);

        public AnimalSubState<ControlledSubStatesFactory> Run() => new ControlledRunSubState(Context, this);

        public AnimalSubState<ControlledSubStatesFactory> Jump() => new ControlledJumpSubState(Context, this);
        
        public AnimalSubState<ControlledSubStatesFactory> Fall() => new ControlledFallSubState(Context, this);
        
        public AnimalSubState<ControlledSubStatesFactory> Stop() => new ControlledStopSubState(Context, this);
        
        public AnimalSubState<ControlledSubStatesFactory> Crouch() => new ControlledCrouchSubState(Context, this);
    }
}