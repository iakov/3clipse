using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _3ClipseGame.Steam.Scenes.GameScenes.StartScreen.Imports.Dynamic_Elements
{
    public class Bush2: MonoBehaviour
    {
        [SerializeField] private bool isActive = true;
        [SerializeField] private float iterationTime = 0.3f;
        [SerializeField] private Vector3 windVector;
        [SerializeField] private float windForce = 1f;
        
        private List<LeafMovement2> _leafScripts;
        private Transform _leafTransform;

        private void Awake() => _leafScripts = GetComponentsInChildren<LeafMovement2>().ToList();
        
        private IEnumerator Start()
        {
            while (isActive)
            {
                foreach (var leafScript in _leafScripts)
                {
                    var rotationDegrees = GetRandomVector3();
                    leafScript.RotateByWind(iterationTime, rotationDegrees);
                }

                yield return new WaitForSeconds(iterationTime * 2);
            }
        }
        
        private Vector3 GetRandomVector3()
        {
            var multiplierVector = windVector.normalized;
            var randomX = multiplierVector.x * Random.Range(-1f, 1f);
            var randomY = multiplierVector.y * Random.Range(-1f, 1f);
            var randomZ = multiplierVector.z * Random.Range(-1f, 1f);

            return new Vector3(randomX, randomY, randomZ) * windForce;
        }
    }
}