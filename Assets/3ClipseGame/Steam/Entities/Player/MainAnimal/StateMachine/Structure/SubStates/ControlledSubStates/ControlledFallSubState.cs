namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.ControlledSubStates
{
    public class ControlledFallSubState : ControlledSubState
    {
        public ControlledFallSubState(MainAnimalStateMachine context, ControlledSubStatesFactory factory) : base(context, factory) {}

        public override void OnStateEnter()
        {
            Context.Stamina.IsRecovering = false;
        }

        public override void OnStateExit()
        {
            Context.Stamina.IsRecovering = true;
        }

        public override bool TrySwitchState(out AnimalSubState<ControlledSubStatesFactory> newAnimalState)
        {
            newAnimalState = null;

            if (Context.AnimalController.IsGrounded) newAnimalState = Factory.Idle();

            return newAnimalState != null;
        }
    }
}