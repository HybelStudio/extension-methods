using System;
using System.Collections.Generic;
using System.Linq;

using Random = UnityEngine.Random;

namespace Hybel.ExtensionMethods
{
    /// <summary>
    /// Extension Methods used on Lists.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Remove an item from the list and return the item.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="list">List.</param>
        /// <param name="index">Index of the item that should be removed.</param>
        /// <returns>Removed item.</returns>
        public static T RemoveAndReturn<T>(this IList<T> list, int index)
        {
            T item = list[index];
            list.RemoveAt(index);
            return item;
        }

        /// <summary>
        /// Shuffle the contents of a list.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="list">List.</param>
        public static void Shuffle<T>(this IList<T> list)
        {
            System.Random rng = new System.Random();
            int number = list.Count;
            while (number > 1)
            {
                number--;
                int k = rng.Next(number + 1);
                T value = list[k];
                list[k] = list[number];
                list[number] = value;
            }
        }

        /// <summary>
        /// Get a random item from a list.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="list">List.</param>
        /// <returns>Random item from a list</returns>
        public static T RandomItem<T>(this IList<T> list)
        {
            if (list.Count == 0)
                throw new IndexOutOfRangeException("Cannot select a random item from an empty list");
            return list[Random.Range(0, list.Count)];
        }

        /// <summary>
        /// Remove random item from a list.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="list">List.</param>
        /// <returns>Removed item.</returns>
        public static T RemoveRandom<T>(this IList<T> list)
        {
            if (list.Count == 0)
                throw new IndexOutOfRangeException("Cannot remove a random item from an empty list");

            int index = Random.Range(0, list.Count);
            return list.RemoveAndReturn(index);
        }

        /// <summary>
        /// Checks if a statement is true for at least one item in the list.
        /// </summary>
        public static bool TrueForOne<T>(this IList<T> list, Func<T, bool> predicate)
        {
            foreach (T item in list)
                if (predicate.Invoke(item))
                    return true;

            return false;
        }

        /// <summary>
        /// Remove duplicate entries in the list.
        /// </summary>
        public static IList<T> RemoveDoubles<T>(this IList<T> list) => list.Distinct().ToList();

        /// <summary>
        /// Foreach <typeparamref name="T1"/> in the list find the first item in a list the <typeparamref name="T1"/> contains.
        /// </summary>
        /// <typeparam name="T1">Type of item in the parent list.</typeparam>
        /// <typeparam name="T2">Type of item in the sub-list.</typeparam>
        /// <param name="listOfItemsWithList">A list of items where each item contains another list.</param>
        /// <param name="listGetter">How to access the sub-list from the parent list.</param>
        /// <param name="match">Predicate to check for when searching.</param>
        /// <returns>The first <typeparamref name="T2"/> found.</returns>
        public static T2 ForEachFind<T1, T2>(this IList<T1> listOfItemsWithList, Func<T1, IList<T2>> listGetter, Predicate<T2> match)
        {
            foreach (T1 itemWithList in listOfItemsWithList)
                foreach (var item in listGetter(itemWithList))
                    if (match(item))
                        return item;

            return default;
        }

        /// <summary>
        /// Find all <typeparamref name="T2"/> in each sub-list of <paramref name="listOfItemsWithList"/>.
        /// </summary>
        /// <typeparam name="T1">Type of item in the parent list.</typeparam>
        /// <typeparam name="T2">Type of item in the sub-list.</typeparam>
        /// <param name="listOfItemsWithList">A list of items where each item contains another list.</param>
        /// <param name="listGetter">How to access the sub-list from the parent list.</param>
        /// <param name="match">Predicate to check for when searching.</param>
        /// <returns>All <typeparamref name="T2"/>s found.</returns>
        public static List<T2> ForEachFindAll<T1, T2>(this IList<T1> listOfItemsWithList, Func<T1, IList<T2>> listGetter, Predicate<T2> match)
        {
            List<T2> foundItems = new List<T2>();

            foreach (T1 itemWithList in listOfItemsWithList)
                foreach (var item in listGetter(itemWithList))
                    if (match(item))
                        foundItems.Add(item);

            return foundItems;
        }

        /// <summary>
        /// Checks if the list is null or has a count of zero.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IList<T> list) =>
            list == null || list.Count == 0;

        /// <summary>
        /// Create a new list containing the same objects as <paramref name="list"/>. Not a deep copy so all child objects are copied as references unless they're value types.
        /// </summary>
        public static List<T> ToNewList<T>(this IList<T> list)
        {
            var clone = new List<T>();
            clone.AddRange(list);
            return clone;
        }

        public static List<T> Populate<T>(this List<T> list)
        {
            int capacity = list.Capacity;

            if (list.Count != 0 || capacity <= 0)
                throw new ArgumentException("List must be empty and have a capacity greater than zero");

            for (int i = 0; i < capacity; i++)
                list.Add(default);

            return list;
        }
    }
}