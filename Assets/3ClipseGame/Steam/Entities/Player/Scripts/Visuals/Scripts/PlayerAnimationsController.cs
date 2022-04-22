using Assets._3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.States;
using Assets._3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates;
using Assets._3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates.ExploreSubStates;
using UnityEngine;

namespace Assets._3ClipseGame.Steam.Entities.Player.Scripts.Visuals.Scripts
{
    public class PlayerAnimationsController : MonoBehaviour
    {
        #region Initialization

        private Animator _animator;
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int IsCrouching = Animator.StringToHash("IsCrouching");

        #endregion

        #region MonoBehaviourMethods

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        #endregion

        #region EventHandlers

        public void OnStateChanged(State currentState, State nextState)
        {
            
        }

        public void OnSubStateChanged(SubState currentSubState, SubState newSubState)
        {
            ApplyNewSubState(newSubState);
            ApplyCurrentSubState(currentSubState);
        }

        #endregion

        #region PrivateMethods

        private void ApplyCurrentSubState(SubState currentSubState)
        {
            if(currentSubState.GetType() == typeof(ExploreRunSubState)) _animator.SetBool(IsRunning, false);
        }

        private void ApplyNewSubState(SubState newSubState)
        {
            if (newSubState.GetType() == typeof(ExploreIdleSubState))
            {
                _animator.SetBool(IsWalking, false);
                _animator.SetBool(IsRunning, false);
                _animator.SetBool(IsCrouching, false);
            }
            else if(newSubState.GetType() == typeof(ExploreWalkSubState)) _animator.SetBool(IsWalking, true);
            else if (newSubState.GetType() == typeof(ExploreRunSubState)) _animator.SetBool(IsRunning, true);
            else if(newSubState.GetType() == typeof(ExploreCrouchSubState)) _animator.SetBool(IsCrouching, true);
        }

        #endregion
    }
}
