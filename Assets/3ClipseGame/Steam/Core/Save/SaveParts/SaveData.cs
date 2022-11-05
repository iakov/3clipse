using System;
using UnityEngine;

namespace _3ClipseGame.Steam.Core.Save.SaveParts
{
    [Serializable]
    public abstract class SaveData<T> where T : Component
    {
        protected SaveData(T saveObject)
        {
            SerializeObject = saveObject;
        }

        [NonSerialized] protected readonly T SerializeObject;

        public abstract void Load(T newObject);
        public abstract void Save();
    }
}
