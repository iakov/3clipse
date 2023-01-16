using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Dialogues.KeyLogic.Narration
{
    [CreateAssetMenu(fileName = "NarrationCharacter", menuName = "Interactables/Dialogue/Narration/NarrationCharacter")]
    public class NarrationCharacter: ScriptableObject
    {
        [SerializeField] private string _characterName;
        public string CharacterName => _characterName;
    }
}