using UnityEngine;

namespace Hybel.ExtensionMethods
{

    /// <summary>
    /// Extension Methods used on floats.
    /// </summary>
    public static class FloatExtensions
    {
        /// <summary>
        /// Turn a float into a bool.
        /// </summary>
        /// <param name="value">Float.</param>
        /// <returns>True if the value is 1, false if the value is 0, false with an error message otherwise.</returns>
        public static bool ToBool(this float value)
        {
            switch (value)
            {
                case 0:
                    return false;
                case 1:
                    return true;
                default:
                    Debug.LogError("A value of " + value + " cannot be converted to bool. Returning false by default.");
                    return false;
            }
        }

        /// <summary>
        /// Remaps a linear range to another linear range.
        /// </summary>
        /// <param name="value">Float.</param>
        /// <param name="valueRangeMin">Minimum value of starting range.</param>
        /// <param name="valueRangeMax">Maximum value of starting range.</param>
        /// <param name="newRangeMin">Minimum value of desired range.</param>
        /// <param name="newRangeMax">Maximum value of desired range.</param>
        /// <returns>Float altered by the remapped range.</returns>
        public static float MapRange(this float value, float valueRangeMin, float valueRangeMax, float newRangeMin, float newRangeMax) =>
            (value - valueRangeMin) / (valueRangeMax - valueRangeMin) * (newRangeMax - newRangeMin) + newRangeMin;

        /// <summary>
        /// Rounds the float value.
        /// </summary>
        public static float Round(this float value) =>
            Mathf.Round(value);

        /// <summary>
        /// Rounds to a multiple of a given value.
        /// </summary>
        public static float Round(this float value, float multiple) =>
            (value / multiple).Round() * multiple;

        /// <summary>
        /// Rounds to the closest int.
        /// </summary>
        public static int RoundToInt(this float value) => Mathf.RoundToInt(value);

        /// <summary>
        /// Floors the float value.
        /// </summary>
        public static float Floor(this float value) =>
            Mathf.Floor(value);

        /// <summary>
        /// Floors to a multiple of a given value.
        /// </summary>
        public static float Floor(this float value, float multiple) =>
            (value / multiple).Floor() * multiple;

        /// <summary>
        /// Rounds downwards to the closest int.
        /// </summary>
        public static int FloorToInt(this float value) => Mathf.FloorToInt(value);

        /// <summary>
        /// Ceils the float value.
        /// </summary>
        public static float Ceil(this float value) =>
            Mathf.Ceil(value);

        /// <summary>
        /// Ceils to a multiple of a given value.
        /// </summary>
        public static float Ceil(this float value, float multiple) =>
            (value / multiple).Ceil() * multiple;

        /// <summary>
        /// Rounds upwards to the closest int.
        /// </summary>
        public static int CeilToInt(this float value) => Mathf.CeilToInt(value);

        /// <summary>
        /// Clamps minimum and maximum to given value.
        /// </summary>
        /// <param name="value">Float.</param>
        /// <param name="min">Minimum.</param>
        /// <param name="max">Maximum.</param>
        /// <returns>Clamped value between min and max</returns>
        public static float Clamp(this float value, float? min = null, float? max = null)
        {
            value = Mathf.Max(value, min ?? value);
            value = Mathf.Min(value, max ?? value);
            return value;
        }

        /// <summary>
        /// Clamps minimum and maximum to given value in both the negative and positive direction.
        /// This means that clamping <paramref name="min"/> to 3 will clamp the value to 3 or -3 depending on if the value is positive or negative.
        /// </summary>
        /// <param name="value">Float.</param>
        /// <param name="min">Minimum.</param>
        /// <param name="max">Maximum.</param>
        /// <returns>Clamped value between min and max</returns>
        public static float SignedClamp(this float value, float? min = null, float? max = null)
        {
            bool valueIsPositive = !value.IsNegative();

            switch (valueIsPositive)
            {
                case true:
                    value = Mathf.Max(value, Mathf.Abs(min ?? value));
                    value = Mathf.Min(value, Mathf.Abs(max ?? value));
                    break;

                case false:
                    value = Mathf.Min(value, -Mathf.Abs(min ?? value));
                    value = Mathf.Max(value, -Mathf.Abs(max ?? value));
                    break;
            }

            return value;
        }

        /// <summary>
        /// Converts a Radian to a Vector2.
        /// </summary>
        /// <param name="radian">Radian.</param>
        /// <returns>Vector2 representation of a radian.</returns>
        public static Vector2 RadianToVector2(this float radian) =>
            new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));

        /// <summary>
        /// Converts a Degree to a Vector 2.
        /// </summary>
        /// <param name="degree">Degree.</param>
        /// <returns>Vector2 representation of a degree.</returns>
        public static Vector2 DegreeToVector2(this float degree) =>
            RadianToVector2(degree * Mathf.Deg2Rad);

        /// <summary>
        /// Checks if value is positive.
        /// <para>0 is not considered positive.</para>
        /// </summary>
        public static bool IsPositive(this float value) => value > 0;

        /// <summary>
        /// Checks if value is negative.
        /// <para>0 is not considered negative.</para>
        /// </summary>
        public static bool IsNegative(this float value) => value < 0;

        /// <summary>
        /// Checks if value is zero.
        /// </summary>
        public static bool IsZero(this float value)
        {
            if (value == 0)
                return true;
            return false;
        }

        /// <summary>
        /// Checks if <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        public static bool IsWithin(this float value, float min, float max) => value >= min && value < max;

        /// <summary>
        /// Checks if <paramref name="value"/> is outside the range of <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <returns></returns>
        public static bool IsBeyond(this float value, float min, float max) => value <= min || value > max;

        /// <summary>
        /// Converts a <paramref name="multiplier"/> value to a percentile value.
        /// </summary>
        public static float MultiplierToPercentile(this float multiplier) => multiplier * 100f;

        /// <summary>
        /// Converts a <paramref name="multiplier"/> value to a percentile value that reflects the difference in value.
        /// <para><b>Example:</b> A 0.8 <paramref name="multiplier"/> converts to -20 percentile difference.</para>
        /// <para><b>Example:</b> A 1.5 <paramref name="multiplier"/> converts to +50 percentile difference.</para>
        /// </summary>
        public static float MultiplierToPercentileDifference(this float multiplier) => (-1f + multiplier) * 100f;
    }
}