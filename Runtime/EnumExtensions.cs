using System;
using System.Linq;

using Random = UnityEngine.Random;

namespace Hybel.ExtensionMethods
{
    /// <summary>
    /// Extension Methods used on Enums.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Get a random value from an enum.
        /// </summary>
        /// <typeparam name="T">Generic Type.</typeparam>
        /// <param name="e">Generic Variable.</param>
        /// <returns>Random enum value.</returns>
        public static T GetRandom<T>() where T : struct, IComparable
        {
            if (!(typeof(T).IsEnum))
                throw new ArgumentException("Value must be an enum");

            return (T)(object)Random.Range(1, Enum.GetNames(typeof(T)).Length);
        }

        /// <summary>
        /// Converts an enum to int.
        /// </summary>
        /// <typeparam name="T">Generic Type.</typeparam>
        /// <param name="e">Generic Variable.</param>
        /// <returns>Int index of enum value.</returns>
        public static int ToInt<T>(this T e) where T : struct, IComparable
        {
            if (!(typeof(T).IsEnum))
                throw new ArgumentException("Argument must be an enum");
            return (int)(object)e;
        }

        public static bool IsDefined<T>(this T e, out T validEnum) where T : struct, IComparable
        {
            var definedValues = (int[])Enum.GetValues(typeof(T));

            validEnum = e;
            return definedValues.Any(definedValue => definedValue == e.ToInt());
        }
    }
}