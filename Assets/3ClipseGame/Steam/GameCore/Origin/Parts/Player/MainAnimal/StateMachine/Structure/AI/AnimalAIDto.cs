using System.Runtime.Serialization;
using _3ClipseGame.Steam.GameCore.GlobalScripts.EntityScripts.CharacterMover;
using _3ClipseGame.Steam.GameCore.Origin.Parts.Input.Inputs.MovementInput;
using _3ClipseGame.Steam.GameCore.Origin.Parts.Player.Scripts;
using UnityEngine;
using UnityEngine.AI;
using CharacterController = _3ClipseGame.Steam.GameCore.GlobalScripts.EntityScripts.CharacterController;
using Random = UnityEngine.Random;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Player.MainAnimal.StateMachine.Structure.AI
{
    public class AnimalAIDto : Dto
    {
        [Header("Global")]
        [SerializeField] private MovementInputProcessor _movementInputProcessor;
        [SerializeField] private Transform _mainCharacterTransform;
        
        [Header("Idle")]
        [SerializeField] private float _waitTimeBeforeEntertain;
        
        [Header("Walk")]
        [SerializeField] private float _startFollowWalkDistance;
        [SerializeField] private float _stopFollowWalkDistance;
        [SerializeField] private AnimationCurve _followWalkSpeedCurve;
        [SerializeField] private Transform[] _possibleFollowTargets;
        
        [Header("Walk Back")]
        [SerializeField] private float _startWalkBackDistance;
        [SerializeField] private float _endWalkBackDistance;
        [SerializeField] private AnimationCurve _walkBackSpeedCurve;

        [Header("Run")]
        [SerializeField] private float _minFollowRunDistance;
        [SerializeField] private AnimationCurve _followRunSpeedCurve;
        
        
        public float WaitTimeBeforeEntertain => _waitTimeBeforeEntertain;
        public float StopFollowWalkDistance => _stopFollowWalkDistance;
        public float StartWalkBackDistance => _startWalkBackDistance;
        public float EndWalkBackDistance => _endWalkBackDistance;
        public float StartFollowWalkDistance => _startFollowWalkDistance;
        public float MinFollowRunDistance => _minFollowRunDistance;
        public Transform[] PossibleFollowTargets => _possibleFollowTargets;
        public AnimationCurve FollowWalkSpeedCurve => _followWalkSpeedCurve;
        public AnimationCurve FollowRunSpeedCurve => _followRunSpeedCurve;
        public AnimationCurve WalkBackSpeedCurve => _walkBackSpeedCurve;
        
        public NavMeshAgent AnimalAgent { get; private set; }
        public PlayerMover AnimalMover { get; private set; }
        public Transform AnimalTransform { get; private set; }
        public Transform CurrentTarget { get; private set; }
        public MovementInputProcessor MovementInputProcessor => _movementInputProcessor;
        public CharacterController MainCharacterController { get; private set; }
        public Transform MainCharacterTransform => _mainCharacterTransform;

        private void Awake()
        {
            AnimalAgent = GetComponent<NavMeshAgent>();
            AnimalMover = GetComponent<PlayerMover>();
            AnimalTransform = transform;
            MainCharacterController = _mainCharacterTransform.GetComponent<CharacterController>();
            
            CheckForSerialization();
        }
        
        private void CheckForSerialization()
        {
            if(MainCharacterTransform == null) throw new SerializationException("MainCharacterTransform cannot be null");
            if(MovementInputProcessor == null) throw new SerializationException("MovementInputProcessor cannot be null");
            if(PossibleFollowTargets.Length == 0) throw new SerializationException("Amount of follow targets cannot be zero");
            if (FollowWalkSpeedCurve.length < 2) throw new SerializationException("FollowWalkSpeed curve is not initialized");
            if(FollowRunSpeedCurve.length < 2) throw new SerializationException("FollowRunSpeed curve is not initialized");
            if(WalkBackSpeedCurve.length < 2) throw new SerializationException("WalkBackSpeed curve is not initialized");
        }
        
        public float GetDistance()
        {
            var animalPosition = AnimalTransform.position;
            var characterPosition = MainCharacterTransform.position;

            return Vector3.Distance(animalPosition, characterPosition);
        }

        public void UpdateCurrentTarget()
        {
            var randomIndex = Random.Range(0, PossibleFollowTargets.Length);
            var randomTarget = PossibleFollowTargets[randomIndex];
            CurrentTarget = randomTarget;
        }
    }
}