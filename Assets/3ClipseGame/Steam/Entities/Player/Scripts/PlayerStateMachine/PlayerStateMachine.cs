using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates;
using UnityEngine;
using UnityEngine.Events;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine
{
    public class PlayerStateMachine : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private UnityEvent<State, bool> switchingState;
        [SerializeField] private UnityEvent<SubState, bool> switchingSubState;

        #endregion
        
        #region PublicGetters

        public CharacterController PlayerController { get; private set; }
        
        public event UnityAction<State, bool> SwitchingState
        {
            add => switchingState.AddListener(value);
            remove => switchingState.RemoveListener(value);
        }

        public event UnityAction<SubState, bool> SwitchingSubState
        {
            add => switchingSubState.AddListener(value);
            remove => switchingSubState.RemoveListener(value);
        }

        #endregion

        #region PrivateFields

        private State _currentState;
        private StateFactory _stateFactory;

        #endregion
        
        #region MonoBehaviourMethods
        
        private void Awake()
        {
            PlayerController = GetComponent<CharacterController>();
            
            _stateFactory = new StateFactory(this);
            _currentState = _stateFactory.ExploreState();
            _currentState.OnStateEnter();
        }

        private void Update()
        {
            if (_currentState.TrySwitchState(out var nextState)) SwitchState(nextState);
            _currentState.OnStateUpdate();
        }

        #endregion

        #region Functions

        private void SwitchState(State nextState)
        {
            _currentState.OnStateExit();
            _currentState = nextState;
            _currentState.OnStateEnter();
        }

        #endregion
    }
}
