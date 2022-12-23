using System;

namespace _3ClipseGame.Steam.GameCore.Origin.Interfaces
{
    public interface IMultiManager<T> : ISoloManager<T> where T : Enum
    {
        void Disable(T disableObjectType);
        void DisableAll();
    }
}
