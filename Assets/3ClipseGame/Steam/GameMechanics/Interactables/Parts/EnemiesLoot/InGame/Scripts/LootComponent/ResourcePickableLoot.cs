using System;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Parts.EnemiesLoot.InGame.Scripts.LootComponent
{
    public abstract class ResourcePickableLoot : PickableLoot
    {
        public abstract override event Action<Interactable> Disappeared;
        public abstract override void Disappear();
    }
}
