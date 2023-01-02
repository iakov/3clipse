using System;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Stelas.KeyLogic.UnknownStela
{
    public class UnknownStela : Stela
    {
        public override event Action<Interactable> Disappeared;

        public override InteractablePresenter GetNewPresenter()
        {
            return null;
        }
        
        public override void Activate(){}
    }
}