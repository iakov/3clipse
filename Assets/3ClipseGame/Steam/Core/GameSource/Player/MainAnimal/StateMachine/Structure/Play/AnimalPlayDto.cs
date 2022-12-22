using System.Runtime.Serialization;
using _3ClipseGame.Steam.Core.GameSource.Parts.Input.Inputs.MovementInput;
using _3ClipseGame.Steam.Core.GameSource.Parts.Player.Specifications.InGame;
using _3ClipseGame.Steam.Core.GameSource.Player.Scripts;
using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;
using UnityEngine.AI;
using CharacterController = _3ClipseGame.Steam.Entities.Scripts.CustomController.CharacterController;

namespace _3ClipseGame.Steam.Core.GameSource.Player.MainAnimal.StateMachine.Structure.Play
{
    public class AnimalPlayDto : Dto
    {
        [SerializeField] private MovementInputProcessor _movementInputProcessor;
        
        [Header("Walk")]
        [Range(1, 10)] [SerializeField] private float _speedInterpolation = 6f;
        [SerializeField] private float _walkSpeed;
        
        [Header("Run")]
        [SerializeField] private float _runStaminaReduce = -5f;
        [SerializeField] private AnimationCurve _runSpeedCurve;
        
        [Header("Jump")]
        [SerializeField] private float _jumpStrength;
        [SerializeField] private float _jumpStaminaReduce = -5f;

        [Header("Crouch")]
        [Range(0, 3)] [SerializeField] private float _crouchSpeedModifier = 1f;
        
        
        public float WalkSpeed => _walkSpeed;
        public AnimationCurve RunSpeedCurve => _runSpeedCurve;
        public float RunStaminaReduce => _runStaminaReduce;
        public float JumpStaminaReduce => _jumpStaminaReduce;
        public float JumpStrength => _jumpStrength;
        public float SpeedInterpolation => _speedInterpolation;
        public float CrouchSpeedModifier => _crouchSpeedModifier;
        
        public NavMeshAgent AnimalAgent { get; private set; }
        public CharacterController AnimalController { get; private set; }
        public PlayerMover AnimalMover { get; private set; }
        public Transform AnimalTransform { get; private set; }
        public Stamina Stamina { get; private set; }
        public MovementInputProcessor MovementInputProcessor => _movementInputProcessor;

        private void Awake()
        {
            AnimalAgent = GetComponent<NavMeshAgent>();
            AnimalController = GetComponent<CharacterController>();
            AnimalMover = GetComponent<PlayerMover>();
            Stamina = GetComponent<Stamina>();
            AnimalTransform = transform;
            
            CheckForSerialization();
        }

        private void CheckForSerialization()
        {
            if (AnimalAgent == null) throw new SerializationException("NavMesh wasn't found");
            if( AnimalController == null) throw new SerializationException("CharacterController wasn't found");
            if (AnimalMover == null) throw new SerializationException("PlayerMover wasn't found");
            if (Stamina == null) throw new SerializationException("Stamina wasn't found");
            if(MovementInputProcessor == null) throw new SerializationException("Movement Input Processor must be initiated");
            if(RunSpeedCurve.length < 2) throw new SerializationException("RunSpeed curve is not initialized");
        }
    }
}