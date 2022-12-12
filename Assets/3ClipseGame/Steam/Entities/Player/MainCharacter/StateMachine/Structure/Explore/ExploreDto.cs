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
        [SerializeField] private CapsuleCollider _capsuleCollider;

        [Header("Explore Parameters")] 
        [Space] 
        [Header("Walk")] 
        [SerializeField] private float _maxWalkSpeed = 2f;
        [SerializeField] private float _walkAngleDampTime = 0.2f;
        [Range(0, 10)] [SerializeField] private float _timeToMaxWalkSpeed = 1f;

        public float TimeToMaxWalkSpeed => _timeToMaxWalkSpeed;
        public float MaxWalkSpeed => _maxWalkSpeed;
        public float WalkAngleDampTime => _walkAngleDampTime;
        public MovementInputProcessor InputProcessor => _movementInputProcessor;
        public PlayerMover PlayerMover => _playerMover;
        public CharacterController PlayerController => _characterController;
        public CapsuleCollider PlayerCollider => _capsuleCollider;

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
