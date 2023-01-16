using _3ClipseGame.Steam.GameCore.Origin;

namespace _3ClipseGame.Steam.GameMechanics.GameSaves.InGame.Data
{
    public interface ISaveData
    {
        void LoadData(SerializationDependencies loadData);
        void SaveData(SerializationDependencies saveData);
    }
}
