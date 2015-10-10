using System;

namespace FluentUriBuilder
{
    internal static class Extensions
    {
        public static void ThrowIfNullOrWhiteSpace(this string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("The argument specified cannot be null or white space.", paramName);
            }
        }

        public static void ThrowIfNull(this string value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
