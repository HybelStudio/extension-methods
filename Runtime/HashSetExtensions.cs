using System.Collections.Generic;

namespace Hybel.ExtensionMethods
{
    /// <summary>
    /// Extension Methods used on HashSets.
    /// </summary>
    public static class HashSetExtensions
    {
        /// <summary>
        /// Index the hashset.
        /// </summary>
        public static T Get<T>(this HashSet<T> set, int index)
        {
            int i = 0;
            foreach (var item in set)
            {
                if (i == index)
                    return item;

                i++;
            }

            return default;
        }

        public static void AddRange<T>(this HashSet<T> set, IEnumerable<T> collection)
        {
            foreach (T item in collection)
                set.Add(item);
        }
    }
}