using System;
using UnityEngine;

namespace _3ClipseGame.Steam.GameMechanics.Interactables
{
    public abstract class Interactable : MonoBehaviour
    {
        public abstract event Action<Interactable> Disappeared;
    }
}
