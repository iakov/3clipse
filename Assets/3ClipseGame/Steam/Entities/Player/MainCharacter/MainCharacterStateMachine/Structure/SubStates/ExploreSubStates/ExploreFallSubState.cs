using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.States;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.SubStates.ExploreSubStates
{
    public class ExploreFallSubState : MainCharacterSubState
    {
        #region Initialization
        public ExploreFallSubState(MainCharacterStateMachine context, MainCharacterSubStateFactory factory) : base(context, factory) =>
            _factory = (ExploreSubStatesFactory) factory;
        
        private ExploreSubStatesFactory _factory;
        
        #endregion

        #region SubStateMethods

        public override void OnStateEnter(){}

        public override void OnStateUpdate(){}

        public override void OnStateExit(){}

        public override bool TrySwitchState(out MainCharacterState newMainCharacterState)
        {
            newMainCharacterState = null;
            
            if (Context.MainCharacter.IsGrounded) newMainCharacterState = _factory.Idle();

            return newMainCharacterState != null;
        }

        #endregion
    }
}