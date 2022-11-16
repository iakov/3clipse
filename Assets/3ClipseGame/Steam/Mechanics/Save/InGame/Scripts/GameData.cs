using System;
using _3ClipseGame.Steam.Core.GameSource.Parts.Player;
using _3ClipseGame.Steam.Mechanics.Save.InGame.Scripts.SaveParts;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame.Scripts
{
    [Serializable]
    public class GameData
    {
        public GameData(SerializationDependencies data)
        {
            if (data != null)
            {
                _playerSaveData = new PlayerSaveData(data.Player);
            }
        }

        private readonly SaveData<Player> _playerSaveData;
        
        public void Apply(SerializationDependencies data)
        {
            _playerSaveData.Load(data.Player);
        }

        public void UpdateData()
        {
            _playerSaveData?.Save();
        }
    }
}