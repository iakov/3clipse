using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.Scripts
{
    public abstract class InventoryStorage<T> : MonoBehaviour where T : Item
    {
        public abstract void AddResources(T resource, int amount);
    }
}