using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts
{
    public class CamBeginRotateMove : Move
    {
        public CamBeginRotateMove(MoveType moveType, Vector3 inputVector, Transform mainCameraTransform) : base(moveType, inputVector, mainCameraTransform)
        {
            _rotatedVector = inputVector.x * mainCameraTransform.right + inputVector.z * mainCameraTransform.forward;
        }
        
        private Vector3 _rotatedVector;

        public override Vector3 GetRotatedVector() => _rotatedVector;
    }
}
