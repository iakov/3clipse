using _3ClipseGame.Steam.Entities.Player.MainCharacter.MainCharacterStateMachine.Structure.States;
using UnityEngine;

namespace _3ClipseGame.Steam.Global.UI.Scripts.Specifications
{
    public class ObserverSwapper : MonoBehaviour
    {
        [SerializeField] private RectTransform animalSpecifications;
        [SerializeField] private RectTransform characterSpecifications;

        public void SwapSpecifications(MainCharacterState oldState, MainCharacterState state)
        {
            var isAnimal = state.GetType() == typeof(AnimalControlState);
            
            animalSpecifications.gameObject.SetActive(isAnimal);
            characterSpecifications.gameObject.SetActive(!isAnimal);
        }
    }
}