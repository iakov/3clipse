using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts
{
    public class PlayerMover : MonoBehaviour
    {
        #region Initialization

        [SerializeField] private float rotationSpeed = 1f;
        private CharacterController _playerController;
        private Transform _cameraTransform;
        private Transform _playerTransform;
        private readonly List<Move> _movesList = new List<Move>();
        public static bool IsFreezed = false;

        #endregion

        #region MonoBehaviourMethods

        private void Start()
        {
            _playerController = GetComponent<CharacterController>();
            _playerTransform = GetComponent<Transform>();
            if (Camera.main != null) _cameraTransform = Camera.main.transform;
        }

        #endregion

        #region PublicMethods

        public void ChangeMove(MoveType type, Vector3 newMove, RotationType rotationType)
        {
            for (var index = 0; index < _movesList.Count; index++)
            {
                if (_movesList[index].MoveType != type) continue;

                _movesList[index] = new Move(type, newMove, rotationType, _cameraTransform);
                return;
            }

            _movesList.Add(new Move(type, newMove, rotationType, _cameraTransform));
        }

        public Vector3 GetLastMove(MoveType type)
        {
            if (_movesList.Count == 0) return Vector3.zero;
            foreach (var move in _movesList.Where(move => move.MoveType == type)) return move.MoveVector;
            return Vector3.zero;
        }

        public void UpdateWork()
        {
            if (IsFreezed) return;
            
            UpdateMove(out var resultMove);
            UpdateRotation(resultMove);
        }

        #endregion

        #region PrivateMethods

        private void UpdateMove(out Vector3 resultMove)
        {
            resultMove = Vector3.zero;
            foreach (var move in _movesList)
            {
                if (move.MoveRotationType == RotationType.RotateWithCamera)
                {
                    var thisMove = move.MoveVector.x * _cameraTransform.right + move.MoveVector.z * _cameraTransform.forward;
                    thisMove.y = move.MoveVector.y;
                    resultMove += thisMove;
                }
                else resultMove += move.MoveVector;
            }
            
            _playerController.Move(resultMove * Time.deltaTime);
        }

        private void UpdateRotation(Vector3 resultMove)
        {
            resultMove.y = 0f;
            if (resultMove == Vector3.zero) return;
            _playerTransform.rotation = Quaternion.Slerp(_playerTransform.rotation, Quaternion.LookRotation(resultMove), rotationSpeed * Time.deltaTime);
        }

        #endregion

        #region PrivateClasses

        private class Move
        { 
            public Move(MoveType type, Vector3 moveVector, RotationType rotationType, Transform cameraTransform)
            {
                MoveType = type;
                MoveRotationType = rotationType;
                InitiateMoveVector(moveVector, cameraTransform);
            }

            private void InitiateMoveVector(Vector3 moveVector, Transform cameraTransform)
            {
                switch (MoveRotationType)
                {
                    case RotationType.NoRotation: case RotationType.RotateWithCamera:
                        MoveVector = moveVector;
                        break;
                    case RotationType.RotateOnBeginning:
                        MoveVector = moveVector.x * cameraTransform.right + moveVector.z * cameraTransform.forward;
                        MoveVector.y = moveVector.y;
                        break;
                    default:
                        throw new ArgumentException("Rotation type is not implemented yet");
                }
            }
            
            public readonly MoveType MoveType;
            public Vector3 MoveVector;
            public readonly RotationType MoveRotationType;
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
