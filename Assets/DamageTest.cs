using _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.LootSystem.Scripts;
using _3ClipseGame.Steam.Entities.Player.MainCharacter;
using UnityEngine;

[RequireComponent(typeof(DeathLoot))]
public class DamageTest : MonoBehaviour
{
    private DeathLoot _deathLoot;

    private void Awake() => _deathLoot = GetComponent<DeathLoot>();

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out MainCharacter mainCharacterComponent)) return;
        
        Debug.Log("Destroy");
        _deathLoot.Drop();
        Destroy(gameObject);
    }
}
