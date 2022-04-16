using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates.ExploreSubStates;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.States
{
    public class ExploreState : State
    {
        public ExploreState(PlayerStateMachine context, StateFactory factory) : base(context, factory){}

        private ExploreSubStatesFactory _subStateFactory;
        private SubState _currentSubState;

        public override void OnStateEnter()
        {
            _subStateFactory = new ExploreSubStatesFactory(Context);
            _currentSubState = _subStateFactory.Idle();
            _currentSubState.OnStateEnter();
        }

        public override void OnStateUpdate()
        {
            AddTime(Time.deltaTime);
            if (_currentSubState.TrySwitchState(out var nextSubState)) SwitchSubState((SubState)nextSubState);
            _currentSubState.OnStateUpdate();
        }
        
        public override void OnStateExit(){}

        public override bool TrySwitchState(out State newState)
        {
            newState = null;
            return false;
        }

        private void SwitchSubState(SubState nextSubState)
        {
            _currentSubState.OnStateExit();
            _currentSubState = nextSubState;
            _currentSubState.OnStateEnter();
        }
    }
}
