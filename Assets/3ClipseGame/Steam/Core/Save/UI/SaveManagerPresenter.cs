using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Core.Save.UI
{
    public class SaveManagerPresenter : MonoBehaviour
    {
        public SaveManager SaveManager;
        [SerializeField] private VerticalLayoutGroup _layoutGroup;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private RectTransform _savePrefab;
        
        private SavePresenter _selectedSavePresenter;
        private SavePresenter _loadedSave;
        private List<SavePresenter> _allSavePresenters;

        private void Start()
        {
            _allSavePresenters = new List<SavePresenter>();
            CreateAllPresenters();
            if(_allSavePresenters.Count > 0) ChangeSelected(_allSavePresenters[0]);
        }
        
        private void CreateAllPresenters()
        {
            var allSaves = SaveManager._gameSaves;
            
            foreach (var saveObject in allSaves)
            {
                var save = (GameSave)saveObject;
                CreateNewSavePresenter(save);
            }
        }

        public void MakeSave()
        {
            if (_inputField.text == string.Empty) return;

            var saveName = _inputField.text;
            var isCreated = SaveManager.TryCreateNewSave(saveName, out var save);
            if(isCreated) CreateNewSavePresenter(save);
        }

        private void CreateNewSavePresenter(GameSave save)
        {
            var newObject = Instantiate(_savePrefab, _layoutGroup.transform);
            var savePresenter = newObject.GetComponent<SavePresenter>();
            savePresenter.ChangeTrackedGameSave(save);
            savePresenter.Selected += ChangeSelected;
            
            _allSavePresenters.Add(savePresenter);
        }
        
        private void ChangeSelected(SavePresenter presenter)
        {
            if(_selectedSavePresenter != null) _selectedSavePresenter.SetSelected(false);
            _selectedSavePresenter = presenter;
            _selectedSavePresenter.SetSelected(true);
        }
        
        public void LoadSave()
        {
            var save = _selectedSavePresenter.TrackedGameSave;
            SaveManager.TryLoadSave(save.Name);
        }

        public void DeleteSave()
        {
            var save = _selectedSavePresenter.TrackedGameSave;
            SaveManager.DeleteSave(save);
            var presenter = FindSavePresenter(save);
            DeleteSavePresenter(presenter);
        }
        
        private SavePresenter FindSavePresenter(GameSave save)
        {
            return _allSavePresenters.Find(presenter => presenter.TrackedGameSave == save);
        }
        
        private void DeleteSavePresenter(SavePresenter savePresenter)
        {
            savePresenter.Selected -= ChangeSelected;
            Destroy(savePresenter.gameObject);
            ChangeSelected();
            _allSavePresenters.Remove(savePresenter);
        }

        private void ChangeSelected()
        {
            var newSelected = TrySelectNext();
            if (!newSelected) newSelected = TrySelectPrevious();
            if(!newSelected) return;

            ChangeSelected(newSelected);
        }

        private SavePresenter TrySelectNext()
        {
            var currentSaveIndex = _allSavePresenters.FindIndex(presenter => presenter == _selectedSavePresenter);
            var maxIndex = _allSavePresenters.Count - 1;

            return currentSaveIndex == maxIndex 
                ? null 
                : _allSavePresenters[currentSaveIndex + 1];
        }

        private SavePresenter TrySelectPrevious()
        {
            var currentSaveIndex = _allSavePresenters.FindIndex(presenter => presenter == _selectedSavePresenter);

            return currentSaveIndex <= 0 
                ? null 
                : _allSavePresenters[currentSaveIndex - 1];
        }
    }
}
