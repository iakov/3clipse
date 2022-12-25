using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Parts.EnemiesLoot.InGame.Scripts.Dropper
{
    public interface ILootCreator
    {
        public GameObject CreateLootObjectInPosition(Transform positionTransform);
    }
}