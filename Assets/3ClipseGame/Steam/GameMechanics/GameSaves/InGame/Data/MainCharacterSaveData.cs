using System;
using _3ClipseGame.Steam.GameCore.Origin;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.GameSaves.InGame.Data
{
    [Serializable]
    public class MainCharacterSaveData : ISaveData
    {
        private Vector3 _mainCharacterPosition;
        private Quaternion _mainCharacterRotation;

        public static MainCharacterSaveData Empty()
        {
            return new MainCharacterSaveData();
        }
        
        public void LoadData(SerializationDependencies loadData)
        {
            var mainCharacterTransform = loadData.MainCharacter;
            mainCharacterTransform.Teleport(_mainCharacterPosition);
            mainCharacterTransform.Rotate(_mainCharacterRotation);
        }

        public void SaveData(SerializationDependencies saveData)
        {
            var mainCharacterTransform = saveData.MainCharacter;
            _mainCharacterPosition = mainCharacterTransform.Center;
            _mainCharacterRotation = mainCharacterTransform.Rotation;
        }
    }
}