using System;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.LootComponent;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.InGame.Scripts.LootComponent
{
    public class DePooledPickableLoot : PickableLoot
    {
        public override event Action<PickableLoot> Disappeared;

        public override void Disappear()
        {
            Disappeared?.Invoke(this);
            Destroy(gameObject);
        }
    }
}