using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame.Scripts
{
    public class SaveManager : MonoBehaviour
    {
        [SerializeField] private SerializationDependencies _serializationDependencies;

        public IEnumerable<GameSave> GameSaves => _allGameSaves;

        private static List<GameSave> _allGameSaves;
        private static GameSave _currentSave;
        
        private void Awake()
        {
            _serializationDependencies = GetComponent<SerializationDependencies>();
            _allGameSaves = FindAllSaves();
        }

        private List<GameSave> FindAllSaves()
        {
            var directories = Directory.GetFiles(SaveSerializer.SavePath);
            return directories.Select(SaveSerializer.Deserialize).ToList();
        }
    }
}
