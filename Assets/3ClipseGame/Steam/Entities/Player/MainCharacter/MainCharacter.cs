using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;
using CharacterController = _3ClipseGame.Steam.Entities.CustomController.CharacterController;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter
{
    [RequireComponent(typeof(MainCharacterStateMachine.MainCharacterStateMachine))]
    [RequireComponent(typeof(PlayerMover))]
    public class MainCharacter : MonoBehaviour
    {
        #region PrivateFields
        
        private PlayerMover _playerMover;
        private MainCharacterStateMachine.MainCharacterStateMachine _mainCharacterStateMachine;
        private Animator _mainCharacterAnimator;
        private CharacterController _characterController;

        #endregion

        #region MonoBehaviourMethods

        private void Awake()
        {
            _mainCharacterStateMachine = GetComponent<MainCharacterStateMachine.MainCharacterStateMachine>();
            _playerMover = GetComponent<PlayerMover>();
            _mainCharacterAnimator = GetComponentInChildren<Animator>();
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            _mainCharacterStateMachine.UpdateWork();
        }

        #endregion
    }
}
