namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.States
{
    public class ControlledState : AnimalState
    {
        public ControlledState(MainAnimalStateMachine context, AnimalStateFactory factory) : base(context, factory){}

        public override void OnStateEnter()
        {
            
        }

        public override void OnStateUpdate()
        {
            
        }

        public override void OnStateExit()
        {
            
        }

        public override bool TrySwitchState(out AnimalState newAnimalState)
        {
            newAnimalState = null;
            
            return newAnimalState != null;
        }
    }
}
