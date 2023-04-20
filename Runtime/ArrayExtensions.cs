using System;
using System.Linq;

namespace Hybel.ExtensionMethods
{
    /// <summary>
    /// Extension Methods used on Arrays.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Creates a new array which is a set of the original array.
        /// </summary>
        public static T[] SubSet<T>(this T[] array, int startInclusive)
        {
            int endExclusive = array.Length;
            T[] subSet = new T[endExclusive - startInclusive];

            for (int i = 0; i < endExclusive - startInclusive; i++)
                subSet[i] = array[i + startInclusive];

            return subSet;
        }

        /// <summary>
        /// Creates a new array which is a set of the original array.
        /// </summary>
        public static T[] SubSet<T>(this T[] array, int startInclusive, int endExclusive)
        {
            T[] subSet = new T[endExclusive - startInclusive];

            for (int i = 0; i < endExclusive - startInclusive; i++)
                subSet[i] = array[i + startInclusive];

            return subSet;
        }

        /// <summary>
        /// Create a copy of the array.
        /// </summary>
        public static T[] Copy<T>(this T[] array) => (T[])array.Clone();

        /// <summary>
        /// Find the index of an <paramref name="item"/> in the <paramref name="array"/>.
        /// </summary>
        public static int IndexOf<T>(this T[] array, T item) => Array.IndexOf(array, item);

        /// <summary>
        /// Finds the index of <paramref name="item"/> in <paramref name="array"/> regardless of the arrays dimension (rank).
        /// </summary>
        /// <param name="item">Item in the <paramref name="array"/>.</param>
        public static int[] FindIndex(this Array array, object item)
        {
            if (array.Rank == 1)
                return new[] { Array.IndexOf(array, item) };

            var found = array.OfType<object>()
                              .Select((v, i) => new { v, i })
                              .FirstOrDefault(s => s.v.Equals(item));

            var indexes = new int[array.Rank];

            if (found == null)
            {
                for (int i = 0; i < indexes.Length; i++)
                    indexes[i] = -1;

                return indexes;
            }

            var last = found.i;
            var lastLength = Enumerable.Range(0, array.Rank)
                                       .Aggregate(1,
                                           (a, v) => a * array.GetLength(v));

            for (var rank = 0; rank < array.Rank; rank++)
            {
                lastLength /= array.GetLength(rank);
                var value = last / lastLength;
                last -= value * lastLength;

                var index = value + array.GetLowerBound(rank);

                if (index > array.GetUpperBound(rank))
                    throw new IndexOutOfRangeException();

                indexes[rank] = index;
            }

            return indexes;
        }

        /// <summary>
        /// Finds the index of <paramref name="item"/> in <paramref name="array"/> regardless of the arrays dimension (rank).
        /// </summary>
        /// <param name="item">Item in the <paramref name="array"/>.</param>
        /// <param name="rank">
        /// The rank of <paramref name="array"/>.
        /// <para>You can use this to loop over the returned array.</para>
        /// </param>
        public static int[] FindIndex(this Array array, object item, out int rank)
        {
            var indicies = array.FindIndex(item);
            rank = indicies.Length;
            return indicies;
        }
    }
}