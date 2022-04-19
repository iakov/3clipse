using System;
using _3ClipseGame.Steam.Entities.Player.Scripts.GlobalScripts;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Input;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine.Structure.SubStates;
using UnityEngine;
using UnityEngine.Events;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerStateMachine
{
    
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(MovementInputHandler))]
    public class PlayerStateMachine : MonoBehaviour
    {
        #region SerializeFields

        [Header("Explore Parameters" )]
        [Space]
        [Range(0, 10)] [SerializeField] private float walkSpeed = 3f;
        [Range(1, 10)] [SerializeField] private float speedInterpolation = 6f;
        [Range(0, 3)] [SerializeField] private float crouchSpeedModifier = 1f;
        [SerializeField] private AnimationCurve runModifierCurve;
        [SerializeField] private UnityEvent<State, bool> switchingState;
        [SerializeField] private UnityEvent<SubState, bool> switchingSubState;

        #endregion
        
        #region PublicGetters

        public uint MoveRotatePriority { get; } = 5;
        public float WalkSpeed => walkSpeed;
        public float SpeedInterpolation => speedInterpolation;
        public float CrouchSpeedModifier => crouchSpeedModifier;
        public AnimationCurve RunModifierCurve => runModifierCurve;
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
        
        public PlayerMover PlayerMover { get; private set; }
        public CharacterController PlayerController { get; private set; }
        public MovementInputHandler InputHandler { get; private set; }
        public Transform Transform { get; private set; }

        #endregion

        #region PrivateFields

        private State _currentState;
        private StateFactory _stateFactory;

        #endregion
        
        #region PublicMethods
        
        public void Start()
        {
            PlayerController = GetComponent<CharacterController>();
            InputHandler = GetComponent<MovementInputHandler>();
            PlayerMover = GetComponent<PlayerMover>();
            Transform = GetComponent<Transform>();
            
            CheckForExceptions();
            _stateFactory = new StateFactory(this);
            _currentState = _stateFactory.ExploreState();
            _currentState.OnStateEnter();
        }

        public void UpdateWork()
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

        private void CheckForExceptions()
        {
            if (RunModifierCurve.length <= 1) throw new ArgumentException("SpeedUpCurve wrong function");
        }

        #endregion
    }
}
