using System;
using _3ClipseGame.Steam.Core.GameSource.Parts;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame.Data
{
    [Serializable]
    public class PlayerSaveData : ISaveData
    {
        private Vector3 _position;
        private Quaternion _rotation;

        public static PlayerSaveData Empty()
        {
            return new PlayerSaveData();
        }

        public void LoadData(SerializationDependencies loadDependencies)
        {
            var playerTransform = loadDependencies.Player.transform;
            playerTransform.position = _position;
            playerTransform.rotation = _rotation;
        }

        public void SaveData(SerializationDependencies saveDependencies)
        {
            var playerTransform = saveDependencies.Player.transform;
            _position = playerTransform.position;
            _rotation = playerTransform.rotation;
            Debug.Log(_position);
        }
    }
}