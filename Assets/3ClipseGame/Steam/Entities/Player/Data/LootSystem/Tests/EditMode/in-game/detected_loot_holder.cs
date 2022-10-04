using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.Detector;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.LootComponent;
using NUnit.Framework;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Data.LootSystem.Tests.EditMode
{
    public class detected_loot_holder
    {
        private DetectedLootHolder _lootHolder;
        private PickableLoot _loot;
        
        [SetUp]
        public void Init()
        {
            InitializeHolder();
            InitializeLoot();
        }

        private void InitializeHolder()
        {
            _lootHolder = DetectedLootHolder.Empty();
        }

        private void InitializeLoot()
        {
            var gameObject = new GameObject();
            _loot = gameObject.AddComponent<DePooledPickableLoot>();
        }

        [Test] 
        public void test_add_new_loot_to_storage()
        {
            var isSuccessful = _lootHolder.TryAddLoot(_loot);
            var isContains = _lootHolder.Contains(_loot);

            Assert.IsTrue(isSuccessful);
            Assert.IsTrue(isContains);
        }

        [Test]
        public void test_add_same_loot_twice()
        {
            var isSuccessful = _lootHolder.TryAddLoot(_loot) && _lootHolder.TryAddLoot(_loot);
            var isContains = _lootHolder.Contains(_loot);
            
            Assert.IsFalse(isSuccessful);
            Assert.IsTrue(isContains);
        }

        [Test]
        public void test_add_loot_and_track_event()
        {
            var isEventInvoked = false;
            _lootHolder.LootAdded += _ => { isEventInvoked = true; };

            _lootHolder.TryAddLoot(_loot);
            
            Assert.IsTrue(isEventInvoked);
        }

        [Test]
        public void test_remove_existing_loot()
        {
            _lootHolder.TryAddLoot(_loot);
            var isSuccessful = _lootHolder.TryRemoveLoot(_loot);
            var isContains = _lootHolder.Contains(_loot);

            Assert.IsTrue(isSuccessful);
            Assert.IsFalse(isContains);
        }

        [Test]
        public void test_remove_non_existing_loot()
        {
            var isSuccessful = _lootHolder.TryRemoveLoot(_loot);
            var isContains = _lootHolder.Contains(_loot);
            
            Assert.IsFalse(isSuccessful);
            Assert.IsFalse(isContains);
        }

        [Test]
        public void test_remove_loot_and_track_event()
        {
            var isEventInvoked = false;
            _lootHolder.LootRemoved += _ => { isEventInvoked = true; };
            
            _lootHolder.TryAddLoot(_loot);
            _lootHolder.TryRemoveLoot(_loot);
            
            Assert.IsTrue(isEventInvoked);
        }
    }
}
