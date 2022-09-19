using System;
using _3ClipseGame.Steam.Entities.Player.Data.Specifications;
using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.SubStates;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using _3ClipseGame.Steam.Global.Input.PlayerInput;
using UnityEngine;
using UnityEngine.Events;
using CharacterController = _3ClipseGame.Steam.Entities.Scripts.CustomController.CharacterController;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine
{
    [RequireComponent(typeof(CharacterController))]
    public class MainCharacterStateMachine : MonoBehaviour
    {
        #region SerializeFields

        [Header("Explore Parameters")] 
        [Space]
        [Header("Walk")]
        [Range(0, 10)] [SerializeField] private float walkSpeed = 3f;
        [Range(1, 10)] [SerializeField] private float speedInterpolation = 6f;
        [Header("Crouch")]
        [Range(0, 2)] [SerializeField] private float crouchSpeedModifier = 0.5f;
        [Header("Jump")]
        [SerializeField] private float jumpStaminaReduce = -5f;
        [SerializeField] private float jumpStrength = 2f;
        [Header("Run")]
        [SerializeField] private float runStaminaReduce = -5f;
        [SerializeField] [Range(0, 1)] private float minRunEntryStamina = 0.3f;
        [SerializeField] private AnimationCurve runModifierCurve;
        [Header("Slide")]
        [SerializeField] private AnimationCurve slideModifierCurve;
        [Space]
        [Space]
        [Header("Events")]
        [SerializeField] private UnityEvent<MainCharacterState, MainCharacterState> switchingState;
        [SerializeField] private UnityEvent<MainCharacterSubState, MainCharacterSubState> switchingSubState;

        #endregion
        
        #region PublicGetters
        
        public float WalkSpeed => walkSpeed;
        public float SpeedInterpolation => speedInterpolation;
        public float CrouchSpeedModifier => crouchSpeedModifier;
        public float JumpStrength => jumpStrength;
        public float RunStaminaReduce => runStaminaReduce;
        public float MinRunEntryStamina => minRunEntryStamina;
        public float JumpStaminaReduce => jumpStaminaReduce;
        public AnimationCurve RunModifierCurve => runModifierCurve;
        public AnimationCurve SlideModifierCurve => slideModifierCurve;

        public PlayerMover PlayerMover { get; private set; }
        public MainCharacter MainCharacter { get; private set; }
        public CharacterController PlayerController { get; private set; }
        public MovementInputHandler InputHandler { get; private set; }
        public Transform Transform { get; private set; }
        public Stamina Stamina { get; private set; }
        public Animator CharacterAnimator { get; private set; }
        
        public event UnityAction<MainCharacterState, MainCharacterState> SwitchingState
        {
            add => switchingState.AddListener(value);
            remove => switchingState.RemoveListener(value);
        }

        public event UnityAction<MainCharacterSubState, MainCharacterSubState> SwitchingSubState
        {
            add => switchingSubState.AddListener(value);
            remove => switchingSubState.RemoveListener(value);
        }

        #endregion

        #region PrivateFields

        private MainCharacterState _currentMainCharacterState;
        private MainCharacterStateFactory _mainCharacterStateFactory;

        #endregion

        #region MonoBehaviourMethods

        private void Awake()
        {
            PlayerController = GetComponent<CharacterController>();
            InputHandler = GetComponentInParent<MovementInputHandler>();
            PlayerMover = GetComponent<PlayerMover>();
            Transform = PlayerController.transform;
            MainCharacter = GetComponent<MainCharacter>();
            Stamina = GetComponent<Stamina>();
            CharacterAnimator = GetComponentInChildren<Animator>();
            
            _mainCharacterStateFactory = new MainCharacterStateFactory(this);
            _currentMainCharacterState = _mainCharacterStateFactory.ExploreState();
        }

        private void Start()
        {
            CheckForExceptions();
            
            _currentMainCharacterState.OnStateEnter();
        }

        private void OnEnable() => _currentMainCharacterState.SwitchingSubState += SwitchSubState;
        private void OnDisable() => _currentMainCharacterState.SwitchingSubState -= SwitchSubState;
        

        #endregion

        #region PublicMethods

        public void UpdateWork()
        {
            if (_currentMainCharacterState == null) return;
            if (_currentMainCharacterState.TrySwitchState(out var nextState)) SwitchState(nextState);
            _currentMainCharacterState.OnStateUpdate();
        }

        #endregion

        #region PrivateMethods

        private void SwitchState(MainCharacterState nextMainCharacterState)
        {
            InputHandler.IsSwitchPressed = false;
            switchingState?.Invoke(_currentMainCharacterState, nextMainCharacterState);
            
            _currentMainCharacterState.OnStateExit();
            _currentMainCharacterState = nextMainCharacterState;
            _currentMainCharacterState.OnStateEnter();
        }

        private void SwitchSubState(MainCharacterSubState current, MainCharacterSubState next) => switchingSubState?.Invoke(current, next);

        private void CheckForExceptions()
        {
            if (RunModifierCurve.length <= 1) throw new ArgumentException("RunModifierCurve wrong function");
            if (SlideModifierCurve.length <= 1) throw new ArgumentException("SlideModifierCurve wrong function");
        }

        #endregion
    }
}
