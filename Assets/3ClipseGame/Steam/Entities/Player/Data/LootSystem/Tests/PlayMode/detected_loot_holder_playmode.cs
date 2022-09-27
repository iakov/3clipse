using System.Collections;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.Detector;
using _3ClipseGame.Steam.Entities.Player.Data.LootSystem.InGame.Scripts.LootComponent;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;

namespace _3ClipseGame.Steam.Entities.Player.Data.LootSystem.Tests.PlayMode
{
    public class detected_loot_holder_playmode
    {
        #region SetUp

        private GameObject _loot;
        private LootDetector _lootDetector;

        private bool _isRetireEventTriggered;
        private bool _isDetectedEventTriggered;

        [SetUp]
        public void Init()
        {
            _isDetectedEventTriggered = false;
            _isRetireEventTriggered = false;
        
            InitializeLoot();
            InitializeDetector();
        }

        private void InitializeLoot()
        {
            _loot = new GameObject("Loot");
            _loot.AddComponent<SphereCollider>().radius = 0.05f;
            _loot.AddComponent<Rigidbody>();
            _loot.AddComponent<DePooledPickableLoot>();
            _loot.transform.position = new Vector3(0f, 2f, 0f);
        }

        private void InitializeDetector()
        {
            _lootDetector = new GameObject("Loot Detector").AddComponent<LootDetector>();
            _lootDetector.GetComponent<SphereCollider>().radius = 1f;

            _lootDetector.LootRetired += OnLootRetired;
            _lootDetector.LootDetected += OnLootDetected;
        }

        private void OnLootDetected(PickableLoot _) => _isDetectedEventTriggered = true;
        private void OnLootRetired(PickableLoot _) => _isRetireEventTriggered = true;

        #endregion

        #region Tests

        [UnityTest]
        public IEnumerator test_detecting_object_with_pickable_loot()
        {
            yield return new WaitForSeconds(0.5f);

            Assert.IsTrue(_isDetectedEventTriggered);
        }

        [UnityTest]
        public IEnumerator test_detecting_object_with_no_pickable_loot()
        {
            Object.Destroy(_loot.GetComponent<PickableLoot>());

            yield return new WaitForSeconds(0.5f);
        
            Assert.IsFalse(_isDetectedEventTriggered);
        }

        [UnityTest]
        public IEnumerator test_retiring_object_with_pickable_loot()
        {
            yield return new WaitForSeconds(1f);

            Assert.IsTrue(_isRetireEventTriggered);
        }

        [UnityTest]
        public IEnumerator test_retiring_object_which_was_disappeared()
        {
            yield return new WaitForSeconds(0.5f);
            _loot.GetComponent<PickableLoot>().Disappear();
            yield return null;
        
            Assert.IsTrue(_isRetireEventTriggered);
        }

        #endregion

        #region TearDown

        [TearDown]
        public void Clear()
        {
            Object.Destroy(_lootDetector.gameObject);
            if(_loot != null) Object.Destroy(_loot);
        }

        #endregion
    }
}