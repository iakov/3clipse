using _3ClipseGame.Steam.Entities.Player.MainCharacter.Visuals.Scripts.AnimationsControllers;
using _3ClipseGame.Steam.Entities.Player.Scripts;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainCharacter
{
    [RequireComponent(typeof(MainCharacterStateMachine.MainCharacterStateMachine))]
    [RequireComponent(typeof(Gravity))]
    [RequireComponent(typeof(PlayerMover))]
    [RequireComponent(typeof(PlayerAnimationsController))]
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
            _mainCharacterAnimator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            _mainCharacterStateMachine.UpdateWork();
            _playerMover.UpdateWork();

            var legsRay = new Ray(_mainCharacterAnimator.GetBoneTransform(HumanBodyBones.Spine).position, Vector3.down);
            IsGrounded = Physics.Raycast(legsRay, _characterController.height/1.5f, _mainCharacterStateMachine.WalkableLayerMask);
        }

        #endregion
    }
}
