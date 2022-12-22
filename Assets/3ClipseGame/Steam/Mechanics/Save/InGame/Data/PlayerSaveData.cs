using System;
using _3ClipseGame.Steam.Core.GameSource;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame.Data
{
    [Serializable]
    public class PlayerSaveData : ISaveData
    {
        private Vector3 _playerPosition;
        private Quaternion _playerRotation;

        public static PlayerSaveData Empty()
        {
            return new PlayerSaveData();
        }

        public void LoadData(SerializationDependencies loadDependencies)
        {
            var playerTransform = loadDependencies.Player.transform;
            playerTransform.position = _playerPosition;
            playerTransform.rotation = _playerRotation;
        }

        public void SaveData(SerializationDependencies saveDependencies)
        {
            var playerTransform = saveDependencies.Player.transform;
            _playerPosition = playerTransform.position;
            _playerRotation = playerTransform.rotation;
        }
    }
}