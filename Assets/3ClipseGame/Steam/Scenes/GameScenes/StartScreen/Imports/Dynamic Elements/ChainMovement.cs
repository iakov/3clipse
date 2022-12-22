using UnityEngine;

namespace _3ClipseGame.Steam.Scenes.GameScenes.StartScreen.Imports.Dynamic_Elements
{
    public class ChainMovement : MonoBehaviour
    {
        [SerializeField] private float _extremelyLowSpeed = 0.01f;
        [SerializeField] private int _windForce = 5000;
        [SerializeField] private float _chainLinkSpeed = 10;
        private Rigidbody _chainLinkRigidbody;
    
        public void Awake() 
        {
            _chainLinkRigidbody = GetComponent<Rigidbody>();
            Pitching();
        }

        private void Update()
        {
            if (_chainLinkRigidbody.velocity.magnitude < _extremelyLowSpeed) Pitching();
        }

        private void Pitching()
        {
            var moveX = new Vector3(_windForce, 0, _windForce) * (_chainLinkSpeed * Time.deltaTime);
            _chainLinkRigidbody.AddForce(moveX);
        }
    }
}