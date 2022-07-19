using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine
{
    [RequireComponent(typeof(PlayerMover))]
    public class MainAnimalStateMachine : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private Transform mainCharacterTransform;
        [SerializeField] private float walkSpeed;
        [Header("Uncontrolled State Parameters")]
        [SerializeField] private float followWalkDistance;
        [SerializeField] private AnimationCurve followWalkSpeed;
        
        [SerializeField] private float followRunDistance;
        [SerializeField] private AnimationCurve followRunSpeed;

        #endregion

        #region PublicGetters

        public Transform MainCharacterTransform => mainCharacterTransform;
        public Transform AnimalTransform { get; private set; }
        public float WalkSpeed => walkSpeed;
        public PlayerMover AnimalMover { get; private set; }
        public float FollowWalkDistance => followWalkDistance;
        public AnimationCurve FollowWalkSpeed => followWalkSpeed;
        public float FollowFollowRunDistance => followRunDistance;
        public AnimationCurve FollowRunSpeed => followRunSpeed;

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
