using System;
using System.Collections.Specialized;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Display
{
    public class OrderedInteractablesDictionary
    {
        public int Count => _dictionary.Count;
        
        private OrderedDictionary _dictionary = new();

        public void AddElement(Interactable key, InteractablePresenter value)
        {
            try
            {
                _dictionary.Add(key, value);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
        }

        public void RemoveElement(Interactable key) => _dictionary.Remove(key);

        public void RemoveElement(int id)
        {
            try
            {
                _dictionary.RemoveAt(id);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
        }

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

        public InteractablePresenter GetValueByID(int id)
        {
            try
            {
                return _dictionary[id] as InteractablePresenter;
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
                return null;
            }
        }

        public InteractablePresenter GetValueByKey(Interactable key)
        {
            try
            {
                return _dictionary[key] as InteractablePresenter;
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
                return null;
            }
        }

        public InteractablePresenter GetNextElement(InteractablePresenter interactablePresenter)
        {
            var id = GetIDByValue(interactablePresenter);
            
            if (id == _dictionary.Count - 1) id = 0;
            else id++;
            
            return _dictionary[id] as InteractablePresenter;
        }

        public InteractablePresenter GetPreviousElement(InteractablePresenter interactablePresenter)
        {
            var id = GetIDByValue(interactablePresenter);

            if (id == 0) id = _dictionary.Count - 1;
            else id--;
            
            return _dictionary[id] as InteractablePresenter;
        }

        public int GetIDByValue(InteractablePresenter value)
        {
            for (var i = 0; i < _dictionary.Count; i++)
            {
                if (value == GetValueByID(i)) return i;
            }

            return 0;
        }
    }
}