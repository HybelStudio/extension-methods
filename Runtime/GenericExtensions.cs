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
        public static IEnumerable<T> Yield<T>(this T item)
        {
            yield return item;
        }

        /// <summary>
        /// Conditionally chain do something with the <paramref name="item"/> if
        /// the <paramref name="condition"/> is true.
        /// </summary>
        /// <param name="item">The item to conditionally chain.</param>
        /// <param name="condition">The condition to check.</param>
        /// <param name="action">The action to perform on the <paramref name="item"/>.</param>
        /// <returns>The <paramref name="item"/>.</returns>
        public static T Conditionally<T>(this T item, bool condition, Action<T> action)
        {
            if (condition)
                action(item);

            return item;
        }

        /// <summary>
        /// Conditionally chain do something with the <paramref name="item"/> if
        /// the <paramref name="condition"/> is true.
        /// </summary>
        /// <param name="item">The item to conditionally chain.</param>
        /// <param name="condition">The condition to check.</param>
        /// <param name="func">The function to perform on the <paramref name="item"/>.</param>
        /// <returns>The <paramref name="item"/> or the result of <paramref name="func"/>.</returns>
        public static T Conditionally<T>(this T item, bool condition, Func<T, T> func)
        {
            if (condition)
                return func(item);

            return item;
        }

        /// <summary>
        /// Conditionally chain do something with the <paramref name="item"/> if
        /// the <paramref name="predicate"/> is true.
        /// </summary>
        /// <param name="item">The item to conditionally chain.</param>
        /// <param name="predicate">The predicate to check.</param>
        /// <param name="action">The action to perform on the <paramref name="item"/>.</param>
        /// <returns>The <paramref name="item"/>.</returns>
        public static T Conditionally<T>(this T item, Predicate<T, bool> predicate, Func<T, T> result)
        {
            if (predicate(item))
                return result(item);

            return item;
        }
    }
}
