using _3ClipseGame.Steam.Global.GameScripts.GameStates;
using _3ClipseGame.Steam.Global.Scripts.GameScripts.GameStates;
using _3ClipseGame.Steam.Global.StateDrivenCamera;
using UnityEngine;
using PlayMode = _3ClipseGame.Steam.Global.GameScripts.GameStates.PlayMode;

namespace _3ClipseGame.Steam.Global.Scripts.GameScripts
{
    public class Game : MonoBehaviour
    {
        #region Singleton

        public static Game Instance { get; private set; }

        #endregion

        #region Initialization

        public MenuMode MenuMode { get; private set; }
        public PlayMode PlayMode { get; private set; }
        public CinematicMode CinematicMode { get; set; }
        
        public CameraAnimatorController StateDrivenCamera;

        #endregion

        #region MonoBehaviourMethods

        private void Awake() => Instance = this;

        private void Start()
        {
            MenuMode = GetComponentInChildren<MenuMode>();
            PlayMode = GetComponentInChildren<PlayMode>();
            CinematicMode = GetComponentInChildren<CinematicMode>();
        }

        #endregion
    }
}
