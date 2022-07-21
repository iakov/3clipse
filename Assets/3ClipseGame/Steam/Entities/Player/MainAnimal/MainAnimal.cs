using UnityEngine;
using CharacterController = _3ClipseGame.Steam.Entities.CustomController.CharacterController;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal
{
    /*[RequireComponent(typeof(CharacterController))]*/
    public class MainAnimal : MonoBehaviour
    {
        #region SerializeFields
        

        #endregion

        #region PrivateFields

        private MainAnimalStateMachine.MainAnimalStateMachine _mainAnimalStateMachine;

        #endregion

        #region PublicGetters

        public CharacterController AnimalController { get; private set; }

        #endregion

        #region MonoBehaviourMethods

        private void Awake()
        {
            /*AnimalController = GetComponent<CharacterController>();*/
            _mainAnimalStateMachine = GetComponent<MainAnimalStateMachine.MainAnimalStateMachine>();
        }

        private void Update()
        {
            _mainAnimalStateMachine.UpdateWork();
        }

        #endregion
    }
}
