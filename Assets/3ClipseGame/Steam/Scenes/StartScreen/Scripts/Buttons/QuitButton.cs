using UnityEngine;

namespace _3ClipseGame.Steam.Scenes.StartScreen.Scripts.Buttons
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