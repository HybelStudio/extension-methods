using UnityEngine;

using Random = UnityEngine.Random;

namespace Hybel.ExtensionMethods
{
    /// <summary>
    /// Extension Methods used on integers.
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        /// Turn an int into a bool.
        /// </summary>
        /// <param name="value">Integer.</param>
        /// <returns>True if the value is 1, false if the value is 0, false with an error message otherwise.</returns>
        public static bool ToBool(this int value)
        {
            switch (value)
            {
                case 0:
                    return false;
                case 1:
                    return true;
                default:
                    Debug.LogError("A value of " + value + " cannot be converted to bool. Returning true by default.");
                    return true;
            }
        }

        /// <summary>
        /// Remaps a linear range to another linear range.
        /// </summary>
        /// <param name="value">Int.</param>
        /// <param name="valueRangeMin">Minimum value of starting range.</param>
        /// <param name="valueRangeMax">Maximum value of starting range.</param>
        /// <param name="newRangeMin">Minimum value of desired range.</param>
        /// <param name="newRangeMax">Maximum value of desired range.</param>
        /// <returns>Float altered by the remapped range.</returns>
        public static float MapRange(this int value, float valueRangeMin, float valueRangeMax, float newRangeMin, float newRangeMax) =>
            (value - valueRangeMin) / (valueRangeMax - valueRangeMin) * (newRangeMax - newRangeMin) + newRangeMin;

        /// <summary>
        /// Randomly changes the value to a positive or negative value.
        /// </summary>
        /// <param name="value">Integer.</param>
        /// <param name="negativeProbability">Probability factor.</param>
        /// <returns>Positive or negative value based on a probability factor.</returns>
        public static int WithRandomSign(this int value, float negativeProbability = 0.5f) =>
            Random.value < negativeProbability ? -value : value;

        /// <summary>
        /// Clamps minimum and maximum to given value.
        /// </summary>
        /// <param name="value">Integer.</param>
        /// <param name="min">Minimum.</param>
        /// <param name="max">Maximum.</param>
        /// <returns>Clamped value between min and max</returns>
        public static int Clamp(this int value, int? min = null, int? max = null)
        {
            value = Mathf.Max(value, min ?? value);
            value = Mathf.Min(value, max ?? value);
            return value;
        }

        /// <summary>
        /// Checks if value is negative.
        /// </summary>
        /// <param name="value">Integer.</param>
        /// <returns>True if value is negative.</returns>
        public static bool IsNegative(this int value) =>
            value < 0;

        /// <summary>
        /// Checks if value is zero.
        /// </summary>
        /// <param name="value">Integer.</param>
        /// <returns>True if value is zero.</returns>
        public static bool IsZero(this int value) =>
            value == 0;

        /// <summary>
        /// Checks if the value is Even.
        /// </summary>
        public static bool IsEven(this int value) =>
            value % 2 == 0;
    }
}