using _3ClipseGame.Steam.Entities.Player.MainCharacter.Visuals.Scripts.AnimationsControllers;
using _3ClipseGame.Steam.Entities.Player.Scripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter
{
    [RequireComponent(typeof(MainCharacterStateMachine.MainCharacterStateMachine))]
    [RequireComponent(typeof(Gravity))]
    [RequireComponent(typeof(PlayerMover))]
    [RequireComponent(typeof(PlayerAnimationsController))]
    public class MainCharacter : MonoBehaviour
    {
        #region PrivateFields
        
        private PlayerMover _playerMover;
        private MainCharacterStateMachine.MainCharacterStateMachine _mainCharacterStateMachine;

        #endregion

        #region MonoBehaviourMethods

        private void Awake()
        {
            _mainCharacterStateMachine = GetComponent<MainCharacterStateMachine.MainCharacterStateMachine>();
            _playerMover = GetComponent<PlayerMover>();
        }

        private void Update()
        {
            _mainCharacterStateMachine.UpdateWork();
            _playerMover.UpdateWork();
        }

        #endregion
    }
}
