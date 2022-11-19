namespace _3ClipseGame.Steam.Core.GameSource.Parts.Save.InGame.Data
{
    public interface ISaveData
    {
        void LoadData(SerializationDependencies loadData);
        void SaveData(ref SerializationDependencies saveData);
    }
}
