using System;
using _3ClipseGame.Steam.Core.Input.PlayerInput;
using _3ClipseGame.Steam.Entities.Player.Data.Specifications;
using _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.MainCharacter;
using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;
using UnityEngine.AI;
using CharacterController = _3ClipseGame.Steam.Entities.Scripts.CustomController.CharacterController;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(PlayerMover))]
    public class MainAnimalStateMachine : MonoBehaviour
    {
        #region SerializeFields

        [Header("Global Parameters")]
        [SerializeField] private Transform _mainCharacterTransform;
        
        [Header("Uncontrolled State Parameters")]
        [Header("Idle")]
        [SerializeField] private float _waitTime;
        
        [Header("Walk")]
        [SerializeField] private float _followWalkDistance;
        [SerializeField] private AnimationCurve _followWalkSpeed;
        [SerializeField] private float _stopWalkDistance;
        [SerializeField] private Transform[] _possibleFollowTargets;
        
        [Header("Walk Back")]
        [SerializeField] private float _walkBackDistance;
        [SerializeField] private AnimationCurve _walkBackSpeed;

        [Header("Run")]
        [SerializeField] private float _followRunDistance;
        [SerializeField] private AnimationCurve _followRunSpeed;

        [Header("Controlled State Parameters")] 
        [Header("Walk")]
        [Range(1, 10)] [SerializeField] private float _speedInterpolation = 6f;
        [SerializeField] private float _walkSpeed;
        
        [Header("Run")]
        [SerializeField] private float _runStaminaReduce = -5f;
        [SerializeField] private AnimationCurve _runSpeed;
        
        [Header("Jump")]
        [SerializeField] private float _jumpStrength;
        [SerializeField] private float _jumpStaminaReduce = -5f;

        [Header("Crouch")]
        [Range(0, 3)] [SerializeField] private float _crouchSpeedModifier = 1f;

        #endregion

        #region PublicGetters

        public float WaitTime => _waitTime;
        public float StopWalkDistance => _stopWalkDistance;
        public float WalkBackDistance => _walkBackDistance;
        public float FollowWalkDistance => _followWalkDistance;
        public float FollowRunDistance => _followRunDistance;
        public float WalkSpeed => _walkSpeed;
        public AnimationCurve RunSpeed => _runSpeed;
        public float RunStaminaReduce => _runStaminaReduce;
        public float JumpStaminaReduce => _jumpStaminaReduce;
        public float JumpStrength => _jumpStrength;
        public float SpeedInterpolation => _speedInterpolation;
        public float CrouchSpeedModifier => _crouchSpeedModifier;
        public bool IsSwitching { get; private set; }
        public Transform MainCharacterTransform => _mainCharacterTransform;
        public Transform AnimalTransform { get; private set; }
        public Transform CurrentTarget { get; set; }
        public Transform[] PossibleFollowTargets => _possibleFollowTargets;
        public AnimationCurve FollowWalkSpeed => _followWalkSpeed;
        public AnimationCurve FollowRunSpeed => _followRunSpeed;
        public AnimationCurve WalkBackSpeed => _walkBackSpeed;
        public CharacterController MainCharacterController { get; private set; }
        public CharacterController AnimalController { get; private set; }
        public PlayerMover AnimalMover { get; private set; }
        public NavMeshAgent AnimalAgent { get; private set; }
        public MovementInputProcessor InputHandler { get; private set; }
        public Stamina Stamina { get; private set; }

        #endregion
        
        #region PrivateFields

        private AnimalState _currentMainAnimalState;
        private AnimalStateFactory _mainAnimalStateFactory;

        #endregion

        #region MonoBehaviourMethods

        private void Awake()
        {
            InputHandler = new MovementInputProcessor(GetComponentInParent<MovementInputHandler>());
            AnimalMover = GetComponent<PlayerMover>();
            AnimalTransform = GetComponent<Transform>();
            AnimalAgent = GetComponent<NavMeshAgent>();
            AnimalController = GetComponent<CharacterController>();
            Stamina = GetComponent<Stamina>();
            MainCharacterController = MainCharacterTransform.GetComponent<CharacterController>();
        }

        private void Start()
        {
            CheckForSerialization();
            
            _mainAnimalStateFactory = new AnimalStateFactory(this);
            _currentMainAnimalState = _mainAnimalStateFactory.UncontrolledState();
            _currentMainAnimalState.OnStateEnter();
        }

        #endregion

        #region PublicMethods

        public void UpdateWork()
        {
            if(_currentMainAnimalState.TrySwitchState(out var newState)) SwitchState(newState);
            _currentMainAnimalState.OnStateUpdate();
        }

        public void SetSwitch(MainCharacterState oldState, MainCharacterState newState)
        {
            if(newState.GetType() == typeof(ExploreMainCharacterState) && oldState.GetType() == typeof(AnimalControlState)) IsSwitching = true;
            else if(newState.GetType() == typeof(AnimalControlState) && oldState.GetType() == typeof(ExploreMainCharacterState)) IsSwitching = true;
        }

        #endregion

        #region PrivateMethods

        private void SwitchState(AnimalState newState)
        {
            _currentMainAnimalState.OnStateExit();
            _currentMainAnimalState = newState;
            _currentMainAnimalState.OnStateEnter();

            IsSwitching = false;
        }

        private void CheckForSerialization()
        {
            if(PossibleFollowTargets.Length == 0) throw new ArgumentException("Amount of follow targets cannot be zero");
            if (FollowWalkSpeed.length < 2) throw new ArgumentException("FollowWalkSpeed curve is not initialized");
            if(FollowRunSpeed.length < 2) throw new ArgumentException("FollowRunSpeed curve is not initialized");
            if(WalkBackSpeed.length < 2) throw new ArgumentException("WalkBackSpeed curve is not initialized");
            if(RunSpeed.length < 2) throw new ArgumentException("RunSpeed curve is not initialized");

        }

        #endregion
    }
}
