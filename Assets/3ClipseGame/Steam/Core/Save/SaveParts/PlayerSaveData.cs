using System;
using _3ClipseGame.Steam.Core.GameSource.Parts.Player;
using UnityEngine;

namespace _3ClipseGame.Steam.Core.Save.SaveParts
{
    [Serializable]
    public class PlayerSaveData : SaveData<Player>
    {
        public PlayerSaveData(Player player) : base(player){}

        [SerializeField] private Vector3 _position;
        [SerializeField] private Quaternion _rotation;
        
        public override void Save()
        {
            var saveObjectTransform = SerializeObject.transform;
            _position = saveObjectTransform.position;
            _rotation = saveObjectTransform.rotation;
        }

        public override void Load(Player newObject)
        {
            var saveObjectTransform = newObject.transform;
            saveObjectTransform.position = _position;
            saveObjectTransform.rotation = _rotation;
        }
    }
}
