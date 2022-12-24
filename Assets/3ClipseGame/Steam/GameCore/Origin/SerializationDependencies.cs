using _3ClipseGame.Steam.GameCore.Origin.Parts.Player.Interfaces;
using UnityEngine;
using CharacterController = _3ClipseGame.Steam.GameCore.GlobalScripts.EntityScripts.CharacterController;

namespace _3ClipseGame.Steam.GameCore.Origin
{
    public class SerializationDependencies : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private CharacterController _mainCharacter;

        public Player Player => _player;
        public CharacterController MainCharacter => _mainCharacter;
    }
}