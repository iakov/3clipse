using System.Collections.Generic;

namespace _3ClipseGame.Steam.GameCore.GlobalScripts.Extensions
{
    public static class LinkedListExtensions
    {
        public static LinkedListNode<T> GetNextListElement<T>(this LinkedList<T> list, T currentValue)
        {
            var currentElement = list.GetElementByValue(currentValue);
            return currentElement?.Next;
        }
        
        public static LinkedListNode<T> GetPreviousListElement<T>(this LinkedList<T> list, T currentValue)
        {
            var currentElement = list.GetElementByValue(currentValue);
            return currentElement?.Previous;
        }

        public static LinkedListNode<T> GetElementByValue<T>(this LinkedList<T> list, T currentValue)
        {
            return list.Find(currentValue);
        }
    }
}
