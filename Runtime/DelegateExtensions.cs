using System;

namespace Hybel.ExtensionMethods
{
    /// <summary>
    /// Extension Methods used on Delegates.
    /// </summary>
    public static class DelegateExtensions
    {
        public static bool HasListeners(this Delegate @delegate) => !((@delegate?.GetInvocationList().Length ?? 0) == 0);
    }
}