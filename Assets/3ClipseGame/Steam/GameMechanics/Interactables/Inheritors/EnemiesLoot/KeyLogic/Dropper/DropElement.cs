using System;
using _3ClipseGame.Steam.GameMechanics.InventorySystem.ResourceInventorySystem.InGame.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.EnemiesLoot.KeyLogic.Dropper
{
    [Serializable]
    public class DropElement
    {
        [SerializeField] private int _maxAmount;
        [Range(0f, 1f)] [SerializeField] private float _dropChance;
        [SerializeField] private Resource _dropResource;

        public Resource DropResource => _dropResource;

        private int _finalAmount;
        private bool _isFinalAmountCalculated;
        
        public int GetDropAmount()
        {
            if (_isFinalAmountCalculated) return _finalAmount;

            _finalAmount = 0;
            for (var i = 0; i < _maxAmount; i++)
            {
                var random = Random.Range(0f, 1f);
                if (random < _dropChance) _finalAmount++;
            }

            _isFinalAmountCalculated = true;
            return _finalAmount;
        }
    }
}