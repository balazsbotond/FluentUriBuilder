using System;

namespace FluentUriBuilder
{
    internal static class ArgumentValidationExtensions
    {
        public static void ThrowIfNull(this string value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void ThrowIfNullOrWhiteSpace(this string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("The argument specified cannot be null or white space.", paramName);
            }
        }

        public static void ThrowIfNotInRange(this int value, int min, int max, string paramName)
        {
            if (value < min || value > max)
            {
                throw new ArgumentOutOfRangeException(paramName, $"Value should be between ${min} and ${max}, inclusive.");
            }
        }
    }
}
