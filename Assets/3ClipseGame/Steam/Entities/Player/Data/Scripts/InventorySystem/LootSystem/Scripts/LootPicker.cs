using _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.ResourceInventorySystem.ScriptableObjects;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.LootSystem.Scripts
{
    public class LootPicker : MonoBehaviour
    {
        [SerializeField] private ResourceInventory resourceInventory;

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!hit.gameObject.TryGetComponent<PickableLoot>(out var lootComponent)) return;
            
            if(lootComponent.Item.GetType() == typeof(Resource)) resourceInventory.AddItem((Resource) lootComponent.Item, lootComponent.Amount);
            Destroy(hit.gameObject);
        }
    }
}
