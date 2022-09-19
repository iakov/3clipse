using System.Collections;
using _3ClipseGame.Steam.Entities.Player.Data.InventorySystem.LootSystem.InGame.Scripts.Dropper;
using _3ClipseGame.Steam.Entities.Player.Scripts;
using _3ClipseGame.Steam.Entities.Scripts;
using UnityEngine;

[RequireComponent(typeof(DeathLootDropper))]
public class DieOnTouch : MonoBehaviour
{
    private Entity _entity;
    private bool _isDying = false;

    private void Awake()
    {
        _entity = GetComponent<Entity>();
    }
    
    private IEnumerator OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.GetComponentInParent<Player>() || _isDying) yield break;
        
        Destroy(gameObject);
    }
}
