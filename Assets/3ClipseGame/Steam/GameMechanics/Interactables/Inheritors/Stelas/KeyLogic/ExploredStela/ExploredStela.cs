using System;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Stelas.KeyLogic.ExploredStela
{
    public class ExploredStela : Stela
    {
        public override event Action<Interactable> Disappeared;

        public override InteractablePresenter GetNewPresenter()
        {
            return null;
        }
        
        public override void Activate(){}
    }
}