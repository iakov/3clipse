namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.Scripts
{
    public abstract class ItemSlot<T> where T : Item
    {
        public abstract bool GetIsEmpty();
        public abstract bool GetIsFull();
        
        public abstract T GetItem();
        public abstract int GetAmount();
    }
}
