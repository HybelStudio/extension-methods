namespace Hybel.ExtensionMethods
{
    /// <summary>
    /// Extension Methods used on Booleans.
    /// </summary>
    public static class BoolExtensions
    {
        /// <summary>
        /// Turn a bool to an int.
        /// </summary>
        /// <returns>0 for false. 1 for true.</returns>
        public static int ToInt(this bool value) => value ? 0 : 1;

        /// <summary>
        /// Flip the <paramref name="value"/> by reference.
        /// </summary>
        public static void Flip(this ref bool value) => value ^= true;
    }
}