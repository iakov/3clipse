using UnityEngine;
using CharacterController = _3ClipseGame.Steam.Entities.CustomController.CharacterController;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal
{
    [RequireComponent(typeof(CharacterController))]
    public class MainAnimal : MonoBehaviour
    {
        #region SerializeFields
        

        #endregion

        #region PublicGetters

        public CharacterController AnimalController { get; private set; }

        #endregion

        private void Awake()
        {
            AnimalController = GetComponent<CharacterController>();
            
        }
    }
}
