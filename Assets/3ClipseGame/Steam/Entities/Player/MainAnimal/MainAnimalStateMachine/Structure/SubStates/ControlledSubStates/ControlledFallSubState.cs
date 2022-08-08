using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.States;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates.ControlledSubStates
{
    public class ControlledFallSubState : AnimalSubState
    {
        public ControlledFallSubState(MainAnimalStateMachine context, AnimalStateFactory factory) : base(context, factory) 
            => _factory = (ControlledSubStatesFactory) factory;

        private ControlledSubStatesFactory _factory;

        public override void OnStateEnter()
        {
            Context.Stamina.IsRecovering = false;
        }

        public override void OnStateUpdate(){}

        public override void OnStateExit()
        {
            Context.Stamina.IsRecovering = true;
        }

        public override bool TrySwitchState(out AnimalState newAnimalState)
        {
            newAnimalState = null;

            if (Context.AnimalController.IsGrounded) newAnimalState = _factory.Idle();

            return newAnimalState != null;
        }
    }
}