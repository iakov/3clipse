using Assets._3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.States;
using Assets._3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates;
using Assets._3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates.ExploreSubStates;
using UnityEngine;

namespace Assets._3ClipseGame.Steam.Entities.Player.Scripts.Visuals.Scripts.AnimationsControllers
{
    public class PlayerAnimationsController : MonoBehaviour
    {
        #region Initialization

        private Animator _animator;
        private StateAnimationsHandler _currentAnimationsHandler;
        
        #endregion

        #region MonoBehaviourMethods

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _currentAnimationsHandler = new ExploreAnimationsHandler(_animator);
            _currentAnimationsHandler.OnStateEnter();
        }

        #endregion

        #region EventHandlers

        public void OnStateChanged(State currentState, State nextState)
        {
            _currentAnimationsHandler.OnStateExit();
            
            if (nextState.GetType() == typeof(ExploreState)) _currentAnimationsHandler = new ExploreAnimationsHandler(_animator);
            
            _currentAnimationsHandler.OnStateEnter();
        }

        public void OnSubStateChanged(SubState currentSubState, SubState newSubState)
        {
            _currentAnimationsHandler.ApplyLeavingSubState(currentSubState);
            _currentAnimationsHandler.ApplyEnteringSubState(newSubState);
        }

        #endregion
    }
}
