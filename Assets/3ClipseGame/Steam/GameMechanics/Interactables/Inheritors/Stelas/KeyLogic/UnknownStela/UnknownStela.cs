using System;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Stelas.KeyLogic.UnknownStela
{
    public class UnknownStela : Stela<UnknownStelaPresenter>
    {
        public override event Action<Interactable<UnknownStelaPresenter>> Disappeared;
        
        public override UnknownStelaPresenter GetPresenter(){}
        public override void Activate(){}
    }
}