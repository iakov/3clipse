using System;
using System.IO;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame.Scripts
{
    public static class SaveSerializer
    {
        public static void Serialize(string path, GameSave save)
        {
            try
            {
                var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
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
                return new GameSave(e.Message, null);
            }
        }
    }
}