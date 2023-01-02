using System;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Stelas.KeyLogic.ExploredStela
{
    public class ExploredStela : Stela<ExploredStelaPresenter>
    {
        public override event Action<Interactable<ExploredStelaPresenter>> Disappeared;

        public override ExploredStelaPresenter GetPresenter()
        {
            throw new NotImplementedException();
        }
        public override void Activate(){}
    }
}