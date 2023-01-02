using System.Collections.Generic;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.EnemiesLoot.KeyLogic.Dropper
{
    public class DropLootOnDeath : MonoBehaviour
    {
        [SerializeField] private List<DropElement> _dropElements;
        [SerializeField] private Transform _spawnDropPosition;

        [SerializeField] private DeathLootCreator _deathLootCreator;

        private void OnDestroy()
        {
            foreach (var dropElement in _dropElements) DropLoot(dropElement);
        }

        private void DropLoot(DropElement element)
        {
            var amount = element.GetDropAmount();
            var lootObject = _deathLootCreator.GetLoot(_spawnDropPosition.position);
            var lootComponent = lootObject.GetComponent<Loot>();
            //TODO: Change Resource and Amount!
        }
    }
}