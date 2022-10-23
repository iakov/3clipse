using System;
using _3ClipseGame.Steam.Core.GameSource.Parts.Input.Inputs.MovementInput;
using _3ClipseGame.Steam.Entities.Player.Data.Specifications.InGame;
using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure;
using _3ClipseGame.Steam.Entities.Player.Scripts;
using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;
using CharacterController = _3ClipseGame.Steam.Entities.Scripts.CustomController.CharacterController;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine
{
    [RequireComponent(typeof(CharacterController))]
    public class MainCharacterStateMachine : StateMachine
    {
        [Header("Global")] 
        [SerializeField] private MovementInputProcessor _movementInputProcessor;
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

        public float WalkSpeed => walkSpeed;
        public float SpeedInterpolation => speedInterpolation;
        public float CrouchSpeedModifier => crouchSpeedModifier;
        public float JumpStrength => jumpStrength;
        public float RunStaminaReduce => runStaminaReduce;
        public float MinRunEntryStamina => minRunEntryStamina;
        public float JumpStaminaReduce => jumpStaminaReduce;
        public AnimationCurve RunModifierCurve => runModifierCurve;
        public AnimationCurve SlideModifierCurve => slideModifierCurve;
        public MovementInputProcessor InputProcessor => _movementInputProcessor;

        public PlayerMover PlayerMover { get; private set; }
        public MainCharacter MainCharacter { get; private set; }
        public CharacterController PlayerController { get; private set; }
        public Transform Transform { get; private set; }
        public Stamina Stamina { get; private set; }
        public Animator CharacterAnimator { get; private set; }

        private MainCharacterState _currentMainCharacterState;
        private MainCharacterStateFactory _mainCharacterStateFactory;


        private void Awake()
        {
            PlayerController = GetComponent<CharacterController>();
            PlayerMover = GetComponent<PlayerMover>();
            MainCharacter = GetComponent<MainCharacter>();
            Stamina = GetComponent<Stamina>();
            CharacterAnimator = GetComponentInChildren<Animator>();
            
            Transform = PlayerController.transform;
            
            _mainCharacterStateFactory = new MainCharacterStateFactory(this);
            _currentMainCharacterState = _mainCharacterStateFactory.ExploreState();
        }

        private void Start()
        {
            CheckForExceptions();
            
            _currentMainCharacterState.OnStateEnter();
        }

        public override void UpdateWork()
        {
            if (_currentMainCharacterState != null)
            {
                if (_currentMainCharacterState.TrySwitchState(out var nextState)) SwitchState(nextState);
                _currentMainCharacterState.OnStateUpdate();
            }
        }

        private void SwitchState(MainCharacterState nextMainCharacterState)
        {
            _currentMainCharacterState.OnStateExit();
            _currentMainCharacterState = nextMainCharacterState;
            _currentMainCharacterState.OnStateEnter();
        }

        private void CheckForExceptions()
        {
            if (RunModifierCurve.length <= 1) throw new ArgumentException("RunModifierCurve wrong function");
            if (SlideModifierCurve.length <= 1) throw new ArgumentException("SlideModifierCurve wrong function");
        }
    }
}
