using _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.ResourceInventorySystem.ScriptableObjects;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ScriptableObjects.Resources
{
    public class ResourceSlot
    {
        #region PublicFields

        public bool IsEmpty { get; private set; } = true;
        public bool IsFull { get; private set; }
        
        public Resource Resource;
        public int CurrentAmount;

        #endregion

        #region PublicMethods
        
        public void AddAmount(int addAmount, out int oddAmount)
        {
            oddAmount = 0;
            CurrentAmount += addAmount;

            IsEmpty = CurrentAmount == 0;
            IsFull = CurrentAmount == Resource.MaximumAmountInSlot;
            
            if (CurrentAmount <= Resource.MaximumAmountInSlot) return;

            IsFull = true;
            oddAmount = CurrentAmount - Resource.MaximumAmountInSlot;
            CurrentAmount = Resource.MaximumAmountInSlot;
        }

        public bool TryTakeAmount(int amount)
        {
            if (amount > CurrentAmount) return false;
            CurrentAmount -= amount;
            
            if (CurrentAmount != 0) return true;
            
            IsEmpty = true;
            Resource = null;

            return true;
        }

        #endregion
        
    }
}
