using System;
using System.Collections.Generic;
using _3ClipseGame.Steam.Core.GameSource.Parts.Save.InGame.Data;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Save.UI.Scripts
{
    public class SavesCreatorEventsWrapper
    {
        public SavesCreatorEventsWrapper(SavesCreator savesCreator)
        {
            _savesCreator = savesCreator;
        }

        public event Action<EmptySavePresenter> EmptyPresenterClicked;
        public event Action<BusySavePresenter> BusyPresenterClicked; 
        public event Action<BusySavePresenter> PresenterCleared;

        private readonly SavesCreator _savesCreator;

        public List<BusySavePresenter> CreateBusyPresenters(List<GameSave> saves)
        {
            var presenters = _savesCreator.CreateBusyPresenters(saves);
            foreach (var presenter in presenters) SubscribeToBusyEvents(presenter);
            return presenters;
        }

        public SavePresenter CreateBusyPresenter(GameSave save, int id)
        {
            var busyPresenter = _savesCreator.CreateBusyPresenter(save, id);
            SubscribeToBusyEvents(busyPresenter);
            return busyPresenter;
        }

        private void SubscribeToBusyEvents(BusySavePresenter busySavePresenter)
        {
            busySavePresenter.Cleared += OnCleared;
            busySavePresenter.Clicked += OnBusyClicked;
        }

        public List<EmptySavePresenter> CreateEmptyPresenters(int busyPresentersAmount, int globalPresentersAmount)
        {
            var presenters = _savesCreator.CreateEmptyPresenters(busyPresentersAmount, globalPresentersAmount);
            foreach (var presenter in presenters) SubscribeToEmptyEvents(presenter);
            return presenters;
        }

        public SavePresenter CreateEmptyPresenter(int id)
        {
            var emptyPresenter = _savesCreator.CreateEmptyPresenter(id);
            SubscribeToEmptyEvents(emptyPresenter);
            return emptyPresenter;
        }
        
        private void SubscribeToEmptyEvents(EmptySavePresenter emptySavePresenter)
        {
            emptySavePresenter.Clicked += OnEmptyClicked;
        }

        private void OnCleared(BusySavePresenter busySavePresenter)
        {
            PresenterCleared?.Invoke(busySavePresenter);
        }
        
        private void OnBusyClicked(BusySavePresenter savePresenter)
        {
            BusyPresenterClicked?.Invoke(savePresenter);
        }
        
        private void OnEmptyClicked(EmptySavePresenter savePresenter)
        {
            EmptyPresenterClicked?.Invoke(savePresenter);
        }
    }
}