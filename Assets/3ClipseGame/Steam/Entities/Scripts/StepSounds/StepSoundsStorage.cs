using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Scripts.StepSounds
{
    [CreateAssetMenu(fileName = "New Step Sounds Storage", menuName = "Entities/Sounds/Step Sounds Storage")]
    public class StepSoundsStorage : ScriptableObject
    {
        [SerializeField] private StepSound[] materialsStepSounds;
        [SerializeField] private List<AudioClip> defaultStepSound;

        public List<AudioClip> TryGetStepSounds(Material material)
        {
            foreach (var materialStepSound in materialsStepSounds)
            {
                if (materialStepSound.materials.All(currentMaterial => currentMaterial.mainTexture != material.mainTexture)) continue;
                return materialStepSound.stepClips.ToList();
            }

            return defaultStepSound;
        }
    }
    
    [System.Serializable]
    public struct StepSound
    {
        public Material[] materials;
        public AudioClip[] stepClips;
    }
}
