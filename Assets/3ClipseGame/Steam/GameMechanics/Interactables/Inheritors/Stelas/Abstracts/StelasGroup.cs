using System.Collections.Generic;
using System.Linq;
using _3ClipseGame.Steam.GameCore.Origin;
using _3ClipseGame.Steam.GameCore.Origin.Interfaces;
using _3ClipseGame.Steam.GameCore.Origin.Parts.GameStates;
using _3ClipseGame.Steam.GameCore.Origin.Parts.UserInterface;
using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Stelas.KeyLogic;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Stelas.Abstracts
{
    public class StelasGroup : MonoBehaviour
    {
        [SerializeField] private GameObject _stelaMenu;
        
        private List<Stela> _allStelas;
        private List<KeyLogic.ExploredStela.ExploredStela> _exploredStelas;
        private List<Stelas.UnknownStela.UnknownStela> _unknownStelas;

        private ISoloManager<GameStateType> _statesManager => GameSource.Instance.GetStatesManager();
        private UIManager _uiManager => GameSource.Instance.GetUIManager();

        private void Awake() => UpdateListsOfStelas();
        private void OnDisable() => UnsubscribeToEvents();

        public void UpdateListsOfStelas()
        {
            if (_allStelas != null) UnsubscribeToEvents();
            
            _allStelas = GetComponentsInChildren<Stela>().ToList();
            _unknownStelas = GetComponentsInChildren<Stelas.UnknownStela.UnknownStela>().ToList();
            _exploredStelas = GetComponentsInChildren<KeyLogic.ExploredStela.ExploredStela>().ToList();
            
            SubscribeToEvents();
        }

        private void UnsubscribeToEvents()
        {
            foreach (var stela in _allStelas)
                stela.StelaActivated -= DrawStelaMenu;
        }
        
        private void SubscribeToEvents()
        {
            foreach (var stela in _allStelas)
                stela.StelaActivated += DrawStelaMenu;
        }

        private void DrawStelaMenu(Stela drawStela)
        {
            _statesManager.Enable(GameStateType.Cinematic);
            _uiManager.DrawNewPanel(_stelaMenu, DrawMode.Mono);
        }

        public List<KeyLogic.ExploredStela.ExploredStela> GetExploredStelas() =>_exploredStelas;
    }
}
