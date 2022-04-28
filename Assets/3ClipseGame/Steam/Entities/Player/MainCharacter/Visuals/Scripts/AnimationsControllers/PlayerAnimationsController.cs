using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.SubStates;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.Visuals.Scripts.AnimationsControllers
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

        public void OnStateChanged(MainCharacterState currentMainCharacterState, MainCharacterState nextMainCharacterState)
        {
            _currentAnimationsHandler.OnStateExit();
            
            if (nextMainCharacterState.GetType() == typeof(ExploreMainCharacterState)) _currentAnimationsHandler = new ExploreAnimationsHandler(_animator);
            
            _currentAnimationsHandler.OnStateEnter();
        }

        public void OnSubStateChanged(MainCharacterSubState currentMainCharacterSubState, MainCharacterSubState newMainCharacterSubState)
        {
            _currentAnimationsHandler.ApplyLeavingSubState(currentMainCharacterSubState);
            _currentAnimationsHandler.ApplyEnteringSubState(newMainCharacterSubState);
        }

        #endregion
    }
}
