using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Scripts
{
    [CreateAssetMenu(fileName = "New Step Sounds Storage", menuName = "Entities/Sounds/Step Sounds Storage")]
    public class StepSoundsStorage : ScriptableObject
    {
        #region SerializeFields

        [SerializeField] private StepSound[] materialsStepSounds;
        [SerializeField] private List<AudioClip> defaultStepSound;

        #endregion

        #region PublicMethods

        public List<AudioClip> TryGetStepSounds(Material material)
        {
            foreach (var materialStepSound in materialsStepSounds)
            {
                if (materialStepSound.materials.All(currentMaterial => currentMaterial.mainTexture != material.mainTexture)) continue;
                return materialStepSound.stepClips.ToList();
            }

            return defaultStepSound;
        }

        #endregion
    }

    #region Structs

    [System.Serializable]
    public struct StepSound
    {
        public Material[] materials;
        public AudioClip[] stepClips;
    }

    #endregion
        
}
