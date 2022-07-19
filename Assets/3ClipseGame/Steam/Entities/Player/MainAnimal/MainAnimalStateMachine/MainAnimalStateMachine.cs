using System.Collections.Generic;
using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;
using UnityEngine.AI;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine
{
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

        [SerializeField] private Transform[] possibleFollowTargets;

        [SerializeField] private float followRunDistance;
        [SerializeField] private AnimationCurve followRunSpeed;

        #endregion

        #region PublicGetters

        public Transform MainCharacterTransform => mainCharacterTransform;
        public Transform AnimalTransform { get; private set; }
        public Transform CurrentTarget { get; set; }
        public PlayerMover AnimalMover { get; private set; }
        public float FollowWalkDistance => followWalkDistance;
        public AnimationCurve FollowWalkSpeed => followWalkSpeed;
        public float FollowRunDistance => followRunDistance;
        public AnimationCurve FollowRunSpeed => followRunSpeed;
        public float StopWalkDistance => stopWalkDistance;
        public float WaitTime => waitTime;
        public Transform[] PossibleFollowTargets => possibleFollowTargets;
        public NavMeshAgent AnimalAgent { get; private set; }

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
        }

        private void Start()
        {
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

        #endregion

        #region PrivateMethods

        private void SwitchState(AnimalState newState)
        {
            _currentMainAnimalState.OnStateExit();
            _currentMainAnimalState = newState;
            _currentMainAnimalState.OnStateEnter();
        }

        #endregion
    }
}
