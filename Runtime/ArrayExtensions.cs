using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        /// <summary>
        /// Filters the <paramref name="array"/> using the <paramref name="filter"/> to determine what is kept.
        /// <para>Allocates a new array.</para>
        /// </summary>
        public static T[] Filter<T>(this T[] array, Func<T, bool> filter)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));

            if (filter is null)
                throw new ArgumentNullException(nameof(filter));

            List<T> filtered = new(array.Length / 2);

            for (int i = 0; i < array.Length; i++)
            {
                T item = array[i];

                if (filter(item))
                    filtered.Add(item);
            }

            return filtered.ToArray();
        }

        /// <summary>
        /// Filters the <paramref name="array"/> using the <paramref name="filter"/> to determine what is kept.
        /// <para>Allocates a new array.</para>
        /// </summary>
        public static T[] Filter<T>(this T[] array, Func<T, int, bool> filter)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));

            if (filter is null)
                throw new ArgumentNullException(nameof(filter));

            List<T> filtered = new(array.Length / 2);
            for (int i = 0; i < array.Length; i++)
            {
                T item = array[i];

                if (filter(item, i))
                    filtered.Add(item);
            }

            return filtered.ToArray();
        }

        /// <summary>
        /// Maps the <paramref name="array"/> using the <paramref name="mapper"/> to determine how to alter the data.
        /// <para>Allocates a new array.</para>
        /// </summary>
        public static TResult[] Map<T, TResult>(this T[] array, Func<T, TResult> mapper)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));

            if (mapper is null)
                throw new ArgumentNullException(nameof(mapper));

            var mapped = new TResult[array.Length];

            for (int i = 0; i < array.Length; i++)
                mapped[i] = mapper(array[i]);

            return mapped;
        }

        /// <summary>
        /// Maps the <paramref name="array"/> using the <paramref name="mapper"/> to determine how to alter the data.
        /// <para>Allocates a new array.</para>
        /// </summary>
        public static TResult[] Map<T, TResult>(this T[] array, Func<T, int, TResult> mapper)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));

            if (mapper is null)
                throw new ArgumentNullException(nameof(mapper));

            var mapped = new TResult[array.Length];

            for (int i = 0; i < array.Length; i++)
                mapped[i] = mapper(array[i], i);

            return mapped;
        }
        
        /// <summary>
        /// Maps the <paramref name="arrays"/> using the <paramref name="mapper"/> and combines all the elements into one array.
        /// <para>Uses a temporary list to hold the values.</para>
        /// </summary>
        /// <typeparam name="T">Type of element in input arrays.</typeparam>
        /// <typeparam name="TResult">Type of element in output array.</typeparam>
        /// <exception cref="ArgumentNullException">Throws if any arguments are null. Not thrown if inner arrays are null, they will be ignored.</exception>
        public static TResult[] MapMany<T, TResult>(this T[][] arrays, Func<T, TResult> mapper)
        {
            if (arrays is null)
                throw new ArgumentNullException(nameof(arrays));

            if (mapper is null)
                throw new ArgumentNullException(nameof(mapper));
            
            var results = new List<TResult>();

            foreach (T[] innerArray in arrays)
            {
                if (innerArray is null)
                    continue;
                
                for (int j = 0; j < innerArray.Length; j++)
                    results.AddRange(innerArray.Map(mapper));
            }

            return results.ToArray();
        }

        /// <summary>
        /// Maps the <paramref name="arrays"/> using the <paramref name="mapper"/> and combines all the elements into one array.
        /// <para>Uses a temporary list to hold the values.</para>
        /// </summary>
        /// <param name="mapper">First input is an element from the arrays, next two inputs are 'i' and 'j', the respective iteration in the loops.</param>
        /// <typeparam name="T">Type of element in input arrays.</typeparam>
        /// <typeparam name="TResult">Type of element in output array.</typeparam>
        /// <exception cref="ArgumentNullException">Throws if any arguments are null. Not thrown if inner arrays are null, they will be ignored.</exception>
        public static TResult[] MapMany<T, TResult>(this T[][] arrays, Func<T, int, int, TResult> mapper)
        {
            if (arrays is null)
                throw new ArgumentNullException(nameof(arrays));

            if (mapper is null)
                throw new ArgumentNullException(nameof(mapper));
            
            var results = new List<TResult>();

            for (int i = 0; i < arrays.Length; i++)
            {
                T[] innerArray = arrays[i];

                for (int j = 0; j < innerArray.Length; j++)
                    results.Add(mapper(innerArray[j], i, j));
            }

            return results.ToArray();
        }
        
        public static int[,] MatrixMultiply(this int[,] left, int[,] right) =>
            left.MatrixMultiply(right, out _, out _);

        public static int[,] MatrixMultiply(this int[,] left, int[,] right, out uint rows, out uint columns)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            if (right is null)
                throw new ArgumentNullException(nameof(right));

            int leftColumns = left.GetLength(1);
            int rightRows = right.GetLength(0);

            if (leftColumns != rightRows)
                throw new ArgumentException("The length of the left hand side matrix must equal the rank of the right hand side matrix.");

            rows = (uint)left.GetLength(0);
            columns = (uint)right.GetLength(1);

            int[,] product = new int[rows, columns];

            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                for (int currentColumn = 0; currentColumn < columns; currentColumn++)
                {
                    int sum = 0;

                    for (int columnIndex = 0; columnIndex < leftColumns; columnIndex++)
                        sum += left[rowIndex, columnIndex] * right[columnIndex, currentColumn];

                    product[rowIndex, currentColumn] = sum;
                }
            }

            return product;
        }

        public static long[,] MatrixMultiply(this long[,] left, long[,] right) =>
            left.MatrixMultiply(right, out _, out _);

        public static long[,] MatrixMultiply(this long[,] left, long[,] right, out uint rows, out uint columns)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            if (right is null)
                throw new ArgumentNullException(nameof(right));

            int leftColumns = left.GetLength(1);
            int rightRows = right.GetLength(0);

            if (leftColumns != rightRows)
                throw new ArgumentException("The length of the left hand side matrix must equal the rank of the right hand side matrix.");

            rows = (uint)left.GetLength(0);
            columns = (uint)right.GetLength(1);

            long[,] product = new long[rows, columns];

            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                for (int currentColumn = 0; currentColumn < columns; currentColumn++)
                {
                    long sum = 0;

                    for (int columnIndex = 0; columnIndex < leftColumns; columnIndex++)
                        sum += left[rowIndex, columnIndex] * right[columnIndex, currentColumn];

                    product[rowIndex, currentColumn] = sum;
                }
            }

            return product;
        }

        public static float[,] MatrixMultiply(this float[,] left, float[,] right) =>
            left.MatrixMultiply(right, out _, out _);

        public static float[,] MatrixMultiply(this float[,] left, float[,] right, out uint rows, out uint columns)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            if (right is null)
                throw new ArgumentNullException(nameof(right));

            int leftColumns = left.GetLength(1);
            int rightRows = right.GetLength(0);

            if (leftColumns != rightRows)
                throw new ArgumentException("The length of the left hand side matrix must equal the rank of the right hand side matrix.");

            rows = (uint)left.GetLength(0);
            columns = (uint)right.GetLength(1);

            float[,] product = new float[rows, columns];

            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                for (int currentColumn = 0; currentColumn < columns; currentColumn++)
                {
                    float sum = 0;

                    for (int columnIndex = 0; columnIndex < leftColumns; columnIndex++)
                        sum += left[rowIndex, columnIndex] * right[columnIndex, currentColumn];

                    product[rowIndex, currentColumn] = sum;
                }
            }

            return product;
        }

        public static double[,] MatrixMultiply(this double[,] left, double[,] right) =>
            left.MatrixMultiply(right, out _, out _);

        public static double[,] MatrixMultiply(this double[,] left, double[,] right, out uint rows, out uint columns)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            if (right is null)
                throw new ArgumentNullException(nameof(right));

            int leftColumns = left.GetLength(1);
            int rightRows = right.GetLength(0);

            if (leftColumns != rightRows)
                throw new ArgumentException("The length of the left hand side matrix must equal the rank of the right hand side matrix.");

            rows = (uint)left.GetLength(0);
            columns = (uint)right.GetLength(1);

            double[,] product = new double[rows, columns];

            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                for (int currentColumn = 0; currentColumn < columns; currentColumn++)
                {
                    double sum = 0;

                    for (int columnIndex = 0; columnIndex < leftColumns; columnIndex++)
                        sum += left[rowIndex, columnIndex] * right[columnIndex, currentColumn];

                    product[rowIndex, currentColumn] = sum;
                }
            }

            return product;
        }

        public static decimal[,] MatrixMultiply(this decimal[,] left, decimal[,] right) =>
            left.MatrixMultiply(right, out _, out _);

        public static decimal[,] MatrixMultiply(this decimal[,] left, decimal[,] right, out uint rows, out uint columns)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            if (right is null)
                throw new ArgumentNullException(nameof(right));

            int leftColumns = left.GetLength(1);
            int rightRows = right.GetLength(0);

            if (leftColumns != rightRows)
                throw new ArgumentException("The length of the left hand side matrix must equal the rank of the right hand side matrix.");

            rows = (uint)left.GetLength(0);
            columns = (uint)right.GetLength(1);

            decimal[,] product = new decimal[rows, columns];

            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                for (int currentColumn = 0; currentColumn < columns; currentColumn++)
                {
                    decimal sum = 0;

                    for (int columnIndex = 0; columnIndex < leftColumns; columnIndex++)
                        sum += left[rowIndex, columnIndex] * right[columnIndex, currentColumn];

                    product[rowIndex, currentColumn] = sum;
                }
            }

            return product;
        }
    }
}