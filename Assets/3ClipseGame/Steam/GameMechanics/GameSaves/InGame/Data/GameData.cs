using System;
using System.Collections.Generic;
using _3ClipseGame.Steam.GameCore.Origin;

namespace _3ClipseGame.Steam.GameMechanics.GameSaves.InGame.Data
{
    [Serializable]
    public class GameData
    {
        #region Initialization

        private List<ISaveData> _saveData;
        private PlayerSaveData _playerSaveData;
        private MainCharacterSaveData _mainCharacterSaveData;

        public static GameData NewGame()
        {
            return new GameData();
        }
        
        private GameData()
        {
            _saveData = new List<ISaveData>();
            InitializeAllSaveData();
        }

        private void InitializeAllSaveData()
        {
            _playerSaveData = PlayerSaveData.Empty();
            _mainCharacterSaveData = MainCharacterSaveData.Empty();
            
            UpdateLinks();
        }

        #endregion

        public void ApplyData(SerializationDependencies applyDependencies)
        {
            if(_saveData == null) UpdateLinks();
            
            foreach (var data in _saveData)
            {
                data.LoadData(applyDependencies);
            }
        }

        public void UpdateData(SerializationDependencies updateDependencies)
        {
            if(_saveData == null) UpdateLinks();
            
            foreach (var saveData in _saveData)
            {
                saveData.SaveData(updateDependencies);
            }
        }

        private void UpdateLinks()
        {
            _saveData = new List<ISaveData>
            {
                _playerSaveData, _mainCharacterSaveData
            };
        }
    }
}
