using _3ClipseGame.Steam.Mechanics.Save.InGame.Data;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame.SaveSerializers
{
    public abstract class SaveSerializer
    {
        public readonly string SavePath = string.Concat(Application.persistentDataPath, "/saves/");
        
        public abstract void Serialize(GameSave save);
        public abstract GameSave Deserialize(string path);
        public abstract void DestroySave(GameSave save);
        public abstract void UpdateSave(GameSave save);

        protected string GetSavePathWithId(int id)
        {
            return string.Concat(SavePath, id, ".save");
        }
    }
}