using System;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Stelas.KeyLogic
{
    public abstract class Stela<T> : Interactable<T> where T : StelaPresenter
    {
        public abstract override event Action<Interactable<T>> Disappeared;
        public abstract override T GetPresenter();
        public abstract override void Activate();
    }
}
