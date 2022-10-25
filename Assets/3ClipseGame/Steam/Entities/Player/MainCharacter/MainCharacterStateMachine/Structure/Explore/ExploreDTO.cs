using System;
using _3ClipseGame.Steam.Core.GameSource.Parts.Input.Inputs.MovementInput;
using _3ClipseGame.Steam.Entities.Player.Data.Specifications.InGame;
using _3ClipseGame.Steam.Entities.Player.Scripts;
using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;
using CharacterController = _3ClipseGame.Steam.Entities.Scripts.CustomController.CharacterController;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.Explore
{
    public class ExploreDto : Dto
    {
        [Header("Global")] 
        [SerializeField] private MovementInputProcessor _movementInputProcessor;
        [Header("Explore Parameters")] 
        [Space]
        [Header("Walk")]
        [Range(0, 10)] [SerializeField] private float _walkSpeed = 3f;
        [Range(1, 10)] [SerializeField] private float _speedInterpolation = 6f;
        [Header("Crouch")]
        [Range(0, 2)] [SerializeField] private float _crouchSpeedModifier = 0.5f;
        [Header("Jump")]
        [SerializeField] private float _jumpStaminaReduce = -5f;
        [SerializeField] private float _jumpStrength = 2f;
        [Header("Run")]
        [SerializeField] private float _runStaminaReduce = -5f;
        [SerializeField] [Range(0, 1)] private float _minRunEntryStamina = 0.3f;
        [SerializeField] private AnimationCurve _runModifierCurve;
        [Header("Slide")]
        [SerializeField] private AnimationCurve _slideModifierCurve;

        public float WalkSpeed => _walkSpeed;
        public float SpeedInterpolation => _speedInterpolation;
        public float CrouchSpeedModifier => _crouchSpeedModifier;
        public float JumpStrength => _jumpStrength;
        public float RunStaminaReduce => _runStaminaReduce;
        public float MinRunEntryStamina => _minRunEntryStamina;
        public float JumpStaminaReduce => _jumpStaminaReduce;
        public AnimationCurve RunModifierCurve => _runModifierCurve;
        public AnimationCurve SlideModifierCurve => _slideModifierCurve;
        public MovementInputProcessor InputProcessor => _movementInputProcessor;

        public PlayerMover PlayerMover { get; private set; }
        public MainCharacter MainCharacter { get; private set; }
        public CharacterController PlayerController { get; private set; }
        public Transform Transform { get; private set; }
        public Stamina Stamina { get; private set; }
        public Animator CharacterAnimator { get; private set; }

        private void Awake()
        {
            PlayerController = GetComponent<CharacterController>();
            PlayerMover = GetComponent<PlayerMover>();
            MainCharacter = GetComponent<MainCharacter>();
            Stamina = GetComponent<Stamina>();
            CharacterAnimator = GetComponentInChildren<Animator>();
            
            Transform = PlayerController.transform;
        }

        private void Start()
        {
            CheckForExceptions();
        }
        
        private void CheckForExceptions()
        {
            if (RunModifierCurve.length <= 1) throw new ArgumentException("RunModifierCurve wrong function");
            if (SlideModifierCurve.length <= 1) throw new ArgumentException("SlideModifierCurve wrong function");
        }
    }
}
