using System;
using System.Collections.Generic;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Save.InGame.Data
{
    [Serializable]
    public class GameData
    {
        #region Initialization

        private List<ISaveData> _saveData;
        private PlayerSaveData _playerSaveData;

        public static GameData NewGame()
        {
            return new GameData();
        }
        
        private GameData()
        {
            _playerSaveData = PlayerSaveData.Empty();
            _saveData = new List<ISaveData>();
            InitializeAllSaveData();
        }

        private void InitializeAllSaveData()
        {
            _playerSaveData = PlayerSaveData.Empty();
            
            _saveData.Add(_playerSaveData);
        }

        #endregion

        public void ApplyData(SerializationDependencies applyDependencies)
        {
            foreach (var data in _saveData)
            {
                data.LoadData(applyDependencies);
            }
        }

        public void UpdateData(SerializationDependencies updateDependencies)
        {
            foreach (var saveData in _saveData)
            {
                saveData.SaveData(ref updateDependencies);
            }
        }
    }
}
