using System.Collections.Generic;
using System.Linq;
using _3ClipseGame.Steam.Mechanics.Save.InGame.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Mechanics.Save.UI.Scripts
{
    public class SaveManagerPresenter : MonoBehaviour
    {
        [SerializeField] private int _savesAmount = 4;
        [SerializeField] private SaveManager _saveManager;
        [SerializeField] private LayoutGroup _savesParent;
        [SerializeField] private RectTransform _emptySavePrefab;
        [SerializeField] private RectTransform _busySavePrefab;

        private List<SavePresenter> _savePresenters;
        private List<EmptySavePresenter> _emptySavePresenters;
        private List<BusySavePresenter> _busySavePresenters;

        private void Start()
        {
            CreateAllSaves();
        }

        private void OnEnable()
        {
            SubscribeToBusyPresenterEvents();
            SubscribeToEmptyPresenterEvents();
        }
        
        private void OnDisable()
        {
            UnsubscribeToBusyPresenterEvents();
            UnsubscribeToEmptyPresenterEvents();
        }
        
        private void CreateAllSaves()
        {
            var saves = _saveManager.GameSaves.ToList();
            CreatePresentersForSaves(saves);
            CreateEmptyPresenters();
        }
        
        #region EmptyPresenters

        private void CreateEmptyPresenters()
        {
            var currentAmountOfSaves = _savePresenters.Count;

            for (var i = currentAmountOfSaves - 1; i < _savesAmount; i++)
            {
                var component = CreateEmptyPresenter();
                _emptySavePresenters.Add(component);
                _savePresenters.Add(component);
                component.Clicked += CreateNewSave;
            }
        }
        
        private EmptySavePresenter CreateEmptyPresenter()
        {
            var newObject = Instantiate(_emptySavePrefab, _savesParent.transform);
            var presenterComponent = newObject.GetComponent<EmptySavePresenter>();
            return presenterComponent;
        }

        private void SubscribeToEmptyPresenterEvents()
        {
            foreach (var emptyPresenter in _emptySavePresenters)
            {
                emptyPresenter.Clicked += CreateNewSave;
            }
        }
        
        private void UnsubscribeToEmptyPresenterEvents()
        {
            foreach (var emptyPresenter in _emptySavePresenters)
            {
                emptyPresenter.Clicked -= CreateNewSave;
            }
        }

        #endregion

        #region BusyPresenters

        private void CreatePresentersForSaves(List<GameSave> saves)
        {
            saves.RemoveAll(save => saves.IndexOf(save) >= _savesAmount);
            
            foreach (var save in saves)
            {
                var component = CreateBusySavePresenter(save);
                _busySavePresenters.Add(component);
                _savePresenters.Add(component);
            }
        }

        private BusySavePresenter CreateBusySavePresenter(GameSave save)
        {
            var newObject = Instantiate(_busySavePrefab, _savesParent.transform);
            var presenterComponent = newObject.GetComponent<BusySavePresenter>();
            presenterComponent.TrackedSave = save;
            return presenterComponent;
        }
        
        private void SubscribeToBusyPresenterEvents()
        {
            foreach (var busyPresenter in _busySavePresenters)
            {
                busyPresenter.Clicked += LoadSave;
                busyPresenter.Cleared += ClearSave;
            }
        }
        
        private void UnsubscribeToBusyPresenterEvents()
        {
            foreach (var busyPresenter in _busySavePresenters)
            {
                busyPresenter.Clicked -= LoadSave;
                busyPresenter.Cleared -= ClearSave;
            }
        }

        #endregion

        private void CreateNewSave(SavePresenter presenter)
        {
            var clickedPresenter = _savePresenters.Find(displayedPresenter => displayedPresenter == presenter);
            Destroy(clickedPresenter);
            //var newSave = _saveManager.CreateNewSave();
            //_saveManager.LoadSave(newSave);
        }

        private void LoadSave(SavePresenter presenter)
        {
            var gameSave = presenter.TrackedSave;
            //_saveManager.LoadSave(gameSave);
        }

        private void ClearSave(BusySavePresenter presenter)
        {
            //_saveManager.DeleteSave(presenter.TrackedSave);
            Destroy(presenter.gameObject);
            CreateEmptyPresenter();
        }
    }
}