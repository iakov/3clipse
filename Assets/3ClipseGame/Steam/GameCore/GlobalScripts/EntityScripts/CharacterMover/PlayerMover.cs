using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _3ClipseGame.Steam.GameCore.GlobalScripts.EntityScripts.CharacterMover
{
    public class PlayerMover : MonoBehaviour
    {
        private CharacterController _characterController;
        private readonly List<Move> _movesList = new();
        public static bool IsFreezed = false;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void FixedUpdate()
        {
            if (IsFreezed) return;
            
            UpdateMove();
        }

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

        public Vector3 RotateWithCamera(Vector3 inputVector, MoveType moveType, RotationType rotationType)
        {
            var move = CreateMove(moveType, inputVector, rotationType);
            return move.GetRotatedVector();
        }

        #endregion

        #region PrivateMethods

        private Move CreateMove(MoveType type, Vector3 newMove, RotationType rotationType)
        {
            var cameraTransform = Camera.main.transform;
            
            switch (rotationType)
            {
                case RotationType.NoRotation:
                    return new NoCamRotationMove(type, newMove, cameraTransform);
                case RotationType.RotateOnBeginning:
                    return new CamBeginRotateMove(type, newMove, cameraTransform);
                case RotationType.RotateWithCamera:
                    return new RotateWithCameraMove(type, newMove, cameraTransform);
                default:
                    return null;
            }
        }

        private void UpdateMove()
        {
            var resultMove = _movesList.Aggregate(Vector3.zero, (current, move) => current + move.GetRotatedVector());
            _characterController.Move(resultMove * Time.fixedDeltaTime);
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
