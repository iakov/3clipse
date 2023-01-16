using System;
using _3ClipseGame.Steam.GameMechanics.InventorySystem.Scripts;

namespace _3ClipseGame.Steam.GameMechanics.InventorySystem.ResourceInventorySystem.InGame.Scripts
{
    public class ResourceSlot : ItemSlot<Resource>
    {
        public ResourceSlot(Resource resource)
        {
            _resource = resource;
        }
        
        public event Action AmountChanged;

        public override bool GetIsEmpty() => _currentAmount <= 0;
        public override bool GetIsFull() => _currentAmount >= _resource.GetMaximumAmountInSlot();

        public override Resource GetItem() => _resource;
        public override int GetAmount() => _currentAmount;

        private Resource _resource;
        private int _currentAmount;

        public void AddAmount(int addAmount, out int oddAmount)
        {
            oddAmount = 0;
            if (addAmount == 0 || _resource.GetMaximumAmountInSlot() == _currentAmount) return;

            _currentAmount += addAmount;
            AmountChanged?.Invoke();

            if (_currentAmount <= _resource.GetMaximumAmountInSlot()) return;

            oddAmount = _currentAmount - _resource.GetMaximumAmountInSlot();
            _currentAmount = _resource.GetMaximumAmountInSlot();
        }

        public bool TryTakeAmount(int amount)
        {
            if (amount > _currentAmount) return false;
            _currentAmount -= amount;
            
            if (_currentAmount == 0) _resource = null;
            return true;
        }
    }
}
