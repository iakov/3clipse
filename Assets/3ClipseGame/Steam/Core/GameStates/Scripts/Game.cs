using _3ClipseGame.Steam.Core.Input.HUDInput;
using _3ClipseGame.Steam.Core.Scripts.GameScripts.GameStates;
using _3ClipseGame.Steam.Global.Scripts.GameScripts.GameStates;
using _3ClipseGame.Steam.Global.StateDrivenCamera;
using UnityEngine;
using PlayMode = _3ClipseGame.Steam.Global.Scripts.GameScripts.GameStates.PlayMode;

namespace _3ClipseGame.Steam.Core.GameStates.Scripts
{
    public class Game : MonoBehaviour
    {
        #region Singleton

        public static Game Instance;

        private void Awake()
        {
            Instance = this;
        }

        #endregion

        #region Public

        public MenuMode MenuMode { get; private set; }
        public PlayMode PlayMode { get; private set; }
        public CinematicMode CinematicMode { get; set; }
        
        public HUDInputHandler HUDInputHandler;
        public CameraAnimatorController StateDrivenCamera;

        #endregion
        
        #region MonoBehaviourMethods

        private void Start()
        {
            MenuMode = GetComponentInChildren<MenuMode>();
            PlayMode = GetComponentInChildren<PlayMode>();
            CinematicMode = GetComponentInChildren<CinematicMode>();
        }

        #endregion
    }
}
