using System.Collections;
using _3ClipseGame.Steam.Mechanics.LootSystem.InGame.Scripts.Detector;
using _3ClipseGame.Steam.Mechanics.LootSystem.InGame.Scripts.LootComponent;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;

namespace _3ClipseGame.Tests.LootTests.PlayMode.in_game
{
    public class loot_detector
    {
        private GameObject _loot;
        private LootDetector _lootDetector;

        private bool _isRetireEventTriggered;
        private bool _isDetectedEventTriggered;

        [UnitySetUp]
        public IEnumerator Init()
        {
            SceneManager.LoadScene("loot_detector_test_scene");
            yield return new WaitForSeconds(0.1f);

            _isDetectedEventTriggered = false;
            _isRetireEventTriggered = false;

            InitializeLoot();
            InitializeDetector();
        }

        private void InitializeLoot()
        {
            _loot = Object.FindObjectOfType<PickableLoot>().gameObject;
        }

        private void InitializeDetector()
        {
            _lootDetector = Object.FindObjectOfType<LootDetector>();

            _lootDetector.LootRetired += OnLootRetired;
            _lootDetector.LootDetected += OnLootDetected;
        }

        private void OnLootDetected(PickableLoot _) => _isDetectedEventTriggered = true;
        private void OnLootRetired(PickableLoot _) => _isRetireEventTriggered = true;

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

            yield return null;

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
    }
}