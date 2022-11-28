using UnityEngine;

namespace _3ClipseGame.Steam.Scenes.TestScene.StartScreen.Scripts.Buttons
{
    public class ExitButton : MonoBehaviour
    {
        public void Quit()
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}