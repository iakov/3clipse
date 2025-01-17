using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.InventorySystem.Scripts
{
    public abstract class InventoryStorage<T> : MonoBehaviour where T : Item
    {
        public abstract void AddResources(T resource, int amount);
    }
}