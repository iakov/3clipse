using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _3ClipseGame.Steam.Mechanics.Save.InGame;
using _3ClipseGame.Steam.Mechanics.Save.UI.Scripts.Interfaces;
using _3ClipseGame.Steam.Mechanics.Save.UI.Scripts.SavePresenters;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.Save.UI.Scripts
{
    [RequireComponent(typeof(ISavesCreator))]
    public class SaveManagerPresenter : MonoBehaviour
    {
        [SerializeField] private int _savesAmount = 4;
        [SerializeField] private SelectedSavePresenter _savesImageComponent;

        private SaveManager _saveManager => SaveManager.Instance;
        private ISavesCreator _savesCreator;
        private ISavePresentersEventsObserver _savePresentersEventsObserver;
        private List<SavePresenter> _savePresenters;
        private SavePresenter _selectedPresenter;

        #region Launch

        private void Awake()
        {
            var savesCreator = GetComponent<ISavesCreator>();
            var newSavesCreatorEventsWrapped = new SavesCreatorEventsWrapper(savesCreator);
            _savesCreator = newSavesCreatorEventsWrapped;
            _savePresentersEventsObserver = newSavesCreatorEventsWrapped;
        }

        private void OnEnable()
        {
            StartCoroutine(DisplaySaveSlotsWithDelay());
            _savePresentersEventsObserver.PresenterCleared += ClearSavePresenter;
            _savePresentersEventsObserver.PresenterSelected += SelectPresenter;
        }

        private void OnDisable()
        {
            _savePresentersEventsObserver.PresenterCleared -= ClearSavePresenter;
            _savePresentersEventsObserver.PresenterSelected -= SelectPresenter;
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

        #endregion

        private void SelectPresenter(SavePresenter presenter, Sprite image)
        {
            if(_selectedPresenter == presenter) return;
            if( _selectedPresenter != null) _selectedPresenter.Unselect();
            
            _savesImageComponent.ChangeImage(image, presenter);
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
