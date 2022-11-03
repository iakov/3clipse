using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Scripts.CharacterMover
{
    public class CamBeginRotateMove : Move
    {
        public CamBeginRotateMove(MoveType moveType, Vector3 inputVector, Transform mainCameraTransform) : base(moveType, inputVector, mainCameraTransform)
        {
            var cameraForward = MainCameraTransform.forward;
            cameraForward.y = 0;
            _rotatedVector =  RawVector.x * MainCameraTransform.right + RawVector.z * cameraForward.normalized;
        }
        
        private readonly Vector3 _rotatedVector;

        public override Vector3 GetRotatedVector() => _rotatedVector;
    }
}
