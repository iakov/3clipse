using System.Runtime.Serialization;
using _3ClipseGame.Steam.Core.GameSource.Parts.Input.Inputs.MovementInput;
using _3ClipseGame.Steam.Core.GameSource.Parts.Player.Specifications.InGame;
using _3ClipseGame.Steam.Entities.Player.Scripts;
using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;
using CharacterController = _3ClipseGame.Steam.Entities.Scripts.CustomController.CharacterController;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter.StateMachine.Structure.Explore
{
    public class ExploreDto : Dto
    {
        [Header("Global")] 
        [SerializeField] private MovementInputProcessor _movementInputProcessor;
        [SerializeField] private PlayerMover _playerMover;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Stamina _stamina;
        [SerializeField] private Animator _animator;
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
        public PlayerMover PlayerMover => _playerMover;
        public CharacterController PlayerController => _characterController;

        public Stamina Stamina => _stamina;
        public Animator CharacterAnimator => _animator;
        
        public Vector3 LastMove { get; private set; }

        private void Awake()
        {
            CheckForExceptions();
        }
        
        private void CheckForExceptions()
        {
            if(_animator == null) throw new SerializationException("Animator cannot be null");
            if(_stamina == null) throw new SerializationException("Stamina cannot be null");
            if(_characterController == null) throw new SerializationException("CharacterController cannot be null");
            if(_playerMover == null) throw new SerializationException("PlayerMover cannot be null");
            if(_movementInputProcessor == null) throw new SerializationException("MovementInputProcessor cannot be null");
            if (RunModifierCurve.length < 2) throw new SerializationException("RunModifierCurve wrong function");
            if (SlideModifierCurve.length < 2) throw new SerializationException("SlideModifierCurve wrong function");
        }

        public void SaveLastMove(bool isRotated)
        {
            var playerMover = PlayerMover;
            var lastMove = playerMover.GetLastMove(MoveType.StateMove, isRotated);
            lastMove.y = 0f;
            LastMove = lastMove;
        }

        public void SwitchStaminaRecovery(bool isRecovering)
        {
            Stamina.IsRecovering = isRecovering;
        }
    }
}
