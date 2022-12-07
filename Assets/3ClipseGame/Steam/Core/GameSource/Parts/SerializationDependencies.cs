using UnityEngine;

namespace _3ClipseGame.Steam.Core.GameSource.Parts
{
    public class SerializationDependencies : MonoBehaviour
    {
        [SerializeField] private Player.Player _player;

        public Player.Player Player => _player;
    }
}