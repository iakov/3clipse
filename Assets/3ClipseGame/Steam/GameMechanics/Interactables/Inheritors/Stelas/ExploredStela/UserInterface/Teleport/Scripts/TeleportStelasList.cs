using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Stelas.Abstracts;
using _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Stelas.KeyLogic.UserInterface;
using UnityEngine;
using UnityEngine.UI;

namespace _3ClipseGame.Steam.GameMechanics.Interactables.Inheritors.Stelas.ExploredStela.UserInterface.Teleport.Scripts
{
    [RequireComponent(typeof(LayoutGroup))]
    public class TeleportStelasList : MonoBehaviour
    {
        [SerializeField] private StelasGroup _stelasGroup;
        [SerializeField] private TeleportIcon _teleportIconPrefab;

        private void Start()
        {
            _stelasGroup.UpdateListsOfStelas();
            var exploredStelas = _stelasGroup.GetExploredStelas();
            
            foreach (var stela in exploredStelas)
            {
                var data = stela.GetTeleportData();
                var icon = Instantiate(_teleportIconPrefab.gameObject, transform);
                data.ApplyDataToIcon(icon.GetComponent<TeleportIcon>());
            }
        }
    }
}
