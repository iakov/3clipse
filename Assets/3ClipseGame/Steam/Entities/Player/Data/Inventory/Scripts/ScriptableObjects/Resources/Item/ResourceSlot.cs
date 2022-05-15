namespace _3ClipseGame.Steam.Entities.Player.Data.Inventory.Scripts.ScriptableObjects.Resources.Item
{
    public class ResourceSlot
    {
        public int Amount { get; private set; }
        public readonly Resource Item;
        
        public ResourceSlot(Resource item, int amount)
        {
            Item = item;
            Amount = amount;
        }

        public bool TryIncreaseAmount(int increaseAmount, out int extraAmount)
        {
            if (Amount + increaseAmount > Item.MaximumAmountInSlot)
            {
                extraAmount = Amount + increaseAmount - Item.MaximumAmountInSlot;
                Amount += increaseAmount - extraAmount;
                return false;
            }

            extraAmount = 0;
            Amount += increaseAmount;
            return true;
        }

        public bool TryDecreaseAmount(int decreaseAmount)
        {
            if (Amount < decreaseAmount) return false;
            Amount -= decreaseAmount;
            return true;
        }
    }
}