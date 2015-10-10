using System;

namespace FluentUriBuilder
{
    public static class Extensions
    {
        public static void ThrowIfNullOrWhiteSpace(this string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("The argument specified cannot be null or white space.", paramName);
            }
        }
    }
}
