using UnityEngine;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Player.MainCharacter
{
    public class MainCharacterBody : MonoBehaviour
    {
        [SerializeField] private MainCharacter _mainCharacter;
        public MainCharacter MainCharacter => _mainCharacter;
    }
}
