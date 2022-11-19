using System;
using System.IO;
using _3ClipseGame.Steam.Core.GameSource.Parts.Save.InGame.Data;
using UnityEngine;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Save.InGame
{
    public static class SaveSerializer
    {
        public static string SavePath = string.Concat(Application.persistentDataPath, "/saves/");
        
        public static void Serialize(GameSave save)
        {
            try
            {
                var saveName = SavePath + save.ID;
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
                return GameSave.NewGame(-1, new Texture2D(1, 1));
            }
        }

        public static void DeleteFile(string saveID)
        {
            try
            {
                File.Delete(SavePath + saveID);
            }
            catch
            {
                //ignored
            }
        }

        public static string GetSavePathWithId(int id)
        {
            return string.Concat(SavePath, id, ".save");
        }
    }
}