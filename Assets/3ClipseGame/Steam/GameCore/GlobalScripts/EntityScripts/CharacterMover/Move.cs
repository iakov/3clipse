using UnityEngine;

namespace _3ClipseGame.Steam.GameCore.GlobalScripts.EntityScripts.CharacterMover
{
    public abstract class Move
    {
        public readonly MoveType MoveType;
        
        protected Move(MoveType moveType, Vector3 inputVector, Transform mainCameraTransform)
        {
            MoveType = moveType;
            RawVector = inputVector;
            MainCameraTransform = mainCameraTransform;
        }
        
        protected Vector3 RawVector;
        protected readonly Transform MainCameraTransform;
        
        public Vector3 GetRawVector() => RawVector;
        public abstract Vector3 GetRotatedVector();
    }
}
