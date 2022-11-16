using System;
using System.IO;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame.Scripts
{
    public static class SaveSerializer
    {
        public static string SavePath = string.Concat(Application.persistentDataPath, "/saves/");
        
        public static void Serialize(GameSave save)
        {
            try
            {
                var saveName = SavePath + save.Id;
                var fileStream = new FileStream(saveName, FileMode.Create, FileAccess.Write);
                var binaryFormatter = BinaryFormatterSearcher.GetBinaryFormatter();
                binaryFormatter.Serialize(fileStream, save);
                fileStream.Close();
            }
            catch (Exception e)
            {
                //ignore
            }
        }

        public static GameSave Deserialize(string path)
        {
            try
            {
                var binaryFormatter = BinaryFormatterSearcher.GetBinaryFormatter();
                var fileStream = new FileStream(path, FileMode.Open);
                var save = binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
                return (GameSave)save;
            }
            catch (Exception e)
            {
                return new GameSave(null);
            }
        }
    }
}