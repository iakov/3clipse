using System;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables
{
    public abstract class Interactable<T> : MonoBehaviour where T : InteractablePresenter
    {
        [SerializeField] protected T PresenterPrefab;
        public abstract event Action<Interactable<T>> Disappeared;
        
        public abstract T GetPresenter();
        public abstract void Activate();
    }
}
