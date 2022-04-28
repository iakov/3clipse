using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.SubStates;
using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.SubStates.ExploreSubStates;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.States
{
    public class ExploreMainCharacterState : MainCharacterState
    {
        #region Initialization

        public ExploreMainCharacterState(MainCharacterStateMachine context, MainCharacterStateFactory factory) : base(context, factory){}
        private ExploreSubStatesFactory _subStateFactory;
        private MainCharacterSubState _currentMainCharacterSubState;

        #endregion

        #region StateMethods

        public override void OnStateEnter()
        {
            _subStateFactory = new ExploreSubStatesFactory(Context);
            _currentMainCharacterSubState = _subStateFactory.Idle();
            _currentMainCharacterSubState.OnStateEnter();
        }

        public override void OnStateUpdate()
        {
            AddTime(Time.deltaTime);
            if (_currentMainCharacterSubState.TrySwitchState(out var nextSubState)) SwitchSubState((MainCharacterSubState)nextSubState);
            _currentMainCharacterSubState.OnStateUpdate();
        }
        
        public override void OnStateExit(){}

        public override bool TrySwitchState(out MainCharacterState newMainCharacterState)
        {
            newMainCharacterState = null;
            return false;
        }

        #endregion

        #region PrivateMethods

        private void SwitchSubState(MainCharacterSubState nextMainCharacterSubState)
        {
            SwitchSubState(_currentMainCharacterSubState, nextMainCharacterSubState);
            _currentMainCharacterSubState.OnStateExit();
            _currentMainCharacterSubState = nextMainCharacterSubState;
            _currentMainCharacterSubState.OnStateEnter();
        }

        #endregion
    }
}
