using System;
using System.Collections.Generic;
using _3ClipseGame.Steam.GameMechanics.GameSaves.InGame.Data;
using _3ClipseGame.Steam.GameMechanics.GameSaves.UI.Scripts.Interfaces;
using _3ClipseGame.Steam.GameMechanics.GameSaves.UI.Scripts.SavePresenters;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.GameSaves.UI.Scripts.SaveCreators
{
    public class SavesCreatorEventsWrapper : ISavesCreator, ISavePresentersEventsObserver
    {
        public SavesCreatorEventsWrapper(ISavesCreator savesCreator)
        {
            _savesCreator = savesCreator;
        }

        public event Action<SavePresenter, Sprite> PresenterSelected;
        public event Action<BusySavePresenter> PresenterCleared;

        private readonly ISavesCreator _savesCreator;

        public List<BusySavePresenter> CreateBusyPresenters(List<GameSave> saves)
        {
            var presenters = _savesCreator.CreateBusyPresenters(saves);
            foreach (var presenter in presenters) SubscribeToBusyEvents(presenter);
            return presenters;
        }

        public BusySavePresenter CreateBusyPresenter(GameSave save, int id)
        {
            var busyPresenter = _savesCreator.CreateBusyPresenter(save, id);
            SubscribeToBusyEvents(busyPresenter);
            return busyPresenter;
        }

        private void SubscribeToBusyEvents(BusySavePresenter busySavePresenter)
        {
            busySavePresenter.Cleared += OnCleared;
            busySavePresenter.Selected += OnSaveSelected;
        }

        public List<EmptySavePresenter> CreateEmptyPresenters(int busyPresentersAmount, int globalPresentersAmount)
        {
            var presenters = _savesCreator.CreateEmptyPresenters(busyPresentersAmount, globalPresentersAmount);
            foreach (var presenter in presenters) SubscribeToEmptyEvents(presenter);
            return presenters;
        }

        public EmptySavePresenter CreateEmptyPresenter(int id)
        {
            var emptyPresenter = _savesCreator.CreateEmptyPresenter(id);
            SubscribeToEmptyEvents(emptyPresenter);
            return emptyPresenter;
        }
        
        private void SubscribeToEmptyEvents(SavePresenter emptySavePresenter)
        {
            emptySavePresenter.Selected += OnSaveSelected;
        }

        private void OnCleared(BusySavePresenter busySavePresenter)
        {
            PresenterCleared?.Invoke(busySavePresenter);
        }

        private void OnSaveSelected(SavePresenter presenter, Sprite image)
        {
            PresenterSelected?.Invoke(presenter, image);
        }
    }
}