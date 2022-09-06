using System;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ResourceInventorySystem.Model.Scripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Model.Picker
{
    public class PickableLoot : MonoBehaviour
    {
        public Resource Resource
        {
            get => _resource;
            set
            {
                ResourceChanged?.Invoke();
                _resource = value;
            }
        }
        public int Amount
        {
            get => _amount;
            set
            {
                AmountChanged?.Invoke();
                _amount = value;
            }
        }

        private int _amount;
        private Resource _resource;

        public event Action AmountChanged;
        public event Action ResourceChanged;
    }
}
