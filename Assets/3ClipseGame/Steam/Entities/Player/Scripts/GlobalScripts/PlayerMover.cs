using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.GlobalScripts
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

        public void ChangeMove(MoveType type, Vector3 newMove, bool isRotatedWithCamera)
        {
            foreach (var move in _movesList)
            {
                if (move.MoveType != type) continue;
                move.MoveVector = newMove;
                move.IsRotatedWithCamera = isRotatedWithCamera;
                return;
            }
            
            _movesList.Add(new Move(type, newMove, isRotatedWithCamera));
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
            
            var resultMove = Vector3.zero;
            foreach (var move in _movesList)
            {
                if (move.IsRotatedWithCamera)
                {
                    var direction = move.MoveVector.x * _cameraTransform.right  + move.MoveVector.z * _cameraTransform.forward;
                    direction.y = move.MoveVector.y;
                    resultMove += direction;
                }
                else resultMove += move.MoveVector;
            }
            _playerController.Move(resultMove * Time.deltaTime);
            
            resultMove.y = 0f;
            if (resultMove == Vector3.zero) return;
            _playerTransform.rotation = Quaternion.Slerp(_playerTransform.rotation, Quaternion.LookRotation(resultMove), rotationSpeed);
        }

        #endregion

        #region PrivateClasses

        private class Move
        {
            public Move(MoveType type, Vector3 moveVector, bool isRotatedWithCamera)
            {
                MoveType = type;
                MoveVector = moveVector;
                IsRotatedWithCamera = isRotatedWithCamera;
            }
            
            public readonly MoveType MoveType;
            public Vector3 MoveVector;
            public bool IsRotatedWithCamera;
        }

        #endregion
    }
    
    public enum MoveType
    {
        GravityMove, StateMove
    }
}
