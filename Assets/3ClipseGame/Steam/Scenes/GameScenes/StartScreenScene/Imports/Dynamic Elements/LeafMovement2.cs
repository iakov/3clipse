using System.Collections;
using UnityEngine;

namespace _3ClipseGame.Steam.Scenes.GameScenes.StartScreenScene.Imports.Dynamic_Elements
{
    public class LeafMovement2 : MonoBehaviour
    {
        private Transform _transform;
        private void Awake() => _transform = GetComponent<Transform>();

        public void RotateByWind(float time, Vector3 windVector) => StartCoroutine(RotateRoutine(time, windVector));

        private IEnumerator RotateRoutine(float time, Vector3 windVector)
        {
            _transform.LeanRotate(windVector, time)
                .setEaseOutSine();
             yield return new WaitForSeconds(time);
            
            _transform.LeanRotate(Vector3.zero, time)
                .setEaseInSine();
            yield return new WaitForSeconds(time);
        }
    }
}