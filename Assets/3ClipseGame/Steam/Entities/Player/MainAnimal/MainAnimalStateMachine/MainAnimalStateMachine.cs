using System;
using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using _3ClipseGame.Steam.Global.Input.PlayerInput;
using UnityEngine;
using UnityEngine.AI;
using CharacterController = _3ClipseGame.Steam.Entities.CustomController.CharacterController;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(PlayerMover))]
    public class MainAnimalStateMachine : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private Transform mainCharacterTransform;
        [Header("Uncontrolled State Parameters")]
        [SerializeField] private float waitTime;
        
        [SerializeField] private float followWalkDistance;
        [SerializeField] private AnimationCurve followWalkSpeed;
        
        [SerializeField] private float stopWalkDistance;
        
        [SerializeField] private float walkBackDistance;
        [SerializeField] private AnimationCurve walkBackSpeed;

        [SerializeField] private Transform[] possibleFollowTargets;

        [SerializeField] private float followRunDistance;
        [SerializeField] private AnimationCurve followRunSpeed;

        [Header("Controlled State Parameters")] 
        [SerializeField] private float walkSpeed;

        #endregion

        #region PublicGetters

        public float WaitTime => waitTime;
        public float StopWalkDistance => stopWalkDistance;
        public float WalkBackDistance => walkBackDistance;
        public float FollowWalkDistance => followWalkDistance;
        public float FollowRunDistance => followRunDistance;
        public float WalkSpeed => walkSpeed;
        public bool IsSwitching { get; private set; }
        public Transform MainCharacterTransform => mainCharacterTransform;
        public Transform AnimalTransform { get; private set; }
        public Transform CurrentTarget { get; set; }
        public Transform[] PossibleFollowTargets => possibleFollowTargets;
        public AnimationCurve FollowWalkSpeed => followWalkSpeed;
        public AnimationCurve FollowRunSpeed => followRunSpeed;
        public AnimationCurve WalkBackSpeed => walkBackSpeed;
        public CharacterController MainCharacterController { get; private set; }
        public CharacterController AnimalController { get; private set; }
        public PlayerMover AnimalMover { get; private set; }
        public NavMeshAgent AnimalAgent { get; private set; }
        public MovementInputHandler InputHandler { get; private set; }

        #endregion
        
        #region PrivateFields

        private AnimalState _currentMainAnimalState;
        private AnimalStateFactory _mainAnimalStateFactory;

        #endregion

        #region MonoBehaviourMethods

        private void Awake()
        {
            AnimalMover = GetComponent<PlayerMover>();
            AnimalTransform = GetComponent<Transform>();
            AnimalAgent = GetComponent<NavMeshAgent>();
            AnimalController = GetComponent<CharacterController>();
            MainCharacterController = MainCharacterTransform.GetComponent<CharacterController>();
            InputHandler = GetComponentInParent<MovementInputHandler>();
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
        }

        #endregion
    }
}
