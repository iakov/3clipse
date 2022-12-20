using _3ClipseGame.Steam.Core.GameSource.Parts;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame.Data
{
    public class MainCharacterSaveData : ISaveData
    {
        private Vector3 _position;
        private Quaternion _rotation;

        public static MainCharacterSaveData Empty()
        {
            return new MainCharacterSaveData();
        }
        
        public void LoadData(SerializationDependencies loadData)
        {
            
        }

        public void SaveData(SerializationDependencies saveData)
        {
            throw new System.NotImplementedException();
        }
    }
}