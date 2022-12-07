using _3ClipseGame.Steam.Core.GameSource.Parts;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame.Data
{
    public interface ISaveData
    {
        void LoadData(SerializationDependencies loadData);
        void SaveData(ref SerializationDependencies saveData);
    }
}
