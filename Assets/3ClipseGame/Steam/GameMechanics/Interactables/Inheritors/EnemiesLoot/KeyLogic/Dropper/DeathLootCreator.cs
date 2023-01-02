using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.EnemiesLoot.KeyLogic.Dropper
{
    public class DeathLootCreator : MonoBehaviour
    {
        [SerializeField] private LootPool _lootPool;
        [SerializeField] private float _dropVerticalForce = 100f;
        [SerializeField] private float _dropHorizontalForce = 10f;
        [SerializeField] private Transform _lootParent;

        public GameObject GetLoot(Vector3 position)
        {
            var loot = _lootPool.GetPoolObject();
            
            SetParent(loot);
            SetActive(loot);
            SetPosition(loot, position);
            SetDropVelocity(loot);
            
            return loot;
        }

        private void SetParent(GameObject loot)
        {
            loot.transform.parent = _lootParent;
        }

        private void SetActive(GameObject loot)
        {
            loot.SetActive(true);
        }

        private void SetPosition(GameObject loot, Vector3 position)
        {
            loot.transform.position = position;
        }

        private void SetDropVelocity(GameObject loot)
        {
            var randomVector = new Vector3(Random.Range(-1f, 1f) * _dropHorizontalForce,
                Random.Range(-1f, 1f) * _dropVerticalForce, Random.Range(-1f, 1f) * _dropHorizontalForce);
            loot.GetComponent<Rigidbody>().AddForce(randomVector, ForceMode.Impulse);
        }
    }
}