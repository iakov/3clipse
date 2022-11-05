using System;
using _3ClipseGame.Steam.Core.Save.SaveParts;

namespace _3ClipseGame.Steam.Core.Save
{
    [Serializable]
    public class GameData
    {
        public GameData(SerializationDependencies data)
        {
            _playerSaveData = new PlayerSaveData(data.Player);
        }
        
        public void Apply(SerializationDependencies data)
        {
            _playerSaveData.Load(data.Player);
        }

        public void UpdateData()
        {
            _playerSaveData.Save();
        }

        private readonly PlayerSaveData _playerSaveData;
    }
}