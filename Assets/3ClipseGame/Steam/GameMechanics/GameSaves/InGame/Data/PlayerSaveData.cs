using System;
using _3ClipseGame.Steam.GameCore.Origin;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.GameSaves.InGame.Data
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