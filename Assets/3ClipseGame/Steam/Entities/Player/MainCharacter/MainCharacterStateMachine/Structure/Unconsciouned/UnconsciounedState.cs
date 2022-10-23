namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.Unconsciouned
{
    public class AnimalControlState : MainCharacterState
    {
        public AnimalControlState(MainCharacterStateMachine context, MainCharacterStateFactory factory) : base(context, factory){}
        private int _framesFromSwitch;

        public override void OnStateEnter()
        {
            
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            _framesFromSwitch++;
        }

        public override void OnStateExit()
        {
            
        }

        public override bool TrySwitchState(out MainCharacterState newMainCharacterState)
        {
            newMainCharacterState = null;

            if (Context.InputProcessor.GetIsSwitched() && _framesFromSwitch >= 2) newMainCharacterState = Factory.ExploreState();

            return newMainCharacterState != null;
        }
    }
}