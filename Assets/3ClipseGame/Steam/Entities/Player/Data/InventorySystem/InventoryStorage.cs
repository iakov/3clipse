using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ResourceInventorySystem.Model.Scripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem
{
    public class InventoryStorage : MonoBehaviour
    {
        [SerializeField] private ResourceInventory resourceInventory;
        public void AddResources(Resource resource, int amount) => resourceInventory.AddItem(resource, amount);
    }
}
