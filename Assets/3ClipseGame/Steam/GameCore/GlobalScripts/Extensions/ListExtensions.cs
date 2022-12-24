using System.Collections.Generic;

namespace _3ClipseGame.Steam.GameCore.GlobalScripts.Extensions
{
    public static class ListExtensions
    {
        public static void MoveToAnotherCollection<T>(this List<T> list, List<T> newList, T element)
        {
            list.Remove(element);
            newList.Add(element);
        }
    }
}
