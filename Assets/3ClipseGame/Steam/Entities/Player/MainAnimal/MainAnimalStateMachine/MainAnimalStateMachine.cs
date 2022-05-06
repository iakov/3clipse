using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.States;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine
{
    public class MainAnimalStateMachine : MonoBehaviour
    {
        #region SerializeFields
        
        [SerializeField] private Transform mainCharacterTransform;
        [SerializeField] private float walkSpeed;
        [SerializeField] private float horizontalMoveOffset;

        #endregion

        #region PublicGetters

        public Transform MainCharacterTransform => mainCharacterTransform;
        public float WalkSpeed => walkSpeed;
        public float HorizontalMoveOffset => horizontalMoveOffset;

        #endregion
        
        #region PrivateFields

        private AnimalState _currentMainAnimalState;
        private AnimalStateFactory _mainAnimalStateFactory;

        #endregion

        #region MonoBehaviourMethods

        private void Start()
        {
            _mainAnimalStateFactory = new AnimalStateFactory(this);
            _currentMainAnimalState = _mainAnimalStateFactory.UncontrolledState();
            _currentMainAnimalState.OnStateEnter();
        }

        #endregion

        #region PublicMethods

        public void ManualUpdate()
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
