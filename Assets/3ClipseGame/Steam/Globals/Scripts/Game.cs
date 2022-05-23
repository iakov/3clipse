using System;
using System.Collections;
using _3ClipseGame.Steam.Globals.UI.Scripts;
using _3ClipseGame.Steam.Scripts;
using _3ClipseGame.Steam.UI.Scripts.TabSystem;
using Cinemachine;
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
        
        public UIManager UIManager;

        private void Awake()
        {
            Instance = this;

            CameraManager = GetComponent<CameraManager>();
            InputManager = GetComponent<InputManager>();
            CursorScript = GetComponent<CursorScript>();
        }

        public void SwitchGameToPlaymode()
        {
            Time.timeScale = 1f;
            
            InputManager.MoveInputHandler.Activate();
            CursorScript.SwitchCursorMode(CursorLockMode.Locked);
            CameraManager.SwitchCamera(CameraManager.CameraType.MainCharacter);
            UIManager.SwitchUIToHUD();

            StartCoroutine(EnableCameraControlsOnEnd(1));
        }

        public void SwitchGameToMenu(TabButton tabButton)
        {
            Time.timeScale = 0.5f;
            
            InputManager.MoveInputHandler.Deactivate();
            CursorScript.SwitchCursorMode(CursorLockMode.Confined);
            CameraManager.SwitchCamera(CameraManager.CameraType.MainMenu);
            InputManager.CameraControllsHandler.Disable();

            UIManager.LastTabButton = tabButton;
            StartCoroutine(OpenUIOnEnd(CameraManager.GetTransitionTime()));
        }

        private IEnumerator EnableCameraControlsOnEnd(float time)
        {
            yield return new WaitForSeconds(time);
            InputManager.CameraControllsHandler.Enable();
        }
        
        private IEnumerator OpenUIOnEnd(float time)
        {
            yield return new WaitForSeconds(time);
            UIManager.SwitchUIToMenu();
        }
    }
}
