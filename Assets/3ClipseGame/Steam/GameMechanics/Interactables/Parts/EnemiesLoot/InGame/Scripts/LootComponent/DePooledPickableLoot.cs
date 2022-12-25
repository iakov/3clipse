using System;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Parts.EnemiesLoot.InGame.Scripts.LootComponent
{
    public class DePooledPickableLoot : ResourcePickableLoot
    {
        public override event Action<Interactable> Disappeared;

        public override void Disappear()
        {
            Disappeared?.Invoke(this);
            Destroy(gameObject);
        }
    }
}