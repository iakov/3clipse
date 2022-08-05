using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ResourceInventorySystem.ScriptableObjects;
using _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.ResourceInventorySystem.ScriptableObjects;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.Scripts
{
    public class InventoryStorage : MonoBehaviour
    {
        [SerializeField] private ResourceInventory resourceInventory;
        public void AddResources(Resource resource, int amount) => resourceInventory.AddItem(resource, amount);
    }
}
