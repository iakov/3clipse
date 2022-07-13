using _3ClipseGame.Steam.Entities.Player.Scripts;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter
{
    [RequireComponent(typeof(MainCharacterStateMachine.MainCharacterStateMachine))]
    [RequireComponent(typeof(Gravity))]
    [RequireComponent(typeof(PlayerMover))]
    public class MainCharacter : MonoBehaviour
    {
        #region PublicFields

        public bool IsGrounded { get; private set; }

        #endregion
        
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
            _playerMover.UpdateWork();

            var legsRay = new Ray(transform.position + _characterController.center , Vector3.down);
            IsGrounded = Physics.SphereCast(legsRay, _characterController.radius - 0.05f, _characterController.height/2 - _characterController.radius + 0.1f, _mainCharacterStateMachine.WalkableLayerMask);
        }

        #endregion
    }
}
