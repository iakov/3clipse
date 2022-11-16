using System;
using System.Threading;
using _3ClipseGame.Steam.Core.GameSource.Parts.Player;
using UnityEditor;
using UnityEngine;

namespace _3ClipseGame.Steam.Mechanics.Save.InGame.Scripts
{
    public class SerializationDependencies : MonoBehaviour
    {
        [SerializeField] private Player _player;

        public Player Player => _player;

        private EditorApplication.CallbackFunction _callback;

        public void FindDependencies(EditorApplication.CallbackFunction callback)
        {
            _callback = callback;
            var dependenciesThread = new Thread(DependenciesRoutine);
            dependenciesThread.Start();
        }

        private void DependenciesRoutine()
        {
            _player = FindObjectOfType<Player>();
            _callback.Invoke();
        }
    }
}