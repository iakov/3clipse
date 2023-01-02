using System;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Stelas.KeyLogic
{
    public abstract class Stela : Interactable
    {
        public abstract override event Action<Interactable> Disappeared;
        public abstract override InteractablePresenter GetNewPresenter();
        public abstract override void Activate();
    }
}
