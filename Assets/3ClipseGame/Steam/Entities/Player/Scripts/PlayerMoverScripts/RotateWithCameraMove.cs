using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts
{
    public class RotateWithCameraMove : Move
    {
        public RotateWithCameraMove(MoveType moveType, Vector3 inputVector, Transform mainCameraTransform) : base(moveType, inputVector, mainCameraTransform)
        {}

        public override Vector3 GetRotatedVector() =>
            RawVector.x * MainCameraTransform.right + RawVector.z * MainCameraTransform.forward;
    }
}