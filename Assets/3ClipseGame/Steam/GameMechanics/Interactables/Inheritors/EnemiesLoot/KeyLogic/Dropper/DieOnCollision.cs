using _3ClipseGame.Steam.GameCore.Origin.Parts.Player.MainCharacter;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.EnemiesLoot.KeyLogic.Dropper
{
    public class DieOnCollision : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.TryGetComponent<MainCharacterBody>(out var mainCharacter) == false) return;
            
            Destroy(gameObject);
        }
    }
}
