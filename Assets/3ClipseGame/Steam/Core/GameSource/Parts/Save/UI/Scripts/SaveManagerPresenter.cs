using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _3ClipseGame.Steam.Core.GameSource.Parts.Save.InGame;
using _3ClipseGame.Steam.Core.GameSource.Parts.Save.InGame.Data;
using UnityEngine;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Save.UI.Scripts
{
    public class SaveManagerPresenter : MonoBehaviour
    {
        [SerializeField] private int _savesAmount = 4;
        [SerializeField] private LayoutGroup _layoutGroup;
        [SerializeField] private RectTransform _emptySaveSlot;
        [SerializeField] private RectTransform _busySaveSlot;

        private List<SavePresenter> _savePresenters;
        private List<BusySavePresenter> _busySavePresenters;
        private List<EmptySavePresenter> _emptySavePresenters;

        private SaveManager _saveManager => SaveManager.Instance;

        private void OnEnable()
        {
            _busySavePresenters = new List<BusySavePresenter>();
            _emptySavePresenters = new List<EmptySavePresenter>();
            _savePresenters = new List<SavePresenter>();
            StartCoroutine(DisplaySaveSlotsWithDelay());
        }

        private IEnumerator DisplaySaveSlotsWithDelay()
        {
            while (SaveManager.Instance == null || SaveManager.Instance.IsSavesFound == false) 
                yield return null;
            
            DisplayAllSlots();
        }

        private void DisplayAllSlots()
        {
            var saves = _saveManager.GameSaves.ToList();
            
            CreateBusyPresenters(saves);
            CreateEmptyPresenters();
        }

        private void CreateBusyPresenters(List<GameSave> saves)
        {
            foreach (var save in saves)
            {
                var savePresenter = CreateBusyPresenter(save);
            }
        }

        private BusySavePresenter CreateBusyPresenter(GameSave save)
        {
            var newObject = Instantiate(_busySaveSlot, _layoutGroup.transform);
            var savePresenter = newObject.GetComponent<BusySavePresenter>();
            
            savePresenter.ChangeTrackedSave(save);
            savePresenter.Clicked += LoadSave;
            savePresenter.Cleared += ClearSavePresenter;
            
            _busySavePresenters.Add(savePresenter);
            _savePresenters.Add(savePresenter);
            
            return savePresenter;
        }

        private void LoadSave(GameSave save)
        {
            _saveManager.LoadGame(save.ID);
        }

        private void ClearSavePresenter(BusySavePresenter presenter, GameSave save)
        {
            presenter.Cleared -= ClearSavePresenter;
            presenter.Clicked -= LoadSave;
            
            _saveManager.DeleteSave(save.ID);
            Destroy(presenter.gameObject);
            CreateBusyPresenter(save);

            _savePresenters.Remove(presenter);
            _busySavePresenters.Remove(presenter);
        }

        private void CreateEmptyPresenters()
        {
            var emptySavesAmount = _savesAmount - _savePresenters.Count;
            if(emptySavesAmount <= 0) return;

            for (var i = 0; i < emptySavesAmount; i++)
            {
                var savePresenter = CreateEmptyPresenter();
            }
        }

        private EmptySavePresenter CreateEmptyPresenter()
        {
            var newObject = Instantiate(_emptySaveSlot, _layoutGroup.transform);
            var savePresenter = newObject.GetComponent<EmptySavePresenter>();
            
            savePresenter.Clicked += CreateNewSave;
            
            _emptySavePresenters.Add(savePresenter);
            _savePresenters.Add(savePresenter);
            
            return savePresenter;
        }

        private void CreateNewSave(EmptySavePresenter presenter)
        {
            presenter.Clicked -= CreateNewSave;
            Destroy(presenter.gameObject);

            _saveManager.NewGame();
        }
    }
}
