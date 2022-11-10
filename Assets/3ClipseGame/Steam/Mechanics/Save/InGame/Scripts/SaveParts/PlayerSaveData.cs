using System;
using _3ClipseGame.Steam.Core.GameSource.Parts.Player;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame.Scripts.SaveParts
{
    [Serializable]
    public class PlayerSaveData : SaveData<Player>
    {
        public PlayerSaveData(Player player) : base(player){}
        
        private Vector3 _position;
        private Quaternion _rotation;

        public override void Save()
        {
            if (SerializeObject != null)
            {
                var saveObjectTransform = SerializeObject.transform;
                _position = saveObjectTransform.position;
                _rotation = saveObjectTransform.rotation;
            }
        }

        public override void Load(Player newObject)
        {
            if (newObject != null)
            {
                var saveObjectTransform = newObject.transform;
                saveObjectTransform.position = _position;
                saveObjectTransform.rotation = _rotation;
            }
        }
    }
}
