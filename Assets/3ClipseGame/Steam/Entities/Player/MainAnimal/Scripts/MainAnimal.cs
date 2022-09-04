using UnityEngine;
using CharacterController = _3ClipseGame.Steam.Entities.CustomController.CharacterController;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.Scripts
{
    [RequireComponent(typeof(CharacterController))]
    public class MainAnimal : MonoBehaviour
    {
        #region PrivateFields

        private StateMachine.MainAnimalStateMachine _mainAnimalStateMachine;

        #endregion

        #region MonoBehaviourMethods

        private void Awake()
        {
            _mainAnimalStateMachine = GetComponent<StateMachine.MainAnimalStateMachine>();
        }

        private void Update()
        {
            _mainAnimalStateMachine.UpdateWork();
        }

        #endregion
    }
}
