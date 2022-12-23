using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _3ClipseGame.Steam.GameMechanics.GameSaves.InGame;
using _3ClipseGame.Steam.GameMechanics.GameSaves.UI.Scripts.Interfaces;
using _3ClipseGame.Steam.GameMechanics.GameSaves.UI.Scripts.SaveCreators;
using _3ClipseGame.Steam.GameMechanics.GameSaves.UI.Scripts.SavePresenters;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.GameSaves.UI.Scripts
{
    public class SavesListPanel : MonoBehaviour
    {
        [SerializeField] private int _savesAmount = 4;
        [SerializeField] private SelectedSavePresenter _savesImageComponent;
        [SerializeField] private SavesManager _savesManager;
        
        private List<SavePresenter> _savePresenters;
        private SavePresenter _selectedPresenter;
        private AdvancedSavesCreator _advancedSavesCreator;

        private void OnEnable()
        {
            _advancedSavesCreator = CreateSavesCreator();
            StartCoroutine(DisplaySaveSlotsWithDelay());
        }

        private void OnDisable()
        {
            _advancedSavesCreator.PresentersEvents.PresenterCleared -= ClearSavePresenter;
            _advancedSavesCreator.PresentersEvents.PresenterSelected -= SelectPresenter;
        }

        private IEnumerator DisplaySaveSlotsWithDelay()
        {
            while (_savesManager == null || _savesManager.IsSavesFound == false)
                yield return null;

            DestroyAllPresenters();
            CreatePresenters();
            
            _advancedSavesCreator.PresentersEvents.PresenterCleared += ClearSavePresenter;
            _advancedSavesCreator.PresentersEvents.PresenterSelected += SelectPresenter;
        }

        private void DestroyAllPresenters()
        {
            if (_savePresenters == null) return;
            foreach (var presenter in _savePresenters) Destroy(presenter.gameObject);
        }

        private void CreatePresenters()
        {
            _savePresenters = _advancedSavesCreator.CreateAllPresenters(_savesAmount);
        }

        private AdvancedSavesCreator CreateSavesCreator()
        { 
            var saves = _savesManager.GameSaves.ToArray();
            var iSavesCreator = GetComponent<ISavesCreator>();
            return new AdvancedSavesCreator(saves, iSavesCreator);
        }

        private void SelectPresenter(SavePresenter presenter, Sprite image)
        {
            if (_selectedPresenter == presenter) return;
            if (_selectedPresenter != null) _selectedPresenter.Unselect();

            _savesImageComponent.ChangeImage(image, presenter);
            _selectedPresenter = presenter;
        }

        private void ClearSavePresenter(BusySavePresenter presenter)
        {
            _savesManager.DeleteSave(presenter.TrackedSave.ID);
            _savePresenters.Remove(presenter);
        }
    }
}
