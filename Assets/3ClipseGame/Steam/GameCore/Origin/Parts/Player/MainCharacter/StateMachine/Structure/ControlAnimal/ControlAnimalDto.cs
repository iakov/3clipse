using System.Runtime.Serialization;
using _3ClipseGame.Steam.GameCore.Origin.Parts.Input.Inputs.MovementInput;
using _3ClipseGame.Steam.GameCore.Origin.Parts.Player.Scripts;
using UnityEngine;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Player.MainCharacter.StateMachine.Structure.ControlAnimal
{
    public class ControlAnimalDto : Dto
    {
        [SerializeField] private MovementInputProcessor _inputProcessor;
        
        public MovementInputProcessor InputProcessor => _inputProcessor;

        private void Start()
        {
            CheckForExceptions();
        }

        private void CheckForExceptions()
        {
            if (_inputProcessor == null) throw new SerializationException();
        }
    }
}