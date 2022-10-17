using System.Collections.Generic;
using UnityEngine;

namespace _3ClipseGame.Steam.Core.GameSource.Parts.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private List<PlayerEntity> _allPossiblePlayerEntities;
        [SerializeField] private PlayerEntity _currentPlayerEntity;
        
        public PlayerEntity GetCurrentPlayerEntity() => _currentPlayerEntity;

        private void OnEnable() => _currentPlayerEntity.SwitchingToNewEntity += ChangeEntity;

        private void ChangeEntity()
        {
            _currentPlayerEntity.SwitchingToNewEntity -= ChangeEntity;
            _currentPlayerEntity.LoseControl();
            _currentPlayerEntity = FindNextEntity();
            _currentPlayerEntity.TakeControl();
            _currentPlayerEntity.SwitchingToNewEntity += ChangeEntity;
        }

        private PlayerEntity FindNextEntity()
        {
            var currentType = _currentPlayerEntity.GetPlayerEntityType();
            return _allPossiblePlayerEntities.Find(entity => entity.GetPlayerEntityType() != currentType);
        }
    }
}
