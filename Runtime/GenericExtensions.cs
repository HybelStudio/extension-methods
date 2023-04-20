using System;
using System.Collections.Generic;

namespace Hybel.ExtensionMethods
{
    /// <summary>
    /// Extension Methods used on anything.
    /// </summary>
    public static class GenericExtensions
    {
        /// <summary>
        /// Dumps the <paramref name="objectToDump"/> and returns void.
        /// <para>Will dispose if <paramref name="objectToDump"/> is <see cref="IDisposable"/></para>
        /// </summary>
        public static void Dump<T>(this T objectToDump)
        {
            if (objectToDump is IDisposable disposable)
                disposable.Dispose();
        }

        /// <summary>
        /// If the result is null, this returns false. Otherwise its true and the <paramref name="value"/> is the item you want to use.
        /// </summary>
        public static bool TryGet<T>(this T item, out T value)
        {
            value = item;
            return value != null;
        }

        /// <summary>
        /// Returns the first not null item starting with <paramref name="item"/> and then going over the <paramref name="otherItems"/> after.
        /// </summary>
        public static T Coalesce<T>(this T item, params T[] otherItems)
            where T : class
        {
            if (item != null)
                return item;

            foreach (var otherItem in otherItems)
            {
                if (otherItem == null)
                    continue;

                return otherItem;
            }

            return null;
        }

        /// <summary>
        /// Make any <paramref name="item"/> iterable.
        /// </summary>
        /// <returns>An enumerable yielding the <paramref name="item"/>.</returns>
        public static IEnumerable<T> Iterable<T>(this T item)
        {
            yield return item;
        }
    }
}