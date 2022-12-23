using System;
using System.IO;
using _3ClipseGame.Steam.GameMechanics.GameSaves.InGame.Data;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.GameSaves.InGame.SaveSerializers
{
    public class BinarySaveSerializer : SaveSerializer
    {
        private BinaryFormatterSearcher _binaryFormatterSearcher = new BinaryFormatterSearcher();
        
        public override void Serialize(GameSave save)
        {
            try
            {
                var saveName = GetSavePathWithId(save.ID);
                var fileStream = new FileStream(saveName, FileMode.Create, FileAccess.Write);
                var binaryFormatter = _binaryFormatterSearcher.GetBinaryFormatter();
                binaryFormatter.Serialize(fileStream, save);
                fileStream.Close();
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        public override GameSave Deserialize(string path)
        {
            try
            {
                var binaryFormatter = _binaryFormatterSearcher.GetBinaryFormatter();
                var fileStream = new FileStream(path, FileMode.Open);
                var save = binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
                return (GameSave)save;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                return null;
            }
        }

        public override void DestroySave(GameSave save)
        {
            try
            {
                var path = GetSavePathWithId(save.ID);
                File.Delete(path);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        public override void UpdateSave(GameSave save)
        {
            DestroySave(save);
            Serialize(save);
        }
    }
}