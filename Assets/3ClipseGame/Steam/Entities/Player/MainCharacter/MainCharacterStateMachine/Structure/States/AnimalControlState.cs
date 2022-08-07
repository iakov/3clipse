namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.States
{
    public class AnimalControlState : MainCharacterState
    {
        public AnimalControlState(MainCharacterStateMachine context, MainCharacterStateFactory factory) : base(context, factory){}

        public override void OnStateEnter()
        {
            Context.InputHandler.SwitchToAnimalControls();
        }

        public override void OnStateUpdate(){}

        public override void OnStateExit(){}

        public override bool TrySwitchState(out MainCharacterState newMainCharacterState)
        {
            newMainCharacterState = null;

            if (Context.InputHandler.IsSwitchPressed) newMainCharacterState = Factory.ExploreState();

            return newMainCharacterState != null;
        }
    }
}