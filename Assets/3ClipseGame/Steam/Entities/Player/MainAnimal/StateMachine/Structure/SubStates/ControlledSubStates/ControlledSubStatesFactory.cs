using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.ControlledSubStates
{
    public class ControlledSubStatesFactory : AnimalSubStateFactory
    {
        #region Initialization

        public ControlledSubStatesFactory(MainAnimalStateMachine context) : base(context){}

        #endregion

        #region Methods

        public AnimalSubState Idle() => new ControlledIdleSubState(Context, this);
        public AnimalSubState Walk() => new ControlledWalkSubState(Context, this);
        public AnimalSubState Run() => new ControlledRunSubState(Context, this);
        public AnimalSubState Jump() => new ControlledJumpSubState(Context, this);
        public AnimalSubState Fall() => new ControlledFallSubState(Context, this);
        public AnimalSubState Stop() => new ControlledStopSubState(Context, this);
        public AnimalSubState Crouch() => new ControlledCrouchSubState(Context, this);

        #endregion
    }
}