using System;
using System.Collections.Generic;
using _3ClipseGame.Steam.Mechanics.Save.InGame.Data;
using _3ClipseGame.Steam.Mechanics.Save.UI.Scripts.Interfaces;
using _3ClipseGame.Steam.Mechanics.Save.UI.Scripts.SavePresenters;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.Save.UI.Scripts.SaveCreators
{
    public class SavesCreator : MonoBehaviour, ISavesCreator
    {
        [Header("Saves")]
        [SerializeField] private Camera _eventsCamera;
        [SerializeField] private GameObject _emptySavePresenter;
        [SerializeField] private GameObject _busySavePresenter;
        [SerializeField] private List<Transform> _savesSpawnPositions;

        public List<BusySavePresenter> CreateBusyPresenters(List<GameSave> saves)
        {
            List<BusySavePresenter> savePresenters = new();
            
            for (var i = 0; i < saves.Count; i++)
            {
                try
                {
                    var savePresenter = CreateBusyPresenter(saves[i], i);
                    savePresenters.Add(savePresenter);
                }
                catch (Exception )
                {
                    var currentTransform = _savesSpawnPositions[i];
                    DeleteAllChildren(currentTransform);
                }
            }

            return savePresenters;
        }

        private void DeleteAllChildren(Transform parentTransform)
        {
            var childCount = parentTransform.childCount;
            for (var i = 0; i < childCount; i++) 
                Destroy(parentTransform.GetChild(i).gameObject);
        }

        public BusySavePresenter CreateBusyPresenter(GameSave save, int id)
        {
            var newObject = Instantiate(_busySavePresenter, _savesSpawnPositions[id]);
            var savePresenter = newObject.GetComponent<BusySavePresenter>();

            SetEventsCameraToCanvas(savePresenter);
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
            SetEventsCameraToCanvas(savePresenter);

            return savePresenter;
        }
        
        private void SetEventsCameraToCanvas(SavePresenter presenter)
        {
            var canvas = presenter.GetComponentInChildren<Canvas>();
            canvas.worldCamera = _eventsCamera;
        }
    }
}
