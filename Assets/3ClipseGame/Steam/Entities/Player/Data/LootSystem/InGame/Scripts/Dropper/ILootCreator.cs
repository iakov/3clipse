using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.Dropper
{
    public interface ILootCreator
    {
        public GameObject CreateLootObjectInPosition(Transform positionTransform);
    }
}