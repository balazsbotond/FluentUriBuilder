using System;
using System.Globalization;

namespace FluentUri
{
    internal static class StringHelper
    {
        public static string ToStringInvariantCulture(object obj)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}", obj);
        }

        public static bool IsNullOrWhiteSpace(string str)
        {
            // string.IsNotNullOrWhiteSpace backported to support .NET 2.0
            if (str == null) return true;

            for (int i = 0; i < str.Length; i++)
            {
                if (!Char.IsWhiteSpace(str[i])) return false;
            }

            return true;
        }
    }
}
