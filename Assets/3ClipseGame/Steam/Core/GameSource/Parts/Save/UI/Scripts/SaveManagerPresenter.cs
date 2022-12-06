using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _3ClipseGame.Steam.Core.GameSource.Parts.Save.InGame;
using UnityEngine;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Save.UI.Scripts
{
    [RequireComponent(typeof(SavesCreator))]
    public class SaveManagerPresenter : MonoBehaviour
    {
        [SerializeField] private int _savesAmount = 4;
        [SerializeField] private UnityEngine.Camera _eventsCamera;
        [SerializeField] private SelectedSavePresenter _savesImageComponent;
        [SerializeField] private Sprite _newSaveSprite;

        private SaveManager _saveManager => SaveManager.Instance;
        private SavesCreatorEventsWrapper _savesCreator;

        private List<SavePresenter> _savePresenters;
        private SavePresenter _selectedPresenter;

        private void Awake()
        {
            var savesCreator = GetComponent<SavesCreator>();
            _savesCreator = new SavesCreatorEventsWrapper(savesCreator);
        }

        private void OnEnable()
        {
            StartCoroutine(DisplaySaveSlotsWithDelay());
            _savesCreator.PresenterCleared += ClearSavePresenter;
            _savesCreator.BusyPresenterClicked += SelectBusyPresenter;
            _savesCreator.EmptyPresenterClicked += SelectEmptyPresenter;
        }

        private void OnDisable()
        {
            _savesCreator.PresenterCleared -= ClearSavePresenter;
            _savesCreator.BusyPresenterClicked -= SelectBusyPresenter;
            _savesCreator.EmptyPresenterClicked -= SelectEmptyPresenter;
        }
        
        private IEnumerator DisplaySaveSlotsWithDelay()
        {
            while (SaveManager.Instance == null || SaveManager.Instance.IsSavesFound == false)
                yield return null;

            DestroyAllPresenters();
            CreateAllPresenters();
        }

        private void DestroyAllPresenters()
        {
            if (_savePresenters == null) return;

            foreach (var presenter in _savePresenters)
                Destroy(presenter.gameObject);
        }

        private void CreateAllPresenters()
        {
            _savePresenters = new List<SavePresenter>();

            CreateBusyPresenters();
            CreateEmptyPresenters(_savePresenters.Count);

            foreach (var savePresenter in _savePresenters)
            {
                var canvas = savePresenter.GetComponentInChildren<Canvas>();
                canvas.worldCamera = _eventsCamera;
            }
        }

        private void CreateBusyPresenters()
        {
            var gameSaves = _saveManager.GameSaves;
            var busyPresenters = _savesCreator.CreateBusyPresenters(gameSaves.ToList());
            _savePresenters = _savePresenters.Concat(busyPresenters).ToList();
        }

        private void CreateEmptyPresenters(int busyPresentersAmount)
        {
            var emptyPresenters = _savesCreator.CreateEmptyPresenters(busyPresentersAmount, _savesAmount);
            _savePresenters = _savePresenters.Concat(emptyPresenters).ToList();
        }

        private void SelectEmptyPresenter(EmptySavePresenter presenter)
        {
            if(_selectedPresenter == presenter) return;
            if( _selectedPresenter != null) _selectedPresenter.Unselect();
            
            presenter.Select();
            _savesImageComponent.ChangeImage(_newSaveSprite, presenter);
            _selectedPresenter = presenter;
        }

        private void SelectBusyPresenter(BusySavePresenter presenter)
        {
            if(_selectedPresenter == presenter) return;
            if( _selectedPresenter != null) _selectedPresenter.Unselect();
            
            presenter.Select();
            _savesImageComponent.ChangeImage(presenter.TrackedSave.GetImage, presenter);
            _selectedPresenter = presenter;
        }

        private void ClearSavePresenter(BusySavePresenter presenter)
        {
            _saveManager.DeleteSave(presenter.TrackedSave.ID);
            _savePresenters.Remove(presenter);
            Destroy(presenter.gameObject);
            _savesCreator.CreateEmptyPresenter(_savesAmount);
        }
    }
}
