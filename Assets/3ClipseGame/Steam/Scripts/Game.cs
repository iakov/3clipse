using System;
using UnityEngine;

namespace _3ClipseGame.Steam.Scripts
{
    [RequireComponent(typeof(InputManager))]
    [RequireComponent(typeof(CursorScript))]
    public class Game : MonoBehaviour
    {
        public static Game Instance { get; private set; }
        [NonSerialized] public InputManager InputManager;
        [NonSerialized] public CursorScript CursorScript;

        private void Awake()
        {
            Instance = this;
            
            InputManager = GetComponent<InputManager>();
            CursorScript = GetComponent<CursorScript>();
        }
    }
}
