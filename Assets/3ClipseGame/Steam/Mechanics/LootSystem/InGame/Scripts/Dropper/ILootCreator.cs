using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.LootSystem.InGame.Scripts.Dropper
{
    public interface ILootCreator
    {
        public GameObject CreateLootObjectInPosition(Transform positionTransform);
    }
}