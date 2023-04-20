using System.Collections.Generic;

namespace Hybel.ExtensionMethods
{
    /// <summary>
    /// Extension Methods used on IDictionaries.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Tries to find a value in the dictionary and checks the found value against <paramref name="valueToCheckEquality"/>.
        /// </summary>
        /// <param name="valueToCheckEquality">This is the value to check to dictionary's value against.</param>
        /// <param name="value">The found value.</param>
        /// <returns>True if a value was found and it's equal to <paramref name="valueToCheckEquality"/>.</returns>
        public static bool TryGetValueAndIfEquals<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue valueToCheckEquality, out TValue value)
        {
            bool isEqual = false;

            if (dictionary.TryGetValue(key, out value))
            {
                if (value is null || valueToCheckEquality is null)
                {
                    if (value is null && valueToCheckEquality is null)
                        return true;

                    return false;
                }

                isEqual = value.Equals(valueToCheckEquality); // isEqual can only become true if the dictionary contains the value.
            }

            // Since isEqual can only be true if the dictionary contains the value and that contained value equals the passed in valueToCheckEquality this represents the same as:
            // (dictionary.Contains(valueToCheckEquality) && dictionary[key] == valueToCheckEquality)
            // Can't do that though since its all generics...
            return isEqual;
        }

        /// <summary>
        /// Adds the same value to a list of provided keys into the <paramref name="dictionary"/>.
        /// </summary>
        public static void AddAll<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TKey> keys, TValue value)
        {
            foreach (var key in keys)
                dictionary.Add(key, value);
        }
    }
}