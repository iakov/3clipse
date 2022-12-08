using UnityEngine;

namespace _3ClipseGame.Steam.Scenes.GameScenes.StartScreen.Scripts.Buttons
{
    public class QuitButton : MonoBehaviour
    {
        public void Quit()
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}