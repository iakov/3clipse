using System;

namespace _3ClipseGame.Steam.Core.GameSource
{
    public interface ISoloManager<T> where T : Enum
    {
        void Enable(T enableObjectType);
        T[] GetActive();
    }
}
