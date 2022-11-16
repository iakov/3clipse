using _3ClipseGame.Steam.Core.GameSource.Parts.Player;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame.Scripts
{
    public class SerializationDependencies : MonoBehaviour
    {
        [SerializeField] private Player _player;

        public Player Player => _player;
    }
}