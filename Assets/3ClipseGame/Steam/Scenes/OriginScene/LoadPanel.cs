using UnityEngine;
using UnityEngine.SceneManagement;

namespace _3ClipseGame.Steam.Scenes.OriginScene
{
    public class LoadPanel : MonoBehaviour
    {
        public void LoadScene()
        {
            gameObject.SetActive(true);
        }

        private void FinishLoad(Scene scene, LoadSceneMode loadMode)
        {
            
        }
    }
}
