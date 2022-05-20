using System;
using _3ClipseGame.Steam.Scripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Globals.Scripts
{
    [RequireComponent(typeof(InputManager))]
    [RequireComponent(typeof(CursorScript))]
    public class Game : MonoBehaviour
    {
        public static Game Instance { get; private set; }
        [NonSerialized] public CameraManager CameraManager;
        [NonSerialized] public InputManager InputManager;
        [NonSerialized] public CursorScript CursorScript;

        private void Awake()
        {
            Instance = this;

            CameraManager = GetComponent<CameraManager>();
            InputManager = GetComponent<InputManager>();
            CursorScript = GetComponent<CursorScript>();
        }
    }
}
