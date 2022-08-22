using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CharacterController = _3ClipseGame.Steam.Entities.CustomController.CharacterController;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts
{
    public class PlayerMover : MonoBehaviour
    {
        #region Initialization

        [SerializeField] private float rotationSpeed = 1f;
        private CharacterController _characterController;
        private Transform _cameraTransform;
        private Transform _playerTransform;
        private CapsuleCollider _capsuleCollider;
        private readonly List<Move> _movesList = new();
        public static bool IsFreezed = false;
        private Rigidbody _rigidbody;

        #endregion

        #region MonoBehaviourMethods

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _playerTransform = GetComponent<Transform>();
            _rigidbody = GetComponent<Rigidbody>();
            _capsuleCollider = GetComponentInChildren<CapsuleCollider>();
            
            if (Camera.main != null) _cameraTransform = Camera.main.transform;
        }

        private void FixedUpdate()
        {
            if (IsFreezed) return;
            
            UpdateMove(out var resultMove);
            UpdateRotation(resultMove);
        }

        #endregion

        #region PublicMethods

        public void ChangeMove(MoveType type, Vector3 newMove, RotationType rotationType)
        {
            for (var index = 0; index < _movesList.Count; index++)
            {
                if (_movesList[index].MoveType != type) continue;

                _movesList[index] = CreateMove(type, newMove, rotationType);
                return;
            }

            _movesList.Add(CreateMove(type, newMove, rotationType));
        }

        public Vector3 GetLastMove(MoveType type, bool isRotated)
        {
            if (_movesList.Count == 0) return Vector3.zero;
            foreach (var move in _movesList.Where(move => move.MoveType == type))
                return isRotated ? move.GetRotatedVector() : move.GetRawVector();
            
            return Vector3.zero;
        }

        #endregion

        #region PrivateMethods

        private Move CreateMove(MoveType type, Vector3 newMove, RotationType rotationType)
        {
            switch (rotationType)
            {
                case RotationType.NoRotation:
                    return new NoCamRotationMove(type, newMove, _cameraTransform);
                case RotationType.RotateOnBeginning:
                    return new CamBeginRotateMove(type, newMove, _cameraTransform);
                case RotationType.RotateWithCamera:
                    return new RotateWithCameraMove(type, newMove, _cameraTransform);
                default:
                    return null;
            }
        }

        private void UpdateMove(out Vector3 resultMove)
        {
            resultMove = _movesList.Aggregate(Vector3.zero, (current, move) => current + move.GetRotatedVector());
            _characterController.Move(resultMove * Time.fixedDeltaTime);
        }

        private void UpdateRotation(Vector3 resultMove)
        {
            resultMove.y = 0f;
            if (resultMove == Vector3.zero) return;
            
            var rotation = Quaternion.Slerp(_capsuleCollider.transform.rotation, Quaternion.LookRotation(resultMove), rotationSpeed * Time.fixedDeltaTime);
            _characterController.Rotate(rotation);
        }

        #endregion
    }
    
    public enum RotationType
    {
        NoRotation, RotateOnBeginning, RotateWithCamera
    }
    
    public enum MoveType
    {
        GravityMove, StateMove
    }
}
