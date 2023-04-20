using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hybel.ExtensionMethods
{
    /// <summary>
    /// Extension Methods used on IEnumerables.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// This wraps the <see cref="Enumerable.Where{TSource}(IEnumerable{TSource}, Func{TSource, bool})"/> function and only exists to be more readable.
        /// </summary>
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> collection, Func<T, bool> filter) =>
            collection.Where(filter);

        /// <summary>
        /// This wraps the <see cref="Enumerable.Where{TSource}(IEnumerable{TSource}, Func{TSource, int, bool})"/> function and only exists to be more readable.
        /// </summary>
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> collection, Func<T, int, bool> filter) =>
            collection.Where(filter);

        /// <summary>
        /// This wraps the <see cref="Enumerable.Select{TSource, TResult}(IEnumerable{TSource}, Func{TSource, TResult})"/> function and only exists to be more readable.
        /// </summary>
        public static IEnumerable<TResult> Map<T, TResult>(this IEnumerable<T> collection, Func<T, TResult> mapper) =>
            collection.Select(mapper);

        /// <summary>
        /// This wraps the <see cref="Enumerable.Select{TSource, TResult}(IEnumerable{TSource}, Func{TSource, int, TResult})"/> function and only exists to be more readable.
        /// </summary>
        public static IEnumerable<TResult> Map<T, TResult>(this IEnumerable<T> collection, Func<T, int, TResult> mapper) =>
            collection.Select(mapper);

        /// <summary>
        /// Run an action for each item in the <paramref name="collection"/>.
        /// </summary>
        /// <param name="action">Action to run on every item in the <paramref name="collection"/>.</param>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (T item in collection)
                action.Invoke(item);
        }

        /// <summary>
        /// Checks if the <paramref name="collection"/> has no values.
        /// </summary>
        /// <returns>True if the <paramref name="collection"/> has a count of zero.</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> collection) =>
            collection.Count() <= 0;

        /// <summary>
        /// Merge all collections into one collection.
        /// </summary>
        /// <returns>The merged collection.</returns>
        public static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>> collectionOfCollections)
        {
            List<T> list = new();

            foreach (IEnumerable<T> collection in collectionOfCollections)
                list.AddRange(collection);

            return list;
        }

        /// <summary>
        /// Fully enumerates the <paramref name="collection"/>.
        /// </summary>
        /// <returns>A List in the form of an IEnumerable.</returns>
        public static IEnumerable<T> Collect<T>(this IEnumerable<T> collection) => collection.ToList();

        /// <summary>
        /// Finds the first not null item in the <paramref name="collection"/> and returns it.
        /// </summary>
        public static T Coalesce<T>(this IEnumerable<T> collection)
            where T : class
        {
            foreach (T item in collection)
            {
                if (item == null)
                    continue;

                return item;
            }

            return null;
        }

        /// <summary>
        /// Chain two enumerables of the same type together as if they were only one enumerable.
        /// </summary>
        public static IEnumerable<T> Chain<T>(this IEnumerable<T> collection, IEnumerable<T> other)
        {
            foreach (T item in collection)
                yield return item;

            foreach (T item in other)
                yield return item; 
        }

        /// <summary>
        /// Chain multiple enumerables of the same type together as if they were only one enumerable.
        /// </summary>
        public static IEnumerable<T> Chain<T>(this IEnumerable<T> collection, params IEnumerable<T>[] others)
        {
            foreach (T item in collection)
                yield return item;

            foreach (IEnumerable<T> other in others)
                foreach (T item in other)
                    yield return item;
        }

        /// <summary>
        /// Look through the <paramref name="collection"/> in a window scrolling across the <paramref name="collection"/>.
        /// </summary>
        /// <param name="windowSize">How many items you wish to see at once.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">This exception is thrown if the <paramref name="windowSize"/> is zero or less.</exception>
        /// <exception cref="ArgumentException">This exceptino is thrown if the <paramref name="collection"/> is smaller than the <paramref name="windowSize"/>.</exception>
        public static IEnumerable<IEnumerable<T>> Windows<T>(this IEnumerable<T> collection, int windowSize)
        {
            if (windowSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(windowSize), "Window size must be greater than zero.");

            var window = new T[windowSize];
            IEnumerator<T> enumerator = collection.GetEnumerator();

            for (int i = 0; i < windowSize; i++)
            {
                if (enumerator.MoveNext())
                    window[i] = enumerator.Current;
                else
                    throw new ArgumentException("Collection was smaller than the windowSize", nameof(collection));
            }

            yield return window;

            while (enumerator.MoveNext())
            {
                for (int i = 1; i < windowSize; i++)
                    window[i - 1] = window[i];

                window[^1] = enumerator.Current;
                yield return window;
            }
        }

        /// <summary>
        /// Splits the <paramref name="collection"/> into sections with size <paramref name="sectionSize"/>.
        /// </summary>
        /// <param name="sectionSize">
        /// How large the sections are.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IEnumerable<IEnumerable<T>> Sections<T>(this IEnumerable<T> collection, int sectionSize)
        {
            if (sectionSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(sectionSize), "Section size must be greater than zero.");

            List<T> section = new List<T>(sectionSize).Populate();
            IEnumerator<T> enumerator = collection.GetEnumerator();

            for (int i = 0; true; i += sectionSize) // Scary infinite loop!
            {
                for (int j = 0; j < sectionSize; j++)
                {
                    if (enumerator.MoveNext())
                        section[j] = enumerator.Current;
                    else
                    {
                        for (int k = sectionSize - 1; k >= j; k--)
                            section.RemoveAt(k);

                        if (section.Count > 0)
                            yield return section;

                        goto end;
                    }
                }

                yield return section;
            }

        end:;
        }

        /// <summary>
        /// Aggregates over a <paramref name="collection"/> to find one desired result.
        /// For each item in the <paramref name="collection"/> an object is selected using the <paramref name="selector"/>.
        /// Then the selected item is passed to the <paramref name="aggregatorFunc"/>.
        /// After which a <see cref="object.Equals"/> is called to check if the <paramref name="aggregatorFunc"/> returned a new value.
        /// </summary>
        /// <typeparam name="T">The type which is aggregated.</typeparam>
        /// <typeparam name="TResult">The type on which to do checks.</typeparam>
        /// <param name="selector">Function which defines what object to aggregate of relative to each item in the <paramref name="collection"/>.</param>
        /// <param name="aggregatorFunc">First argument is a currently selected value to be returned by the aggregate function. Second argument is the next value to be considered. The return value will be the first argument in the next iteration.</param>
        /// <returns>The last item returned by the <paramref name="aggregatorFunc"/> on the last iteration.</returns>
        /// <exception cref="ArgumentNullException">Thrown if any of the arguments are null.</exception>
        public static T Aggregate<T, TResult>(this IEnumerable<T> collection, Func<T, TResult> selector, Func<TResult, TResult, TResult> aggregatorFunc)
        {
            if (collection is null)
                throw new ArgumentNullException(nameof(collection));

            if (selector is null)
                throw new ArgumentNullException(nameof(selector));

            if (aggregatorFunc is null)
                throw new ArgumentNullException(nameof(aggregatorFunc));

            T aggregateItem = default;
            TResult aggregateResult = default;

            foreach (var item in collection)
            {
                TResult current = selector(item);
                TResult oldResult = aggregateResult;
                aggregateResult = aggregatorFunc(aggregateResult, current);

                if (!Equals(aggregateResult, oldResult))
                    aggregateItem = item;
            }

            return aggregateItem;
        }

        /// <summary>
        /// Get a subset of the <paramref name="collection"/>.
        /// </summary>
        /// <param name="start">Where in the collection to start.</param>
        /// <param name="count">How many items to add to the subset.</param>
        public static IEnumerable<T> SubSet<T>(this IEnumerable<T> collection, int start, int? count = null)
        {
            int i = -1;
            foreach (var item in collection)
            {
                i++;

                if (i < start)
                    continue;

                if (count.HasValue && i >= count + start)
                    yield break;

                yield return item;
            }
        }

        /// <summary>
        /// Adds a single <paramref name="item"/> to the end of the <paramref name="collection"/>.
        /// </summary>
        public static IEnumerable<T> PushToEnd<T>(this IEnumerable<T> collection, T item)
        {
            foreach (T i in collection)
                yield return i;

            yield return item;
        }

        /// <summary>
        /// Adds a single <paramref name="item"/>  to the start of the <paramref name="collection"/>.
        /// </summary>
        public static IEnumerable<T> PushToStart<T>(this IEnumerable<T> collection, T item)
        {
            yield return item;

            foreach (T i in collection)
                yield return i;
        }

        /// <summary>
        /// Insert a single <paramref name="item"/> into the <paramref name="collection"/> at the specified <paramref name="index"/>.
        /// <para>This does not enumerate the <paramref name="collection"/>.</para>
        /// </summary>
        public static IEnumerable<T> Insert<T>(this IEnumerable<T> collection, T item, int index)
        {
            int i = 0;
            foreach (T c in collection)
            {
                if (i == index)
                    yield return item;

                yield return c;
                i++;
            }
        }

        /// <summary>
        /// Skip certain items in the <paramref name="collection"/> based on its index.
        /// <para>This does not enumerate the <paramref name="collection"/>.</para>
        /// </summary>
        /// <param name="predicate">Function choosing if certain indicies should be skipped or not. Returning true will skip that item, false will yield the item.</param>
        public static IEnumerable<T> Skip<T>(this IEnumerable<T> collection, Func<int, bool> predicate)
        {
            int i = 0;

            foreach (T item in collection)
            {
                if (!predicate(i))
                    yield return item;

                i++;
            }
        }

        /// <summary>
        /// Modify each item in the <paramref name="collection"/>.
        /// </summary>
        public static IEnumerable<T> Modify<T>(this IEnumerable<T> collection, Func<T, T> modification)
        {
            foreach (T item in collection)
                yield return modification(item);
        }

        /// <summary>
        /// Modify each item in the <paramref name="collection"/> using the items index.
        /// </summary>
        /// <param name="modification">First argument is the index of the item in the <paramref name="collection"/>. Second argument is the item itself. Yields the return value.</param>
        public static IEnumerable<T> ModifyByIndex<T>(this IEnumerable<T> collection, Func<int, T, T> modification)
        {
            int i = 0;

            foreach (T item in collection)
            {
                yield return modification(i, item);
                i++;
            }
        }

        /// <summary>
        /// Slice the <paramref name="collection"/> by selecting a <paramref name="start"/> and <paramref name="end"/> index as well as how to <paramref name="step"/> through the <paramref name="collection"/>.
        /// </summary>
        /// <param name="start">Start index of selection. Inclusive.</param>
        /// <param name="end">End index of selection. Exclusive.</param>
        /// <param name="step">
        /// The increment used when stepping through the selection. A negative number steps through the selection backwards. If this is <c>0</c> it throws and exception.
        /// <para>The function skips as many times as it should before yielding. For example: a <paramref name="step"/> of 2 will skip the first item in the <paramref name="collection"/> and yield the second one, and so on.</para>
        /// </param>
        /// <returns>Slice of the original <paramref name="collection"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IEnumerable<T> Slice<T>(this IEnumerable<T> collection, int start = 0, int? end = null, int step = 1)
        {
            if (collection is null)
                throw new ArgumentNullException(nameof(collection));

            if (start < 0)
                throw new ArgumentOutOfRangeException(nameof(start), "Start index cannot be less than 0.");

            if (step == 0)
                throw new ArgumentOutOfRangeException(nameof(step), "Step cannot be zero as it would result in an infinite loop.");

            if (end <= start)
                throw new ArgumentOutOfRangeException(nameof(end), "End index must be greater than start index.");

            if (start != 0 || end != null)
                collection = collection.SubSet(start, end.HasValue ? end - start : null);

            if (step.IsNegative())
            {
                collection = collection.Reverse();
                step *= -1;
            }

            IEnumerator<T> enumerator = collection.GetEnumerator();
            bool nextExists = true; 

            while (nextExists)
            {
                for (int i = 0; i < step; i++)
                    if (!(nextExists = enumerator.MoveNext()))
                        yield break;

                yield return enumerator.Current;
            }
        }

        /// <summary>
        /// Converts any enumerable of characters to a string using a <see cref="StringBuilder"/>.
        /// </summary>
        public static string ToText(this IEnumerable<char> charCollection)
        {
            StringBuilder builder = new();

            foreach (char @char in charCollection)
                builder.Append(@char);

            return builder.ToString();
        }
    }

    /// <summary>
    /// Extension Methods used on IEnumerators.
    /// </summary>
    public static class EnumeratorExtensions
    {
        /// <summary>
        /// Get the next item from the <paramref name="iterator"/>. <paramref name="moveSuccess"/> will be false when the iterator is done.
        /// </summary>
        public static T Next<T>(this IEnumerator<T> iterator, out bool moveSuccess)
        {
            moveSuccess = iterator.MoveNext();
            return iterator.Current;
        }
    }
}