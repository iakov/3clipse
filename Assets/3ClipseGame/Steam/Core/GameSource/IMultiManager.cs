using System;

namespace _3ClipseGame.Steam.Core.GameSource
{
    public interface IMultiManager<T> : ISoloManager<T> where T : Enum
    {
        void Disable(T disableObjectType);
        void DisableAll();
    }
}
