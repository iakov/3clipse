using System.Collections.Generic;
using System.Linq;
using _3ClipseGame.Steam.Entities.Player.Data.Inventory.Scripts.ScriptableObjects.Resources.Item;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.Inventory.Scripts.ScriptableObjects.Resources.Inventory
{
    [CreateAssetMenu(fileName = "New Resource Inventory", menuName = "Inventory/Resources/Resource Inventory")]
    public class ResourceInventory : ScriptableObject
    {
        public List<ResourceSlot> Slots { get; private set; }

        private void Awake() => Slots ??= new List<ResourceSlot>();
        
        public void AddNewItem(Resource item, int amount)
        {
            foreach (var slot in Slots.Where(slot => slot.Item == item))
            {
                var isEnoughSpace = slot.TryIncreaseAmount(amount, out var extraAmount);
                if (!isEnoughSpace) Slots.Add(new ResourceSlot(item, extraAmount));
                
                return;
            }

            Slots.Add(new ResourceSlot(item, amount));
        }
    }
}
