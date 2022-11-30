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
        [Header("Saves")]
        [SerializeField] private int _savesAmount = 4;
        [SerializeField] private GameObject _emptySaveSlot;
        [SerializeField] private GameObject _busySaveSlot;
        [SerializeField] private List<Transform> _savesSpawnPositions;
        [SerializeField] private UnityEngine.Camera _camera;
        
        [Header("Image")]
        [SerializeField] private Image _imageComponent;
        [SerializeField] private Sprite _defaultSprite;

        private List<SavePresenter> _savePresenters;
        private List<BusySavePresenter> _busySavePresenters;
        private List<EmptySavePresenter> _emptySavePresenters;

        private SaveManager _saveManager => SaveManager.Instance;

        private void OnEnable()
        {
            DestroyAllPresenters();
            _busySavePresenters = new List<BusySavePresenter>();
            _emptySavePresenters = new List<EmptySavePresenter>();
            _savePresenters = new List<SavePresenter>();
            StartCoroutine(DisplaySaveSlotsWithDelay());
        }

        private void DestroyAllPresenters()
        {
            if(_savePresenters == null) return;

            foreach (var presenter in _savePresenters)
                Destroy(presenter.gameObject);
        }

        private IEnumerator DisplaySaveSlotsWithDelay()
        {
            Debug.Log("1");
            while (SaveManager.Instance == null || SaveManager.Instance.IsSavesFound == false) 
                yield return null;
            
            Debug.Log("2");
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
            Debug.Log("Busy Full");
            foreach (var save in saves)
            {
                Debug.Log("Busy");
                var savePresenter = CreateBusyPresenter(save);
                var canvas = savePresenter.GetComponentInChildren<Canvas>();
                canvas.worldCamera = _camera;
            }
        }

        private BusySavePresenter CreateBusyPresenter(GameSave save)
        {
            var objectIndex = _savePresenters.Count;
            var newObject = Instantiate(_busySaveSlot, _savesSpawnPositions[objectIndex]);
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
            _imageComponent.sprite = save.GetImage;
            //_saveManager.LoadGame(save.ID);
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
                var canvas = savePresenter.GetComponentInChildren<Canvas>();
                canvas.worldCamera = _camera;
            }
        }

        private EmptySavePresenter CreateEmptyPresenter()
        {
            var objectIndex = _savePresenters.Count;
            var newObject = Instantiate(_emptySaveSlot, _savesSpawnPositions[objectIndex]);
            var savePresenter = newObject.GetComponent<EmptySavePresenter>();
            
            savePresenter.Clicked += CreateNewSave;
            
            _emptySavePresenters.Add(savePresenter);
            _savePresenters.Add(savePresenter);
            
            return savePresenter;
        }

        private void CreateNewSave(EmptySavePresenter presenter)
        {
            _imageComponent.sprite = _defaultSprite;
            // presenter.Clicked -= CreateNewSave;
            // Destroy(presenter.gameObject);
            //
            // _saveManager.NewGame();
        }
    }
}
