using System.Collections.Generic;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts.GlobalScripts
{
    public class PlayerRotator : MonoBehaviour
    {
        [Range(0,1)] [SerializeField] private float rotationSpeed = 1f;
        private List<Rotation> _rotationsList = new List<Rotation>();
        private Transform _characterTransform;

        public void Start()
        {
            _characterTransform = transform;
        }
        
        public void UpdateWork()
        {
            if (_rotationsList.Count == 0) return;

            Rotation targetRotation = _rotationsList[0];
            foreach (var rotation in _rotationsList) if (rotation.Priority > targetRotation.Priority) targetRotation = rotation;
            var currentRotation = _characterTransform.rotation;
            _characterTransform.rotation = Quaternion.Slerp(currentRotation, targetRotation.RotateInfo, rotationSpeed);
        }

        public void ChangeRotation(Quaternion rotateInfo, uint priority)
        {
            foreach (var rotation in _rotationsList)
            {
                if(rotation.Priority == priority) rotation.RotateInfo = rotateInfo;
                return;
            }
            _rotationsList.Add(new Rotation(rotateInfo, priority));
        }
        
        #region PrivateClasses

        private class Rotation
        {
            public Rotation(Quaternion rotation, uint priority)
            {
                Priority = priority;
                RotateInfo = rotation;
            }
            
            public uint Priority;
            public Quaternion RotateInfo;
        }

        #endregion
    }
}
