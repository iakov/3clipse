using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ResourceInventorySystem.Model.Scripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.Model.Dropper
{
    [System.Serializable]
    public struct DropElement
    {
        #region SerializeFields

        public Resource DropItem;
        [SerializeField] private int _maxDropAmount;
        [SerializeField] [Range(0, 1)] private float _dropChance;

        #endregion

        #region PrivateFields

        private int _finalAmount;
        private bool _isRandomCalculated;

        #endregion

        #region PublicMethods

        public int GetFinalAmountOfDrop()
        {
            if (_isRandomCalculated) return _finalAmount;

            var counter = 0;
            while (counter < _maxDropAmount)
            {
                var randomNumber = Random.Range(0, 100);
                if (randomNumber < _dropChance * 100) _finalAmount++;
                counter++;
            }

            _isRandomCalculated = true;
            return _finalAmount;
        }

        #endregion
    }
}