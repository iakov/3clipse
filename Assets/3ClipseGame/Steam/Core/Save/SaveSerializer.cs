using System.IO;

namespace _3ClipseGame.Steam.Core.Save
{
    public static class SaveSerializer
    {
        public static void Serialize(string path, GameSave save)
        {
            var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            var binaryFormatter = BinaryFormatterSearcher.GetBinaryFormatter();
            binaryFormatter.Serialize(fileStream, save);
            fileStream.Close();
        }

        public static GameSave Deserialize(string path)
        {
            var binaryFormatter = BinaryFormatterSearcher.GetBinaryFormatter();
            var fileStream = new FileStream(path, FileMode.Open);
            var save = binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return (GameSave) save;
        }
    }
}