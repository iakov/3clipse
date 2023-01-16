using UnityEngine;

namespace _3ClipseGame.Steam.GameCore.GlobalScripts.EntityScripts.CharacterMover

{
    public class RotateWithCameraMove : Move
    {
        public RotateWithCameraMove(MoveType moveType, Vector3 inputVector, Transform mainCameraTransform) 
            : base(moveType, inputVector, mainCameraTransform){}

        public override Vector3 GetRotatedVector()
        {
            var cameraForward = MainCameraTransform.forward;
            cameraForward.y = 0;
            return RawVector.x * MainCameraTransform.right + RawVector.z * cameraForward.normalized;
        }
    }
}