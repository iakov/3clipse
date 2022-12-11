using System.Collections.Generic;
using System.Linq;
using _3ClipseGame.Steam.Mechanics.Save.InGame.Data;
using _3ClipseGame.Steam.Mechanics.Save.UI.Scripts.Interfaces;
using _3ClipseGame.Steam.Mechanics.Save.UI.Scripts.SavePresenters;

namespace _3ClipseGame.Steam.Mechanics.Save.UI.Scripts.SaveCreators
{
    public class AdvancedSavesCreator
    {
        public AdvancedSavesCreator(GameSave[] gameSaves, ISavesCreator savesCreator)
        {
            _gameSaves = gameSaves.ToList();
            
            var newSavesCreatorEventsWrapped = new SavesCreatorEventsWrapper(savesCreator);
            _savesCreator = newSavesCreatorEventsWrapped;
            PresentersEvents = newSavesCreatorEventsWrapped;
        }

        public ISavePresentersEventsObserver PresentersEvents;
        
        private List<SavePresenter> _savePresenters;
        private List<GameSave> _gameSaves;
        private ISavesCreator _savesCreator;

        public List<SavePresenter> CreateAllPresenters(int amount)
        {
            _savePresenters = new List<SavePresenter>();

            CreateBusyPresenters();
            CreateEmptyPresenters(_savePresenters.Count, amount);
            return _savePresenters;
        }

        private void CreateBusyPresenters()
        {
            var busyPresenters = _savesCreator.CreateBusyPresenters(_gameSaves);
            _savePresenters = _savePresenters.Concat(busyPresenters).ToList();
        }

        private void CreateEmptyPresenters(int busyPresentersAmount, int savesAmount)
        {
            var emptyPresenters = _savesCreator.CreateEmptyPresenters(busyPresentersAmount, savesAmount);
            _savePresenters = _savePresenters.Concat(emptyPresenters).ToList();
        }
    }
}