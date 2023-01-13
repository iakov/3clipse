using System;
using System.Collections.Specialized;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Display
{
    public class OrderedInteractablesDictionary
    {
        public int Count => _dictionary.Count;
        
        private OrderedDictionary _dictionary = new();

        public void AddElement(Interactable key, InteractablePresenter value) => _dictionary.Add(key, value);

        public void RemoveElement(Interactable key) => _dictionary.Remove(key);
        
        public bool Contains(Interactable key)
        {
            try
            {
                var element = _dictionary[key];
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public InteractablePresenter GetValueByID(int id) =>  _dictionary[id] as InteractablePresenter;

        public InteractablePresenter GetValueByKey(Interactable key) => _dictionary[key] as InteractablePresenter;

        public InteractablePresenter GetNextElement(InteractablePresenter interactablePresenter)
        {
            var currentID = GetIDByValue(interactablePresenter);
            if (currentID == _dictionary.Count - 1) return interactablePresenter;
            return _dictionary[++currentID] as InteractablePresenter;
        }

        public InteractablePresenter GetPreviousElement(InteractablePresenter interactablePresenter)
        {
            var currentID = GetIDByValue(interactablePresenter);
            if (currentID == 0) return interactablePresenter;
            return _dictionary[--currentID] as InteractablePresenter;
        }

        private int GetIDByValue(InteractablePresenter value)
        {
            for (var i = 0; i < _dictionary.Count; i++)
            {
                if (value == GetValueByID(i)) return i;
            }

            return -1;
        }
    }
}