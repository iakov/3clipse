using System;

namespace _3ClipseGame.Steam.GameCore.Origin.Interfaces
{
    public interface ISoloManager<T> where T : Enum
    {
        void Enable(T enableObjectType);
        T[] GetActive();
    }
}
