using System;
using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Stelas.KeyLogic.UserInterface;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Stelas.KeyLogic.ExploredStela
{
    public class ExploredStela : Stela
    {
        [SerializeField] private ExploredStelaPresenter _stelaPresenter;
        [SerializeField] private TeleportData _teleportData;
        
        public override event Action<Stela> StelaActivated;
        public override event Action<Interactable> Disappeared;

        public override InteractablePresenter GetNewPresenter()
        {
            return _stelaPresenter;
        }

        public override void Activate() => StelaActivated?.Invoke(this);

        public TeleportData GetTeleportData() => _teleportData;
    }
}