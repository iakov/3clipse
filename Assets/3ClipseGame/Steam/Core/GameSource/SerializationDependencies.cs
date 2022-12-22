using UnityEngine;
using CharacterController = _3ClipseGame.Steam.Entities.Scripts.CustomController.CharacterController;

namespace _3ClipseGame.Steam.Core.GameSource
{
    public class SerializationDependencies : MonoBehaviour
    {
        [SerializeField] private Parts.Player.Player _player;
        [SerializeField] private CharacterController _mainCharacter;

        public Parts.Player.Player Player => _player;
        public CharacterController MainCharacter => _mainCharacter;
    }
}