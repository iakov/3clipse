using System.Collections;
using _3ClipseGame.Steam.Entities.Player.Data.Scripts.InventorySystem.LootSystem.Scripts;
using _3ClipseGame.Steam.Entities.Player.Scripts;
using UnityEngine;

[RequireComponent(typeof(DeathLoot))]
public class Test : MonoBehaviour
{
    [SerializeField] [Min(0f)] private float dropRate;
    [SerializeField] [Min(0f)] private float waitAfterLastDropTime = 0.1f;

    private bool _isDying = false;
    private IEnumerator OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.GetComponentInParent<Player>() || _isDying) yield break;

        _isDying = true;
        var deathLoot = GetComponent<DeathLoot>();
        yield return deathLoot.Drop(dropRate);
        yield return new WaitForSeconds(waitAfterLastDropTime);
        Destroy(gameObject);
    }
}
