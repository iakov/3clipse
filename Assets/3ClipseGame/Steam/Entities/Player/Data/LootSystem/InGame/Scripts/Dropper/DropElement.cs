using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.ResourceInventorySystem.InGame.Scripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.Dropper
{
    [System.Serializable]
    public class DropElement
    {
        #region Serialization

        [SerializeField] private Resource _resource;
        [SerializeField] private int _maxDropAmount;
        [SerializeField] [Range(0, 1)] private float _dropChance;

        #endregion

        #region Initialization

        private int _finalAmount;
        private bool _isRandomCalculated;

        #endregion

        #region Public

        public int GetFinalAmountOfDrop()
        {
            if (_isRandomCalculated) return _finalAmount;
            
            _finalAmount = CalculateFinalAmount();
            _isRandomCalculated = true;
            return _finalAmount;
        }

        public Resource GetResource()
        {
            return _resource;
        }

        #endregion

        private int CalculateFinalAmount()
        {
            var finalQuantity = 0;
            
            for (var i = 0; i < _maxDropAmount; i++)
                if (RandomPercentage() < _dropChance) finalQuantity++;

            return finalQuantity;
        }

        private float RandomPercentage()
        {
            return Random.Range(0f, 1f);
        }
    }
}