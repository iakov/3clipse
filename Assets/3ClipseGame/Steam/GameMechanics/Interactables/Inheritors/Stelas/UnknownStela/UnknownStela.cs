using System;
using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Stelas.KeyLogic;
using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Stelas.KeyLogic.UnknownStela;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Stelas.UnknownStela
{
    public class UnknownStela : Stela
    {
        [SerializeField] private UnknownStelaPresenter _stelaPresenter;
        [SerializeField] private KeyLogic.ExploredStela.ExploredStela _afterExplorationStela;

        public override event Action<Stela> StelaActivated;
        public override event Action<Interactable> Disappeared;

        public override InteractablePresenter GetNewPresenter() => _stelaPresenter;

        public override void Activate()
        {
            StelaActivated?.Invoke(this);
            Instantiate(_afterExplorationStela, transform.parent);
            Destroy(gameObject);
        }
    }
}