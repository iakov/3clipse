using System;
using System.Collections.Generic;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.Scripts
{
    public abstract class ItemInventory<T1, T2> : ScriptableObject where T1 : Item where T2 : ItemSlot<T1>
    {
        public abstract event Action<T2> SlotChanged;
        public abstract IEnumerable<T2> GetSlots();
        public abstract bool TryRemoveItem(T1 item, int amount = 1);
        public abstract bool TryAddItem(T1 item, int amount = 1);
    }
}
