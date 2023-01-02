using System;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.EnemiesLoot.KeyLogic
{
    public class Loot : Interactable<LootPresenter>
    {
        public override event Action<Interactable<LootPresenter>> Disappeared;

        public override LootPresenter GetPresenter()
        {
            throw new NotImplementedException();
        }
        public override void Activate(){}
    }
}