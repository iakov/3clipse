using System;

namespace _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.LootComponent
{
    public abstract class ResourcePickableLoot : PickableLoot
    {
        public abstract override event Action<PickableLoot> Disappeared;
        public abstract override void Disappear();
    }
}
