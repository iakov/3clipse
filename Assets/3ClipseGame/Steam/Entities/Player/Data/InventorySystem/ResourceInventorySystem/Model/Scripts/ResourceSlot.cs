using System;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ResourceInventorySystem.Model.Scripts
{
    public class ResourceSlot
    {
        #region PublicFields

        public bool IsEmpty => CurrentAmount == 0;
        public bool IsFull => CurrentAmount >= Resource.GetMaximumAmountInSlot();
        
        public Resource Resource;
        public int CurrentAmount;

        #endregion

        #region Events

        public event Action AmountChanged;

        #endregion

        #region PublicMethods

        public void AddAmount(int addAmount, out int oddAmount)
        {
            oddAmount = 0;
            if (addAmount == 0 || Resource.GetMaximumAmountInSlot() == CurrentAmount) return;

            CurrentAmount += addAmount;
            AmountChanged?.Invoke();

            if (CurrentAmount <= Resource.GetMaximumAmountInSlot()) return;

            oddAmount = CurrentAmount - Resource.GetMaximumAmountInSlot();
            CurrentAmount = Resource.GetMaximumAmountInSlot();
        }

        public bool TryTakeAmount(int amount)
        {
            if (amount > CurrentAmount) return false;
            CurrentAmount -= amount;
            
            if (CurrentAmount == 0) Resource = null;
            return true;
        }

        #endregion
    }
}
