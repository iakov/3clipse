using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts
{
    public abstract class Move
    {
        public MoveType MoveType { get; private set; }
        
        protected Move(MoveType moveType, Vector3 inputVector, Transform mainCameraTransform)
        {
            MoveType = moveType;
            RawVector = inputVector;
            MainCameraTransform = mainCameraTransform;
        }
        
        protected Vector3 RawVector;
        protected Transform MainCameraTransform;
        
        public Vector3 GetRawVector() => RawVector;
        public abstract Vector3 GetRotatedVector();
    }
}
