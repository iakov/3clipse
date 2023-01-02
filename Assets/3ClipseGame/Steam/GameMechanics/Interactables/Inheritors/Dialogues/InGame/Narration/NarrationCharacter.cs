using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Parts.Dialogues.InGame.Narration
{
    [CreateAssetMenu(fileName = "NarrationCharacter", menuName = "Dialogue/Narration/NarrationCharacter")]
    public class NarrationCharacter: ScriptableObject
    {
        [SerializeField] private string _characterName;
        public string CharacterName => _characterName;
    }
}