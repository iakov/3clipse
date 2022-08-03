using System;
using _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.LootSystem.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem
{
    public class Item : ScriptableObject
    {
        #region SerializeFields

        [SerializeField] private new string name;
        [TextArea(0, 10)] [SerializeField] private string description;
        [SerializeField] private string id;
        [SerializeField] private Sprite uiImage;
        [SerializeField] private GameObject lootPrefab;

        #endregion

        #region PublicGetters

        public string Name => name;
        public string Description => description;
        public string ID => id;
        public Sprite UIImage => uiImage;
        public GameObject LootPrefab => lootPrefab;

        #endregion
    }
}
