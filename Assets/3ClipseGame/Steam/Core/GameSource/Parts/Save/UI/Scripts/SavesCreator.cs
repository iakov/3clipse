using System.Collections.Generic;
using _3ClipseGame.Steam.Core.GameSource.Parts.Save.InGame.Data;
using UnityEngine;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Save.UI.Scripts
{
    public class SavesCreator : MonoBehaviour
    {
        [Header("Saves")]
        [SerializeField] private GameObject _emptySavePresenter;
        [SerializeField] private GameObject _busySavePresenter;
        [SerializeField] private List<Transform> _savesSpawnPositions;

        public List<BusySavePresenter> CreateBusyPresenters(List<GameSave> saves)
        {
            List<BusySavePresenter> savePresenters = new();
            
            for (var i = 0; i < saves.Count; i++)
            {
                var savePresenter = CreateBusyPresenter(saves[i], i);
                savePresenters.Add(savePresenter);
            }

            return savePresenters;
        }
        
        public BusySavePresenter CreateBusyPresenter(GameSave save, int id)
        {
            var newObject = Instantiate(_busySavePresenter, _savesSpawnPositions[id]);
            var savePresenter = newObject.GetComponent<BusySavePresenter>();

            savePresenter.ChangeTrackedSave(save);
            
            return savePresenter;
        }

        public List<EmptySavePresenter> CreateEmptyPresenters(int busyPresentersAmount, int presentersAmount)
        {
            List<EmptySavePresenter> savePresenters = new();
            if(presentersAmount <= 0) return savePresenters;

            for (var i = busyPresentersAmount; i < presentersAmount; i++)
            {
                var savePresenter = CreateEmptyPresenter(i);
                savePresenters.Add(savePresenter);
            }

            return savePresenters;
        }
        
        public EmptySavePresenter CreateEmptyPresenter(int id)
        {
            var newObject = Instantiate(_emptySavePresenter, _savesSpawnPositions[id]);
            var savePresenter = newObject.GetComponent<EmptySavePresenter>();

            return savePresenter;
        }
    }
}
