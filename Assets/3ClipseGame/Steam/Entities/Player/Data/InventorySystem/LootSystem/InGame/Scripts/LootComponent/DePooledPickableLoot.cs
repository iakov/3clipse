using System;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.InGame.Scripts.LootComponent
{
    public class DePooledPickableLoot : PickableLoot
    {
        #region Public

        public override event Action<PickableLoot> Disappeared;

        public override void Disappear()
        {
            Destroy(gameObject);
            Disappeared?.Invoke(this);
        }

        #endregion
    }
}