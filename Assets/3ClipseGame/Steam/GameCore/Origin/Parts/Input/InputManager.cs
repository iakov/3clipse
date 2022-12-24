using System.Collections.Generic;
using _3ClipseGame.Steam.GameCore.GlobalScripts.Extensions;
using _3ClipseGame.Steam.GameCore.Origin.Interfaces;
using _3ClipseGame.Steam.GameCore.Origin.Parts.Input.Inputs;
using UnityEngine;

namespace _3ClipseGame.Steam.GameCore.Origin.Parts.Input
{
    public class InputManager : MonoBehaviour, IMultiManager<InputType>
    {
        [SerializeField] private List<InputProcessor> _inputProcessors;
        
        private List<InputProcessor> _disabledProcessors;
        private List<InputProcessor> _enabledProcessors;

        private void Awake()
        {
            _disabledProcessors = new(_inputProcessors);
            _enabledProcessors = new();
        }
        
        public void Enable(InputType enableObjectType)
        {
            var processors = FindWithType(enableObjectType);
            foreach (var processor in processors)
            {
                processor.Enable();
                _disabledProcessors.MoveToAnotherCollection(_enabledProcessors, processor);
            }
        }

        public InputType[] GetActive()
        {
            var activeTypes = GetTypes(_enabledProcessors);
            return activeTypes.ToArray();
        }

        public void Disable(InputType disableObjectType)
        {
            var processors = FindWithType(disableObjectType);
            foreach (var processor in processors)
            {
                processor.Disable();
                _enabledProcessors.MoveToAnotherCollection(_disabledProcessors, processor);
            }
        }

        public void DisableAll()
        {
            foreach (var processor in _enabledProcessors)
            {
                processor.Disable();
                _enabledProcessors.MoveToAnotherCollection(_disabledProcessors, processor);
            }
        }

        private List<InputProcessor> FindWithType(InputType type)
        {
            var found = _inputProcessors.FindAll(inputProcessor => inputProcessor.GetInputType() == type);
            return found;
        }
        
        private List<InputType> GetTypes(List<InputProcessor> processors)
        {
            List<InputType> types = new();
            processors.ForEach(processor => types.Add(processor.GetInputType()));
            return types;
        }
    }
}
